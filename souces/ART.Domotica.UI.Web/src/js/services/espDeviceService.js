'use strict';
app.factory('espDeviceService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.GetListInApplicationCompleted', onGetListInApplicationCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.DeleteFromApplicationCompleted', onDeleteFromApplicationCompleted);
    }

    var getListInApplication = function () {
        return $http.post(serviceBase + 'api/espDevice/getListInApplication').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var deleteFromApplication = function (hardwaresInApplicationId) {
        var data = {
            hardwaresInApplicationId: hardwaresInApplicationId
        };
        return $http.post(serviceBase + 'api/espDevice/deleteFromApplication', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };  

    var onGetListInApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        EventDispatcher.trigger('espDeviceService_onGetListInApplicationCompleted', data);
    }

    var onDeleteFromApplicationCompleted = function (payload) {
        EventDispatcher.trigger('espDeviceService_onDeleteFromApplicationCompleted');
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.getListInApplication = getListInApplication;
    serviceFactory.deleteFromApplication = deleteFromApplication;

    return serviceFactory;

}]);