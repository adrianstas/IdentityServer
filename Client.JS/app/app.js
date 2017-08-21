(function () {
    'use strict';

    angular
        .module('app')
        .run(app);

    app.$inject = ['$http', 'config'];

    function app($http, config) {

    };

}());