namespace Client.MVC.Code
{
    namespace Constants
    {
        public class IdentityConstants
        {
            public const string IssuerUri = "https://localhost:44388";
            public const string APIRegular = "https://localhost:44321/api/values";
            public const string APISecret = "https://localhost:44310/api/values";

            public const string TokenEndoint = "https://localhost:44388/connect/token";
            public const string UserInfoEndoint = "https://localhost:44388/connect/userinfo";
            public const string AuthEndoint = "https://localhost:44388/connect/authorize";

            public const string MVCClientSecretHybrid = "mvc_client_hybrid";
            public const string MVCHybridCallback = "http://localhost:59196/Values/Index";
        }
    }
}