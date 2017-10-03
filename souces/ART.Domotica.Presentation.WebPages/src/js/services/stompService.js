'use strict';
app.factory('stompService', ['$http', '$log', 'ngAuthSettings', function ($http, $log, ngAuthSettings) {
    
    var serviceFactory = {};    
    
    var onConnected = function (frame) {
        if (serviceFactory.onConnected != null) {
            serviceFactory.onConnected();
        }
        debug('connected in broker');
    }

    var debug = function (str) {
        console.log(str);
    };

    // stomp

    var wsBrokerHostName = ngAuthSettings.wsBrokerHostName;
    var wsBrokerPort = ngAuthSettings.wsBrokerPort;

    var url = 'ws://' + wsBrokerHostName + ':' + wsBrokerPort + '/ws';

    var client = Stomp.client(url);

    client.connect('test', 'test', onConnected);    

    // serviceFactory

    serviceFactory.onConnected = null;
    serviceFactory.client = client;

    return serviceFactory;   

}]);