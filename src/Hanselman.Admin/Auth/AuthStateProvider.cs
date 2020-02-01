using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Web;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;

namespace Hanselman.Admin.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        readonly HttpClient httpClient;
        readonly IJSRuntime jsRuntime;
        NavigationManager manager;
        public AuthStateProvider(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager manager)
        {
            this.httpClient = httpClient;
            this.jsRuntime = jsRuntime;
            this.manager = manager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetAuthToken();

            if (token?.AuthenticationToken != null)
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", token.AuthenticationToken);
                try
                {
                    var authResponse = await httpClient.GetStringAsync(Constants.AzureFunctionAuthURL + Constants.AuthMeEndpoint);
                    
                    //To see the response uncomment the line below
                    //Console.WriteLine(authResponse);

                    await LocalStorage.SetAsync(jsRuntime, "authtoken", token);
                    var authInfo = JsonSerializer.Deserialize<List<AuthInfo>>(authResponse);
                    switch (authInfo[0].ProviderName)
                    {
                        case "twitter":
                            {
                                var user = authInfo[0].UserId.ToLower();
                                if (user != "jamesmontemagno" && user != "shanselman")
                                    throw new AccessViolationException("Only scott and james can access this.");

                            }
                            return await GetTwitterClaims(authInfo[0]);
                        default: break;
                    }                    
                }
                catch (AccessViolationException e)
                {
                    Console.WriteLine("Not authorized: " + e.Message);
                    httpClient.DefaultRequestHeaders.Remove("X-ZUMO-AUTH");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Unable to authenticate " + e.Message);
                    httpClient.DefaultRequestHeaders.Remove("X-ZUMO-AUTH");
                }
            }
            await LocalStorage.DeleteAsync(jsRuntime, "authtoken");
            var identity = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
        async Task<AuthToken> GetAuthToken()
        {
            var authTokenFragment = HttpUtility.UrlDecode(new Uri(manager.Uri).Fragment);
            if (string.IsNullOrEmpty(authTokenFragment))
            {
                return await LocalStorage.GetAsync<AuthToken>(jsRuntime, "authtoken");
            }
            var getJsonRegEx = new Regex(@"\{(.|\s)*\}");
            var matches = getJsonRegEx.Matches(authTokenFragment);
            if (matches.Count == 1)
            {
                AuthToken authToken;
                try
                {
                    authToken = JsonSerializer.Deserialize<AuthToken>(matches[0].Value);
                }
                // JsonSerializer in preview, don't know what it will thow.
                catch (Exception e)
                {
                    Console.WriteLine($"Error in authentication token: {e.Message}");
                    return new AuthToken();
                }
                await jsRuntime.InvokeAsync<string>(
                        "EasyAuthDemoUtilities.updateURLwithoutReload", Constants.BlazorWebsiteURL);
                return authToken;
            }
            return new AuthToken();
        }
        Task<AuthenticationState> GetTwitterClaims(AuthInfo authInfo)
        {
            var userClaims = new List<Claim>();
            
            foreach (var userClaim in authInfo.UserClaims)
            {
                userClaims.Add(new Claim(userClaim.Type, userClaim.Value));
            }
            var identity = new ClaimsIdentity(userClaims, "EasyAuth");
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
        public async Task Logout()
        {
            var authresponse = await httpClient.GetAsync(Constants.AzureFunctionAuthURL + Constants.LogOutEndpoint);
            httpClient.DefaultRequestHeaders.Remove("X-ZUMO-AUTH");
            await LocalStorage.DeleteAsync(jsRuntime, "authtoken");
            if(authresponse.IsSuccessStatusCode)
            {
                NotifyAuthenticationStateChanged();
            }          
        }
        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}