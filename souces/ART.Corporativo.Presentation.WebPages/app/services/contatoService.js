'use strict';
app.factory('contatoService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.corporativoDistributedServicesUri;

    var serviceFactory = {};

    var _get = function () {

        return $http.get(serviceBase + 'api/contato').then(function (results) {
            return results;
        });
    };

    serviceFactory.get = _get;

    return serviceFactory;

}]);