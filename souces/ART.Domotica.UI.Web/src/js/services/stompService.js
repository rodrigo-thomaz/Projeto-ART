'use strict';
app.factory('stompService', ['$log', 'ngAuthSettings', '$rootScope', 'applicationMQ', function ($log, ngAuthSettings, $rootScope, applicationMQ) {
    
    var serviceFactory = {};    
    
    var onConnected = function (frame) {
        $rootScope.$emit('stompService_onConnected', frame);
    }

    var onError = function (frame) {
        $rootScope.$emit('stompService_onError', frame);
    }

    var subscribe = function (topic, callback) {
        client.subscribe(generateStringTopic(topic), callback);
    };

    var subscribeAllViews = function (topic, callback) {
        client.subscribe(generateStringTopicAllViews(topic), callback);
    };

    var unsubscribe = function (topic) {
        client.unsubscribe(generateStringTopic(topic));
    };    

    var generateStringTopic = function (topic){
        return '/topic/ART.Application.' + applicationMQ.applicationTopic + '.WebUI.' + applicationMQ.webUITopic + '.' + topic;
    }

    var generateStringTopicAllViews = function (topic) {
        return '/topic/ART.Application.' + applicationMQ.applicationTopic + '.WebUI.' + topic;
    }

    var connected = function () {
        return client.connected;
    };

    var onApplicationMQInitialized = function (event, data) {

        clearOnApplicationMQInitialized();

        var headers = {
            login: applicationMQ.user,
            passcode: applicationMQ.password,
        };

        client = Stomp.client(url);

        serviceFactory.client = client;         

        client.connect(headers, onConnected, onError);    
    };    

    $rootScope.$on('$destroy', function () {
        clearOnApplicationMQInitialized();        
    });

    var wsBrokerHostName = ngAuthSettings.wsBrokerHostName;
    var wsBrokerPort = ngAuthSettings.wsBrokerPort;       

    var url = 'ws://' + wsBrokerHostName + ':' + wsBrokerPort + '/ws';

    var client = Stomp.client(url);

    var clearOnApplicationMQInitialized = $rootScope.$on('applicationMQServiceInitialized', onApplicationMQInitialized);        
    
    // serviceFactory    

    serviceFactory.subscribe = subscribe;
    serviceFactory.subscribeAllViews = subscribeAllViews;
    serviceFactory.unsubscribe = unsubscribe;
    serviceFactory.connected = connected;

    return serviceFactory;   

}]);