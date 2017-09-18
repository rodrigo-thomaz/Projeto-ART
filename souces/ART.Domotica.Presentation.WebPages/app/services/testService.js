'use strict';
app.factory('testService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.adminDistributedServicesUri;

    var serviceFactory = {};

    var _get = function () {

        return $http.get(serviceBase + 'api/test').then(function (results) {
            return results;
        });
    };

    serviceFactory.get = _get;

    return serviceFactory;

}]);