'use strict';
app.factory('applicationService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'applicationContext', function ($http, $log, ngAuthSettings, $rootScope, applicationContext) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initializedEventName = 'applicationService.onInitialized';

    var serviceFactory = {};    

    var get = function () {
        return $http.post(serviceBase + 'api/application/get').then(function (results) {
            applicationContext.application = results.data;
            applicationContext.applicationMQ = results.data.applicationMQ;
            applicationContext.applicationMQ.application = results.data;
            applicationContext.applicationLoaded = true;
            $rootScope.$emit(initializedEventName);
        });
    };

    get();

    serviceFactory.initializedEventName = initializedEventName;

    return serviceFactory;  

}]);