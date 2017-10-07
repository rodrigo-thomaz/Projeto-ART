'use strict';
app.factory('applicationService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var initialized = false;

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-GetCompleted', onGetCompleted);
        if (!initialized) {
            initialized = true;
            get();
        }
    }   

    var get = function () {
        return $http.post(serviceBase + 'api/application/get').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetCompleted = function (payload) {        
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        serviceFactory.application = JSON.parse(dataUTF8);
    }

    EventDispatcher.on('stompService_onConnected', onConnected);               

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.application = {};  

    return serviceFactory;   

}]);