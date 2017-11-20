'use strict';
app.factory('stompService', ['$log', 'ngAuthSettings', '$rootScope', function ($log, ngAuthSettings, $rootScope) {
    
    var serviceFactory = {};    

    serviceFactory.session = null;

    var onConnected = function (frame) {

        serviceFactory.session = frame.headers.session;

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