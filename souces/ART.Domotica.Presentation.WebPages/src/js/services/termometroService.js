'use strict';
app.factory('termometroService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var get = function () {

        return $http.get(serviceBase + 'api/termometro').then(function (results) {
            return results;
        });
    };

    //serviceFactory
    
    serviceFactory.get = get;

    return serviceFactory;

}]);