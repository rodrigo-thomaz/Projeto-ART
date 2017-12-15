'use strict';
app.factory('sensorInDeviceService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'sensorInDeviceConstant', 'sensorInDeviceFinder',
    function ($http, ngAuthSettings, $rootScope, stompService, sensorInDeviceConstant, sensorInDeviceFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setOrdinationCompletedSubscription = null;

        var setOrdination = function (deviceSensorsId, sensorId, sensorDatasheetId, sensorTypeId, ordination) {
            var data = {
                deviceSensorsId: deviceSensorsId,
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                ordination: ordination,
            }
            return $http.post(serviceBase + sensorInDeviceConstant.setOrdinationApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setOrdinationCompletedSubscription = stompService.subscribeAllViews(sensorInDeviceConstant.setOrdinationCompletedTopic, onSetOrdinationCompleted);
        }

        var onSetOrdinationCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorInDevice = sensorInDeviceFinder.getByKey(result.deviceSensorsId, result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            sensorInDevice.ordination = result.ordination;
            deviceContext.$digest();
            $rootScope.$emit(sensorInDeviceConstant.setOrdinationCompletedEventName + result.deviceSensorsId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setOrdinationCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setOrdination = setOrdination;

        return serviceFactory;

    }]);