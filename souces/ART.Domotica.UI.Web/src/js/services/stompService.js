'use strict';
app.factory('stompService', ['$log', 'ngAuthSettings', '$rootScope', function ($log, ngAuthSettings, $rootScope) {
    
    var serviceFactory = {};    
    
    serviceFactory.session = null;    

    var onConnected = function (frame) {

        var clientTopic = getRandomInt(100000000, 999999999);
        serviceFactory.session = clientTopic;

        $rootScope.$emit('stompService_onConnected', frame);

        debug('connected in broker');
        debug('session: ' + frame.headers.session);        
    }

    var onError = function (frame) {

        serviceFactory.session = null;

        $rootScope.$emit('stompService_onError', frame);

        debug('Error connecting in broker');
    }

    var debug = function (str) {
        console.log(str);
    };

    var subscribe = function (topic, callback) {
        client.subscribe('/topic/ART.WebUI.' + serviceFactory.session + '.' + topic, callback);
    };

    var getRandomInt = function (min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min)) + min;
    };

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
    
    serviceFactory.client = client;

    return serviceFactory;   

}]);