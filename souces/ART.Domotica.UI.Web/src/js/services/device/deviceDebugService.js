'use strict';
app.factory('deviceDebugService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceDebugFinder', 'deviceDebugConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceDebugFinder, deviceDebugConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setRemoteEnabledCompletedSubscription = null;
        var setResetCmdEnabledCompletedSubscription = null;
        var setSerialEnabledCompletedSubscription = null;
        var setShowColorsCompletedSubscription = null;
        var setShowDebugLevelCompletedSubscription = null;
        var setShowProfilerCompletedSubscription = null;
        var setShowTimeCompletedSubscription = null;

        var setRemoteEnabled = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setRemoteEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setResetCmdEnabled = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setResetCmdEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setSerialEnabled = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setSerialEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowColors = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowColorsApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowDebugLevel = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowDebugLevelApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowProfiler = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowProfilerApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowTime = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowTimeApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setRemoteEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setRemoteEnabledCompletedTopic, onSetRemoteEnabledCompleted);
            setResetCmdEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setResetCmdEnabledCompletedTopic, onSetResetCmdEnabledCompleted);
            setSerialEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setSerialEnabledCompletedTopic, onSetSerialEnabledCompleted);
            setShowColorsCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowColorsCompletedTopic, onSetShowColorsCompleted);
            setShowDebugLevelCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowDebugLevelCompletedTopic, onSetShowDebugLevelCompleted);
            setShowProfilerCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowProfilerCompletedTopic, onSetShowProfilerCompleted);
            setShowTimeCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowTimeCompletedTopic, onSetShowTimeCompleted);
        }

        var onSetRemoteEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.remoteEnabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setRemoteEnabledCompletedEventName + result.deviceId, result);
        };

        var onSetResetCmdEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.resetCmdEnabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setResetCmdEnabledCompletedEventName + result.deviceId, result);
        };

        var onSetSerialEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.serialEnabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setSerialEnabledCompletedEventName + result.deviceId, result);
        };

        var onSetShowColorsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.showColors = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowColorsCompletedEventName + result.deviceId, result);
        };

        var onSetShowDebugLevelCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.showDebugLevel = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowDebugLevelCompletedEventName + result.deviceId, result);
        };

        var onSetShowProfilerCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.showProfiler = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowProfilerCompletedEventName + result.deviceId, result);
        };

        var onSetShowTimeCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDebug.showTime = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowTimeCompletedEventName + result.deviceId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setRemoteEnabledCompletedSubscription.unsubscribe();
            setResetCmdEnabledCompletedSubscription.unsubscribe();
            setSerialEnabledCompletedSubscription.unsubscribe();
            setShowColorsCompletedSubscription.unsubscribe();
            setShowDebugLevelCompletedSubscription.unsubscribe();
            setShowProfilerCompletedSubscription.unsubscribe();
            setShowTimeCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setRemoteEnabled = setRemoteEnabled;
        serviceFactory.setResetCmdEnabled = setResetCmdEnabled;
        serviceFactory.setSerialEnabled = setSerialEnabled;
        serviceFactory.setShowColors = setShowColors;
        serviceFactory.setShowDebugLevel = setShowDebugLevel;
        serviceFactory.setShowProfiler = setShowProfiler;
        serviceFactory.setShowTime = setShowTime;

        return serviceFactory;

    }]);