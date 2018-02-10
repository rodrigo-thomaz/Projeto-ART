'use strict';
app.factory('deviceSerialService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSerialFinder', 'deviceSerialConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSerialFinder, deviceSerialConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setEnabledCompletedSubscription = null;

        var setEnabled = function (deviceSerialId, deviceId, deviceDatasheetId, enabled) {
            var data = {
                deviceSerialId: deviceSerialId,
                deviceId: deviceId,
                deviceDatasheetId: deviceDatasheetId,
                enabled: enabled,
            }
            return $http.post(serviceBase + deviceSerialConstant.setEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setEnabledCompletedSubscription = stompService.subscribeAllViews(deviceSerialConstant.setEnabledCompletedTopic, onSetEnabledCompleted);
        }

        var onSetEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSerial = deviceSerialFinder.getByKey(result.deviceSerialId, result.deviceId, result.deviceDatasheetId);
            deviceSerial.enabled = result.enabled;
            deviceContext.$digest();
            $rootScope.$emit(deviceSerialConstant.setEnabledCompletedEventName + result.deviceSerialId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setEnabledCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setEnabled = setEnabled;

        return serviceFactory;

    }]);