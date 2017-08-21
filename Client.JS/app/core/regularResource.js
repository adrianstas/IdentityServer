(function() {
    'use strict';

    angular
        .module('core.module')
        .factory('RegularResource', RegularResource);

    RegularResource.$inject = ['$resource', 'config'];

    function RegularResource($resource, config) {
        return {
            ManagementValues: $resource(config.regularApiUrl + '/api/values/ManagementValues'),
            RecruitmentValues: $resource(config.regularApiUrl + '/api/values/RecruitmentValues'),
            PublicValues: $resource(config.regularApiUrl + '/api/values/PublicValues')
        };
    }
})();