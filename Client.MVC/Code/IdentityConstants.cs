namespace Client.MVC.Code
{
    namespace Constants
    {
        public class IdentityConstants
        {
            public const string APIRegular = "https://localhost:44321/api/values";
            public const string APISecret = "https://localhost:44310/api/values";

            public const string TokenEndoint = "https://localhost:44388/connect/token";
            public const string AuthEndoint = "https://localhost:44388/connect/authorize";

            public const string MVCClientSecret = "mvc_client_auth_code";
            public const string MVCClientSecretAuthCode = "mvc_client_auth_code";

            public const string MVCAuthCodeCallback = "http://localhost:59196/callback";
        }
    }
}