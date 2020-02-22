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
        public static string GetKey => "";
        public static string AddKey => "";
        public static string RemoveKey => "";
        public static string UpdateItemKey => "";
        public static string UpdateAllKey => "";
        public static string UploadKey => "";
#else
        public static string BackendFunctionURL => "https://hanselmanforms.azurewebsites.net";
        
        public static string GetKey => "?code=AgpOflfg8bvWmvapaXBOW/xeko1G83P0zOdEvLQZlsWT9h70V1WjBg==";
        public static string AddKey => "?code=At0klmT6mm7VsF/86XYJAGdkQzygM/mtsfnawi4EPtex/Ypw0v2Xyw==";
        public static string RemoveKey => "?code=0fGDi8Iyr5aHZhDxh7D85L/vL9TPP1E5BjRCk5DBq64aWzyFEtvSVA==";
        public static string UpdateItemKey => "?code=GqBtPqO2a7JHfZkablND0E5jZDzcTJVPK0JJOJaJCqylF8WCa/qAvw==";
        public static string UpdateAllKey => "?code=2ORW6AXso4uqxehej0jN0RzQOFiCVzcbq1AJ30xj54hS2N40aw1f1Q==";
        public static string UploadKey => "?code=nkpF4s3q4sd9k21O1N7OGgdGOn5UKRU7zy5Xr3h29cIu1tH9C7mAOA==";
#endif

        public static string GetFeaturedItemsURL => $"{BackendFunctionURL}/api/GetFeaturedItems{GetKey}";
        public static string AddFeturedItemURL => $"{BackendFunctionURL}/api/AddFeaturedItem{AddKey}";
        public static string RemoveFeturedItemURL => $"{BackendFunctionURL}/api/RemoveFeaturedItem{RemoveKey}";
        public static string UpdateFeturedItemURL => $"{BackendFunctionURL}/api/UpdateFeaturedItem{UpdateItemKey}";
        public static string UpdateAllFeturedItemURL => $"{BackendFunctionURL}/api/UpdateAllFeaturedItems{UpdateAllKey}";

        public static string AuthMeEndpoint => "/.auth/me";
        public static string LogOutEndpoint => "/.auth/logout";
        public static string TwitterEndpoint => "/.auth/login/twitter";
        public static string PostloginRedirect => "?post_login_redirect_url=";
        public static string LoginMode => "&session_mode=token";
    }
}