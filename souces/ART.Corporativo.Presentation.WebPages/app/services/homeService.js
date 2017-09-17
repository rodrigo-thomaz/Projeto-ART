'use strict';
app.factory('homeService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.corporativoDistributedServicesUri;

    var serviceFactory = {};

    var _get = function () {

        return $http.get(serviceBase + 'api/home').then(function (results) {
            return results;
        });
    };

    serviceFactory.get = _get;

    return serviceFactory;

}]);