﻿(function() {
    'use strict';

    angular
        .module('core.module')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope', 'RegularResource', 'SecretResource', 'OidcManager'];

    function HomeController($scope, RegularResource, SecretResource, OidcManager) {

        var vm = this;

        vm.getData = _getData;
        vm.performLogin = _performLogin;
        vm.clearStorage = _clearStorage;
        vm.logOut = _logOut;

        vm.publicValues = undefined;
        vm.managementValues = undefined;
        vm.recruitmentValues = undefined;
        vm.secretValues = undefined;
        vm.recruitmentAuthorized = true;
        vm.managementAuthorized = true;
        vm.secretAuthorized = true;
        vm.userName = localStorage["user_name"];

        $scope.$on('userLoggedIn', function (event, data) {
            vm.userName = data.userName;
            $scope.$apply();
        });

        $scope.$on('userLoggedOut', function (event, data) {
            vm.userName = undefined;
            localStorage.removeItem("user_name");
            localStorage.removeItem("access_token");
            $scope.$apply();
        });

        var mgr = OidcManager.OidcTokenManager();

        function _performLogin() {
            mgr.signinRedirect().then(function() {
            }).catch(function(err) {
            });
        }

        function _clearStorage() {
            localStorage['access_token'] = undefined;
        }

        function _getData() {
            RegularResource.PublicValues.query({ ommitBearerToken: true },
                function(response) {
                    vm.publicValues = response;
                },
                function(error) {
                    console.log(error);
                });

            RegularResource.ManagementValues.query({},
                function(response) {
                    vm.managementValues = response;
                    vm.managementAuthorized = true;
                },
                function(error) {
                    console.log(error);
                    vm.managementAuthorized = false;
                });

            RegularResource.RecruitmentValues.query({},
                function(response) {
                    vm.recruitmentValues = response;
                    vm.recruitmentAuthorized = true;
                },
                function(error) {
                    console.log(error);
                    vm.recruitmentAuthorized = false;
                });

            SecretResource.SecretValues.query({},
                function(response) {
                    vm.secretValues = response;
                    vm.secretAuthorized = true;
                },
                function(error) {
                    console.log(error);
                    vm.secretAuthorized = false;
                });
        }

        function _logOut() {

            mgr.signoutRedirect().then(function () {
                console.log('logged out');
            }).catch(function (err) {
            });
        }

    }

}());