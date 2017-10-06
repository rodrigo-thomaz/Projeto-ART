'use strict';
app.factory('thermometerDeviceService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var get = function () {

        return $http.get(serviceBase + 'api/thermometer').then(function (results) {
            return results;
        });
    };

    //serviceFactory
    
    serviceFactory.get = get;

    return serviceFactory;

}]);