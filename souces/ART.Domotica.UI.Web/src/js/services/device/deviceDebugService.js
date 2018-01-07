'use strict';
app.factory('deviceDebugService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceDebugFinder', 'deviceDebugConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceDebugFinder, deviceDebugConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setTelnetTCPPortCompletedSubscription = null;
        var setRemoteEnabledCompletedSubscription = null;
        var setResetCmdEnabledCompletedSubscription = null;
        var setSerialEnabledCompletedSubscription = null;
        var setShowColorsCompletedSubscription = null;
        var setShowDebugLevelCompletedSubscription = null;
        var setShowProfilerCompletedSubscription = null;
        var setShowTimeCompletedSubscription = null;

        var setTelnetTCPPort = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setTelnetTCPPortApiUri, data).then(function (results) {
                return results;
            });
        };

        var setRemoteEnabled = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setRemoteEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setResetCmdEnabled = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setResetCmdEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setSerialEnabled = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setSerialEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowColors = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowColorsApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowDebugLevel = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowDebugLevelApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowProfiler = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowProfilerApiUri, data).then(function (results) {
                return results;
            });
        };

        var setShowTime = function (deviceDebugId, deviceDatasheetId, value) {
            var data = {
                deviceDebugId: deviceDebugId,
                deviceDatasheetId: deviceDatasheetId,
                value: value,
            }
            return $http.post(serviceBase + deviceDebugConstant.setShowTimeApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setTelnetTCPPortCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setTelnetTCPPortCompletedTopic, onSetTelnetTCPPortCompleted);
            setRemoteEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setRemoteEnabledCompletedTopic, onSetRemoteEnabledCompleted);
            setResetCmdEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setResetCmdEnabledCompletedTopic, onSetResetCmdEnabledCompleted);
            setSerialEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setSerialEnabledCompletedTopic, onSetSerialEnabledCompleted);
            setShowColorsCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowColorsCompletedTopic, onSetShowColorsCompleted);
            setShowDebugLevelCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowDebugLevelCompletedTopic, onSetShowDebugLevelCompleted);
            setShowProfilerCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowProfilerCompletedTopic, onSetShowProfilerCompleted);
            setShowTimeCompletedSubscription = stompService.subscribeAllViews(deviceDebugConstant.setShowTimeCompletedTopic, onSetShowTimeCompleted);
        }

        var onSetTelnetTCPPortCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.telnetTCPPort = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setTelnetTCPPortCompletedEventName + result.deviceDebugId, result);
        };

        var onSetRemoteEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.remoteEnabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setRemoteEnabledCompletedEventName + result.deviceDebugId, result);
        };

        var onSetResetCmdEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.resetCmdEnabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setResetCmdEnabledCompletedEventName + result.deviceDebugId, result);
        };

        var onSetSerialEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.serialEnabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setSerialEnabledCompletedEventName + result.deviceDebugId, result);
        };

        var onSetShowColorsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.showColors = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowColorsCompletedEventName + result.deviceDebugId, result);
        };

        var onSetShowDebugLevelCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.showDebugLevel = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowDebugLevelCompletedEventName + result.deviceDebugId, result);
        };

        var onSetShowProfilerCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.showProfiler = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowProfilerCompletedEventName + result.deviceDebugId, result);
        };

        var onSetShowTimeCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDebug = deviceDebugFinder.getByKey(result.deviceDebugId, result.deviceDatasheetId);
            deviceDebug.showTime = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDebugConstant.setShowTimeCompletedEventName + result.deviceDebugId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setTelnetTCPPortCompletedSubscription.unsubscribe();
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

        serviceFactory.setTelnetTCPPort = setTelnetTCPPort;
        serviceFactory.setRemoteEnabled = setRemoteEnabled;
        serviceFactory.setResetCmdEnabled = setResetCmdEnabled;
        serviceFactory.setSerialEnabled = setSerialEnabled;
        serviceFactory.setShowColors = setShowColors;
        serviceFactory.setShowDebugLevel = setShowDebugLevel;
        serviceFactory.setShowProfiler = setShowProfiler;
        serviceFactory.setShowTime = setShowTime;

        return serviceFactory;

    }]);