'use strict';
app.factory('hardwaresInApplicationJoinService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.SearchPinCompleted', onSearchPinCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.InsertHardwareCompleted', onInsertHardwareCompleted);        
    }

    var searchPin = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/hardwaresInApplication/searchPin', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };  

    var insertHardware = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/hardwaresInApplication/insertHardware', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var onSearchPinCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);        
        EventDispatcher.trigger('hardwaresInApplicationService_onSearchPinReceived', data);
    }

    var onInsertHardwareCompleted = function (payload) {
        EventDispatcher.trigger('hardwaresInApplicationService_onInsertHardwareReceived');
    }    

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.searchPin = searchPin;
    serviceFactory.insertHardware = insertHardware;    

    return serviceFactory;

}]);