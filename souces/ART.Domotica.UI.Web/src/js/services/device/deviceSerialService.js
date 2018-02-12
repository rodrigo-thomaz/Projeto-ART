'use strict';
app.factory('deviceSerialService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSerialFinder', 'deviceSerialConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSerialFinder, deviceSerialConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setEnabledCompletedSubscription = null;
        var setPinCompletedSubscription = null;

        var setEnabled = function (deviceTypeId, deviceDatasheetId, deviceId, deviceSerialId, enabled) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                deviceSerialId: deviceSerialId,
                enabled: enabled,
            }
            return $http.post(serviceBase + deviceSerialConstant.setEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setPin = function (deviceSerialId, value, direction) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                deviceSerialId: deviceSerialId,
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
            var deviceSerial = deviceSerialFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId, result.deviceSerialId);
            deviceSerial.enabled = result.enabled;
            deviceContext.$digest();
            $rootScope.$emit(deviceSerialConstant.setEnabledCompletedEventName + result.deviceSerialId, result);
        }

        var onSetPinCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSerial = deviceSerialFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId, result.deviceSerialId);
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