(function() {
    'use strict';

    angular
        .module('core.module')
        .factory('SecretResource', SecretResource);

    SecretResource.$inject = ['$resource', 'config'];

    function SecretResource($resource, config) {
        return {
            SecretValues: $resource(config.secretApiUrl + '/api/values/SecretValues')
        };
    }
})();