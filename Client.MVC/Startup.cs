﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Client.MVC.Code;
using Client.MVC.Code.Constants;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Client.MVC.Startup))]

namespace Client.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            AntiForgeryConfig.UniqueClaimTypeIdentifier =
                IdentityModel.JwtClaimTypes.Name;

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "mvc_client_hybrid",
                Authority = IdentityConstants.IssuerUri,
                RedirectUri = IdentityConstants.MVCHybridCallback,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token token",
                Scope = "openid regular secret additional offline_access",
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    AuthenticationFailed = n =>
                    {
                        Console.WriteLine(n.Exception.Message);
                        return null;
                    },
                    SecurityTokenValidated = async n =>
                    {
                        TokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);
                        TokenHelper.DecodeAndWrite(n.ProtocolMessage.AccessToken);

                        var idTokenClaim = new Claim("id_token", n.ProtocolMessage.IdToken);

                        var givenNameClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.GivenName);

                        var scopeClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.Scope);

                        var familyNameClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.FamilyName);

                        if (givenNameClaim == null && familyNameClaim == null)
                        {
                            var userInfo = new UserInfoClient(IdentityConstants.UserInfoEndoint);
                            var userInfoResponse = await userInfo.GetAsync(n.ProtocolMessage.AccessToken);

                            givenNameClaim =
                                userInfoResponse.Claims.FirstOrDefault(
                                    claim => claim.Type == IdentityModel.JwtClaimTypes.GivenName);

                            familyNameClaim = userInfoResponse.Claims.FirstOrDefault(
                                claim => claim.Type == IdentityModel.JwtClaimTypes.FamilyName);
                        }

                        var subClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.Subject);

                        // create a new claims, issuer + sub as unique identifier
                        var nameClaim = new Claim(IdentityModel.JwtClaimTypes.Name,
                            IdentityConstants.IssuerUri + subClaim.Value);

                        var newClaimsIdentity = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            IdentityModel.JwtClaimTypes.Name,
                            IdentityModel.JwtClaimTypes.Role);

                        newClaimsIdentity.AddClaim(nameClaim);
                        newClaimsIdentity.AddClaim(idTokenClaim);

                        if (givenNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(givenNameClaim);
                        }

                        if (familyNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(familyNameClaim);
                        }

                        if (scopeClaim != null)
                        {
                            newClaimsIdentity.AddClaim(scopeClaim);
                        }

                        var tokenClientForRefreshToken = new TokenClient(IdentityConstants.TokenEndoint,
                            "mvc_client_hybrid", IdentityConstants.MVCClientSecretHybrid);

                        var refreshTokenResponse =
                            await tokenClientForRefreshToken.RequestAuthorizationCodeAsync(n.ProtocolMessage.Code,
                                IdentityConstants.MVCHybridCallback);

                        var expirationDateAsRoundtripString = DateTime.SpecifyKind(
                                DateTime.UtcNow.AddSeconds(refreshTokenResponse.ExpiresIn), DateTimeKind.Utc)
                            .ToString("o");

                        newClaimsIdentity.AddClaim(new Claim("refresh_token", refreshTokenResponse.RefreshToken));
                        newClaimsIdentity.AddClaim(new Claim("access_token", refreshTokenResponse.AccessToken));
                        newClaimsIdentity.AddClaim(new Claim("expires_at", expirationDateAsRoundtripString));

                        // create a new authentication ticket, overwriting the old one.
                        n.AuthenticationTicket = new AuthenticationTicket(
                            newClaimsIdentity,
                            n.AuthenticationTicket.Properties);
                    },
                    RedirectToIdentityProvider = n =>
                    {
                        // if signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");
                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }
                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}