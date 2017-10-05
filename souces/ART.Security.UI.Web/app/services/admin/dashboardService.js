'use strict';
app.factory('dashboardService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var dashboardServiceFactory = {};

    var _getDashboards = function () {

        return $http.get(serviceBase + 'api/dashboard').then(function (results) {
            return results;
        });
    };

    dashboardServiceFactory.getDashboards = _getDashboards;

    return dashboardServiceFactory;

}]);