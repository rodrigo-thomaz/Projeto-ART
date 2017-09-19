'use strict';
app.factory('termometroService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.domoticaDistributedServicesUri;

    var serviceFactory = {};

    var _get = function () {

        return $http.get(serviceBase + 'api/termometro').then(function (results) {
            return results;
        });
    };

    serviceFactory.get = _get;

    return serviceFactory;

}]);