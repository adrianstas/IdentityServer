using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.Default;
using Owin;
using Serilog;
using STS.Config;
using System;
using System.Security.Cryptography.X509Certificates;

namespace STS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map(string.Empty, idsrvApp =>
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Trace()
                    .CreateLogger();

                var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(IdentityScopes.Get())
                    .UseInMemoryUsers(Users.Get());

                var corsPolicyService = new DefaultCorsPolicyService()
                {
                    AllowAll = true
                };

                idServerServiceFactory.CorsPolicyService = new
                    Registration<IdentityServer3.Core.Services.ICorsPolicyService>(corsPolicyService);

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "Security Token Service",
                    IssuerUri = Constants.IssuerUri,
                    PublicOrigin = Constants.Origin,
                    SigningCertificate = LoadCertificate(),
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        EnablePostSignOutAutoRedirect = true,
                        PostSignOutAutoRedirectDelay = 5
                    },
                    LoggingOptions = new LoggingOptions()
                    {
                        WebApiDiagnosticsIsVerbose = true,
                        EnableWebApiDiagnostics = true,
                        EnableKatanaLogging = true,
                        EnableHttpLogging = true
                    }
                };

                idsrvApp.UseIdentityServer(options);
            });
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2($@"{AppDomain.CurrentDomain.BaseDirectory}Certificate\Certificate.pfx", "Test123");
        }
    }
}