(function () {
    'use strict';

    angular
        .module('core.module')
        .service('authInterceptorService', authInterceptorService);

    authInterceptorService.$inject = ['localStorageService'];

    function authInterceptorService(localStorageService) {

        var vm = this;
        vm.request = request;

        function request(config) {

            config.headers = config.headers || {};

            console.log('This gets executed before each request');

            return config;
        }
    }
})();