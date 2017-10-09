'use strict';
app.factory('hardwaresInApplicationService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.GetListCompleted', onGetListCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.DeleteHardwareCompleted', onDeleteHardwareCompleted);
    }

    var getList = function () {
        return $http.post(serviceBase + 'api/hardwaresInApplication/getList').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var deleteHardware = function (hardwaresInApplicationId) {
        var data = {
            hardwaresInApplicationId: hardwaresInApplicationId
        };
        return $http.post(serviceBase + 'api/hardwaresInApplication/deleteHardware', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };  

    var onGetListCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        EventDispatcher.trigger('hardwaresInApplicationService_onGetListCompleted', data);
    }

    var onDeleteHardwareCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        EventDispatcher.trigger('hardwaresInApplicationService_onDeleteHardwareReceived', data);
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.getList = getList;
    serviceFactory.deleteHardware = deleteHardware;

    return serviceFactory;

}]);