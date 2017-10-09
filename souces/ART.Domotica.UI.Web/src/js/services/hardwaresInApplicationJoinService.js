'use strict';
app.factory('hardwaresInApplicationJoinService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.SearchPinCompleted', onSearchPinCompleted);
    }

    var searchPin = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/hardwaresInApplication/searchPin', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };  

    var onSearchPinCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);        
        EventDispatcher.trigger('hardwaresInApplicationService_onSearchPinReceived', data);
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.searchPin = searchPin;

    return serviceFactory;

}]);