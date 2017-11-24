'use strict';

app.factory('applicationMQ', [function () {

    var serviceFactory = {};    
    
    var init = function (applicationMQ) {

        serviceFactory.user = applicationMQ.user;
        serviceFactory.password = applicationMQ.password;
        serviceFactory.applicationTopic = applicationMQ.applicationTopic;
        serviceFactory.webUITopic = applicationMQ.webUITopic;

        serviceFactory.initialized = true;
    }
    
    //Functions
    serviceFactory.init = init;

    //Properties
    serviceFactory.initialized = false;

    serviceFactory.user = null;
    serviceFactory.password = null;
    serviceFactory.applicationTopic = null;
    serviceFactory.webUITopic = null;

    return serviceFactory;

}]);

app.factory('applicationMQService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'applicationMQ', function ($http, $log, ngAuthSettings, $rootScope, applicationMQ) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var getApplicationMQ = function () {
        return $http.post(serviceBase + 'api/applicationMQ/get').then(function (results) {
            applicationMQ.init(results.data);
            $rootScope.$emit('applicationMQServiceInitialized');
        });
    };

    getApplicationMQ();

    return serviceFactory;   

}]);