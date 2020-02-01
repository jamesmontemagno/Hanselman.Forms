using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Hanselman.Admin.Auth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hanselman.Functions.Auth
{
    public static class AuthInfoExtensions
    {
        private static HttpClient httpClient = new HttpClient(); // cache and reuse to avoid repeated creation on Function calls

        /// <summary>
        /// Find a claim of the specified type
        /// </summary>
        /// <param name="authInfo"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static AuthUserClaim GetClaim(this AuthInfo authInfo, string claimType)
        {
            return authInfo.UserClaims.FirstOrDefault(c => c.Type == claimType);
        }

        public static async Task<AuthInfo> GetAuthInfoAsync(HttpRequest req)
        {
            var zumoAuthToken = req.Headers["X-ZUMO-AUTH"].ToString();

            if (string.IsNullOrEmpty(zumoAuthToken))
            {
                return null;
            }
            var authMeRequest = new HttpRequestMessage(HttpMethod.Get, GetEasyAuthEndpoint())
            {
                Headers =
                        {
                            { "X-ZUMO-AUTH", zumoAuthToken }
                        }
            };
            var response = await httpClient.SendAsync(authMeRequest);
            var json = await response.Content.ReadAsStringAsync();
            try
            {
                var infos = JsonConvert.DeserializeObject<AuthInfo[]>(json);
                return infos.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the EasyAuth properties for the currently authenticated user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<AuthInfo> GetAuthInfoAsync(this HttpRequestMessage request)
        {
            var zumoAuthToken = request.GetZumoAuthToken();
            if (string.IsNullOrEmpty(zumoAuthToken))
            {
                return null;
            }
            var authMeRequest = new HttpRequestMessage(HttpMethod.Get, GetEasyAuthEndpoint())
            {
                Headers =
                        {
                            { "X-ZUMO-AUTH", zumoAuthToken }
                        }
            };
            var response = await httpClient.SendAsync(authMeRequest);
            var authInfoArray = await response.Content.ReadAsAsync<AuthInfo[]>();
            return authInfoArray.Length >= 1 ? authInfoArray[0] : null; // The .auth/me content is a single item array if it is populated
        }

        private static string GetEasyAuthEndpoint()
        {
            // Get the hostname from environment variables so that we don't need config - thank you App Service!
            var hostname = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
            var authMe = Environment.GetEnvironmentVariable("AUTH_ME");
            if (!string.IsNullOrWhiteSpace(authMe))
                hostname = authMe;

            // Build up the .auth/me url
            var requestUri = $"https://{hostname}/.auth/me";
            return requestUri;
        }

        private static string GetZumoAuthToken(this HttpRequestMessage req)
        {
            return req.Headers.GetValues("X-ZUMO-AUTH").FirstOrDefault();
        }
    }
}
