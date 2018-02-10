'use strict';
app.factory('deviceSerialService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSerialFinder', 'deviceSerialConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSerialFinder, deviceSerialConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setEnabledCompletedSubscription = null;
        var setPinCompletedSubscription = null;

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

        var setPin = function (deviceSerialId, deviceId, deviceDatasheetId, value, direction) {
            var data = {
                deviceSerialId: deviceSerialId,
                deviceId: deviceId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
                direction: direction,
            }
            return $http.post(serviceBase + deviceSerialConstant.setPinApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setEnabledCompletedSubscription = stompService.subscribeAllViews(deviceSerialConstant.setEnabledCompletedTopic, onSetEnabledCompleted);
            setPinCompletedSubscription = stompService.subscribeAllViews(deviceSerialConstant.setPinCompletedTopic, onSetPinCompleted);
        }

        var onSetEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSerial = deviceSerialFinder.getByKey(result.deviceSerialId, result.deviceId, result.deviceDatasheetId);
            deviceSerial.enabled = result.enabled;
            deviceContext.$digest();
            $rootScope.$emit(deviceSerialConstant.setEnabledCompletedEventName + result.deviceSerialId, result);
        }

        var onSetPinCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSerial = deviceSerialFinder.getByKey(result.deviceSerialId, result.deviceId, result.deviceDatasheetId);
            if(result.direction === 'Receive')
                deviceSerial.pinRX = result.value;
            else if(result.direction === 'Transmit')
                deviceSerial.pinTX = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceSerialConstant.setPinCompletedEventName + result.deviceSerialId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setEnabledCompletedSubscription.unsubscribe();
            setPinCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setEnabled = setEnabled;
        serviceFactory.setPin = setPin;

        return serviceFactory;

    }]);