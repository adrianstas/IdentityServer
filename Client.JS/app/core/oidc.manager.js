(function () {
    'use strict';

    // oidc manager for dep injection
    angular.module("core.module")
        .factory("OidcManager", function ($rootScope) {

            var settings = {
                authority: 'https://localhost:44388',
                client_id: 'angular_client_implicit',
                redirect_uri: 'http://localhost:53364/callback.html',
                response_type: 'token',
                scope: 'regular secret'
            };

            var mgr = new Oidc.UserManager(settings);

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