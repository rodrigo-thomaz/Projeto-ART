'use strict';
app.factory('stompService', ['$log', 'ngAuthSettings', '$rootScope', 'applicationMQ', function ($log, ngAuthSettings, $rootScope, applicationMQ) {
    
    var serviceFactory = {};    
    
    serviceFactory.session = null; 
    
    var onConnected = function (frame) {
        $rootScope.$emit('stompService_onConnected', frame);
    }

    var onError = function (frame) {

        serviceFactory.session = null;

        $rootScope.$emit('stompService_onError', frame);
    }

    var subscribe = function (topic, callback) {
        var applicationTopic = applicationMQ.applicationTopic;
        client.subscribe('/topic/ART.Application.' + applicationTopic + '.WebUI.' + serviceFactory.session + '.' + topic, callback);
    };

    var unsubscribe = function (topic) {
        var applicationTopic = applicationMQ.applicationTopic;
        client.unsubscribe('/topic/ART.Application.' + applicationTopic + '.WebUI.' + serviceFactory.session + '.' + topic);
    };    

    var onApplicationMQInitialized = function (event, data) {

        clearOnApplicationMQInitialized();

        var headers = {
            login: applicationMQ.user,
            passcode: applicationMQ.password,
        };

        client = Stomp.client(url);

        serviceFactory.client = client;

        serviceFactory.session = applicationMQ.webUITopic;

        var wsBrokerHostName = ngAuthSettings.wsBrokerHostName;
        var wsBrokerPort = ngAuthSettings.wsBrokerPort;        

        client.connect(headers, onConnected, onError);    
    };    

    $rootScope.$on('$destroy', function () {
        clearOnApplicationMQInitialized();        
    });

    var url = 'ws://' + wsBrokerHostName + ':' + wsBrokerPort + '/ws';

    var client = Stomp.client(url);

    var clearOnApplicationMQInitialized = $rootScope.$on('applicationMQServiceInitialized', onApplicationMQInitialized);        
    
    // serviceFactory    

    serviceFactory.subscribe = subscribe;
    serviceFactory.unsubscribe = unsubscribe;    

    return serviceFactory;   

}]);