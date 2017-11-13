'use strict';
app.factory('espDeviceAdminService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDeviceAdmin.GetAllViewCompleted', onGetAllCompleted);
    }

    var getAll = function () {
        return $http.post(serviceBase + 'api/espDevice/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        EventDispatcher.trigger('espDeviceAdminService_onGetAllCompleted', data);        
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.getAll = getAll;

    return serviceFactory; 

}]);