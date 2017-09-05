(function () {
    'use strict';

    // oidc manager for dep injection
    angular.module("core.module")
        .factory("OidcManager", function ($rootScope) {

            var settings = {
                authority: 'https://localhost:44388',
                client_id: 'angular_client_implicit_open_id',
                redirect_uri: 'http://localhost:53364/callback.html',
                post_logout_redirect_uri: 'http://localhost:53364/logout_callback.html',
                response_type: 'id_token token',
                scope: 'openid profile regular secret additional',
                EnablePostSignOutAutoRedirect: true,
                filterProtocolClaims: true,
                loadUserInfo: true,
                userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
                silent_redirect_uri: window.location.protocol + "//" + window.location.host + '/silent_callback.html',
                accessTokenExpiringNotificationTime: 15,
                automaticSilentRenew: true
            };

            var mgr = new Oidc.UserManager(settings);


            mgr.events.addAccessTokenExpiring(function () {
                console.log("access token expiring");
                mgr.signinRedirect();
            });

            mgr.events.addAccessTokenExpired(function () {
                console.log("token expired");                
            });

            mgr.events.addSilentRenewError(function (e) {
                console.log("silent renew error", e.message);
            });

            mgr.events.addUserLoaded(function (user) {
                console.log("user loaded", user);
                mgr.getUser().then(function (user) {
                    $rootScope.$broadcast('userLoggedIn', {
                        userName: user.profile.given_name
                    });
                });
            });

            mgr.events.addUserUnloaded(function (user) {
                console.log("user loaded", user);
                $rootScope.$broadcast('userLoggedOut');
            });

            return {
                OidcTokenManager: function () {
                    return mgr;
                }
            };
        });
})();