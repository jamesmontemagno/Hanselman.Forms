namespace Hanselman.Admin.Auth
{
    public static class Constants
    {
#if DEBUG
        public static string BlazorWebsiteURL => "https://6a7046f0.ngrok.io";
#else
        public static string BlazorWebsiteURL => "https://hanselmanadmin.z21.web.core.windows.net/";    
#endif
        public static string AzureFunctionAuthURL => "https://hanselmanforms.azurewebsites.net";

#if DEBUG
        public static string BackendFunctionURL => "http://localhost:7071";
#else
        public static string BackendFunctionURL => "https://hanselmanforms.azurewebsites.net";
#endif

        public static string GetFeaturedItemsURL => $"{BackendFunctionURL}/api/GetFeaturedItems";
        public static string AddFeturedItemURL => $"{BackendFunctionURL}/api/AddFeaturedItem";
        public static string RemoveFeturedItemURL => $"{BackendFunctionURL}/api/RemoveFeaturedItem";
        public static string UpdateFeturedItemURL => $"{BackendFunctionURL}/api/UpdateFeaturedItem";
        public static string UpdateAllFeturedItemURL => $"{BackendFunctionURL}/api/UpdateAllFeaturedItems";

        public static string AuthMeEndpoint => "/.auth/me";
        public static string LogOutEndpoint => "/.auth/logout";
        public static string TwitterEndpoint => "/.auth/login/twitter";
        public static string PostloginRedirect => "?post_login_redirect_url=";
        public static string LoginMode => "&session_mode=token";
    }
}