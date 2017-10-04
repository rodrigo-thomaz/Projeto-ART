'use strict';
app.factory('stompService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', function ($http, $log, ngAuthSettings, EventDispatcher) {
    
    var serviceFactory = {};    

    serviceFactory.session = null;

    var onConnected = function (frame) {

        serviceFactory.session = frame.headers.session;

        EventDispatcher.trigger('stompService_onConnected', frame);

        debug('connected in broker');
        debug('session: ' + frame.headers.session);        
    }

    var onError = function (frame) {

        serviceFactory.session = null;

        EventDispatcher.trigger('stompService_onError', frame);

        debug('Error connecting in broker');
    }

    var debug = function (str) {
        console.log(str);
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

    serviceFactory.client = client;

    return serviceFactory;   

}]);