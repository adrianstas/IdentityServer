(function () {
    'use strict';

    angular.module('core.module')
    .constant("config", {
        regularApiUrl: "https://localhost:44321",
        secretApiUrl: "https://localhost:44310"
    });
}());

