'use strict';
app.factory('applicationService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'mainContext', function ($http, $log, ngAuthSettings, $rootScope, mainContext) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initializedEventName = 'applicationService.onInitialized';

    var serviceFactory = {};    

    var get = function () {
        return $http.post(serviceBase + 'api/application/get').then(function (results) {
            mainContext.application = results.data;
            mainContext.applicationMQ = results.data.applicationMQ;
            mainContext.applicationMQ.application = results.data;
            mainContext.applicationLoaded = true;
            $rootScope.$emit(initializedEventName);
        });
    };

    get();

    serviceFactory.initializedEventName = initializedEventName;

    return serviceFactory;  

}]);