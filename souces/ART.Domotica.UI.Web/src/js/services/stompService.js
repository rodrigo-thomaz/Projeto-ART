'use strict';
app.factory('stompService', ['$log', 'ngAuthSettings', '$rootScope', 'mainContext', function ($log, ngAuthSettings, $rootScope, mainContext) {

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
        //return client.subscribe(generateStringTopic(topic), callback);
        return client.subscribe(generateStringTopic(topic), callback, { id: topic });
    };

    var subscribeAllViews = function (topic, callback) {
        return client.subscribe(generateStringTopicAllViews(topic), callback, { id: topic });
    };

    var unsubscribe = function (subscriptionId) {
        return client.unsubscribe(subscriptionId);
    };    

    var generateStringTopic = function (topic) {
        //https://rabbitmq.docs.pivotal.io/36/rabbit-web-docs/stomp.html        
        //return '/exchange/amq.topic/ART.Application.' + mainContext.applicationMQ.applicationTopic + '.WebUI.' + mainContext.applicationMQ.webUITopic + '.' + topic;
        return '/topic/ART.Application.' + mainContext.applicationMQ.applicationTopic + '.WebUI.' + mainContext.applicationMQ.webUITopic + '.' + topic;
    }

    var generateStringTopicAllViews = function (topic) {
        //https://rabbitmq.docs.pivotal.io/36/rabbit-web-docs/stomp.html        
        //return '/exchange/amq.topic/ART.Application.' + mainContext.applicationMQ.applicationTopic + '.WebUI.' + topic;
        return '/topic/ART.Application.' + mainContext.applicationMQ.applicationTopic + '.WebUI.' + topic;
    }

    var connected = function () {
        return client.connected;
    };

    var connect = function (event, data) {

        var headers = {
            login: mainContext.applicationMQ.user,
            passcode: mainContext.applicationMQ.password,
            // additional header
            //'client-id': 'my-client-id'
        };

        client = Stomp.client(url);

        serviceFactory.client = client;         

        client.connect(headers, onConnected, onError);    
    };  

    var wsBrokerHostName = ngAuthSettings.wsBrokerHostName;
    var wsBrokerPort = ngAuthSettings.wsBrokerPort;       

    var url = 'ws://' + wsBrokerHostName + ':' + wsBrokerPort + '/ws';

    var client = Stomp.client(url);

    connect();
        
    // serviceFactory    

    serviceFactory.connectedEventName = connectedEventName;
    serviceFactory.errorEventName = errorEventName;

    serviceFactory.subscribe = subscribe;
    serviceFactory.subscribeAllViews = subscribeAllViews;
    serviceFactory.unsubscribe = unsubscribe;
    serviceFactory.connected = connected;
    
    return serviceFactory;   

}]);