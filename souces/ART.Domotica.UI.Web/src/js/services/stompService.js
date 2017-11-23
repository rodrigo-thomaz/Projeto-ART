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
        client.subscribe('/topic/ART.WebUI.' + serviceFactory.session + '.' + topic, callback);
    };

    var unsubscribe = function (topic) {
        client.unsubscribe('/topic/ART.WebUI.' + serviceFactory.session + '.' + topic);
    };

    var getRandomInt = function (min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min)) + min;
    };

    var onApplicationMQInitialized = function (event, data) {
        clearOnApplicationMQInitialized();
        //client.connect(headers, onConnected, onError);    
    };

    var clearOnApplicationMQInitialized = $rootScope.$on('applicationMQServiceInitialized', onApplicationMQInitialized);        

    $rootScope.$on('$destroy', function () {
        clearOnApplicationMQInitialized();
    });
    
    // stomp

    var wsBrokerHostName = ngAuthSettings.wsBrokerHostName;
    var wsBrokerPort = ngAuthSettings.wsBrokerPort;

    var url = 'ws://' + wsBrokerHostName + ':' + wsBrokerPort + '/ws';

    var client = Stomp.client(url);

    var headers = {
        login: 'test',
        passcode: 'test',        
    };

    client.connect(headers, onConnected, onError);    

    // serviceFactory    

    serviceFactory.subscribe = subscribe;
    serviceFactory.unsubscribe = unsubscribe;
    
    serviceFactory.client = client;

    var clientTopic = getRandomInt(100000000, 999999999);
    serviceFactory.session = clientTopic;

    return serviceFactory;   

}]);