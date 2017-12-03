'use strict';
app.factory('stompService', ['$log', 'ngAuthSettings', '$rootScope', 'applicationMQ', function ($log, ngAuthSettings, $rootScope, applicationMQ) {

    var serviceFactory = {};    

    var connectedEventName = 'stompService.onConnected';
    var errorEventName = 'stompService.onError';
    
    var onConnected = function (frame) {
        $rootScope.$emit(connectedEventName, frame);
    }

    var onError = function (frame) {
        $rootScope.$emit(errorEventName, frame);
    }

    var subscribe = function (topic, callback) {
        return client.subscribe(generateStringTopic(topic), callback);
    };

    var subscribeAllViews = function (topic, callback) {
        return client.subscribe(generateStringTopicAllViews(topic), callback);
    };

    var unsubscribe = function (subscriptionId) {
        return client.unsubscribe(subscriptionId);
    };    

    var generateStringTopic = function (topic) {
        //https://rabbitmq.docs.pivotal.io/36/rabbit-web-docs/stomp.html        
        //return '/exchange/amq.topic/ART.Application.' + applicationMQ.applicationTopic + '.WebUI.' + applicationMQ.webUITopic + '.' + topic;
        return '/topic/ART.Application.' + applicationMQ.applicationTopic + '.WebUI.' + applicationMQ.webUITopic + '.' + topic;
    }

    var generateStringTopicAllViews = function (topic) {
        //https://rabbitmq.docs.pivotal.io/36/rabbit-web-docs/stomp.html        
        //return '/exchange/amq.topic/ART.Application.' + applicationMQ.applicationTopic + '.WebUI.' + topic;
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

    serviceFactory.connectedEventName = connectedEventName;
    serviceFactory.errorEventName = errorEventName;

    serviceFactory.subscribe = subscribe;
    serviceFactory.subscribeAllViews = subscribeAllViews;
    serviceFactory.unsubscribe = unsubscribe;
    serviceFactory.connected = connected;
    
    return serviceFactory;   

}]);