'use strict';

app.factory('applicationMQ', [function () {

    var serviceFactory = {};    
    
    var init = function (applicationMQ) {
        serviceFactory.brokerApplicationTopic = applicationMQ.brokerApplicationTopic;
        serviceFactory.initialized = true;
    }
    
    //Functions
    serviceFactory.init = init;

    //Properties
    serviceFactory.initialized = false;
    serviceFactory.brokerApplicationTopic = null;

    return serviceFactory;

}]);

app.factory('applicationMQService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'applicationMQ', function ($http, $log, ngAuthSettings, $rootScope, stompService, applicationMQ) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    serviceFactory.applicationMQ = {};

    var initialized = false;
    var initializing = false;

    var onConnected = function () {
        if (!initialized && initializing) {
            return;
        }
        else if (!initialized && !initializing) {
            stompService.subscribe('ApplicationMQ.GetViewCompleted', onGetApplicationMQCompleted);
            getApplicationMQ();
            initializing = true;            
        }    
    }   

    var getApplicationMQ = function () {
        return $http.post(serviceBase + 'api/applicationMQ/get').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetApplicationMQCompleted = function (payload) {        
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        applicationMQ.init(JSON.parse(dataUTF8));
        clearOnConnected();
        initialized = true;
        initializing = false;
        $rootScope.$emit('applicationMQServiceInitialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.client.connected)
        onConnected();

    return serviceFactory;   

}]);