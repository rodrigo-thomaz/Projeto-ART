'use strict';
app.factory('sensorsInDeviceService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 
    function ($http, ngAuthSettings, $rootScope, stompService) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var onConnected = function () {

        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        return serviceFactory;

    }]);