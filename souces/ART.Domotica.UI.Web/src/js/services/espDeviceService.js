'use strict';
app.factory('espDeviceService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.GetListInApplicationCompleted', onGetListInApplicationCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.DeleteFromApplicationCompleted', onDeleteFromApplicationCompleted);
        if (!initialized) {
            initialized = true;
            getListInApplication();
        }
    }

    var getListInApplication = function () {
        return $http.post(serviceBase + 'api/espDevice/getListInApplication').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var deleteFromApplication = function (hardwareInApplicationId) {
        var data = {
            hardwareInApplicationId: hardwareInApplicationId
        };
        return $http.post(serviceBase + 'api/espDevice/deleteFromApplication', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };  

    var onGetListInApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            data[i].createDate = new Date(data[i].createDate * 1000).toLocaleString();
            serviceFactory.devices.push(data[i]);
        }
    }

    var onDeleteFromApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < serviceFactory.devices.length; i++) {
            if (serviceFactory.devices[i].hardwareInApplicationId == data.hardwareInApplicationId) {
                serviceFactory.devices.splice(i, 1);
            }
        }
        EventDispatcher.trigger('espDeviceService_onDeleteFromApplicationCompleted');
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.deleteFromApplication = deleteFromApplication;

    serviceFactory.devices = [];  

    return serviceFactory;

}]);