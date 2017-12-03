'use strict';
app.factory('espDeviceAdminService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, $log, ngAuthSettings, $rootScope, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.subscribe('ESPDeviceAdmin.GetAllViewCompleted', onGetAllCompleted);
    }

    var getAll = function () {
        return $http.post(serviceBase + 'api/espDevice/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        $rootScope.$emit('espDeviceAdminService_onGetAllCompleted', data);
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory

    serviceFactory.getAll = getAll;

    return serviceFactory; 

}]);