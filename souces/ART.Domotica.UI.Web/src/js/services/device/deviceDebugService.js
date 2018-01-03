'use strict';
app.factory('deviceDebugService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceDebugFinder', 'deviceDebugConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceDebugFinder, deviceDebugConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setActiveCompletedSubscription = null;

        var setActive = function (deviceDebugId, deviceDatasheetId, active) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                active: active,
            }
            return $http.post(serviceBase + deviceDebugConstant.setActiveApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setActiveCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setActiveCompletedTopic, onSetActiveCompleted);
        }

        var onSetActiveCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.active = result.active;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setActiveCompletedEventName + result.deviceDebugId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setActiveCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setActive = setActive;

        return serviceFactory;

    }]);