'use strict';
app.factory('deviceSensorsService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSensorsConstant', 'deviceSensorsFinder',
    function ($http, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSensorsConstant, deviceSensorsFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setReadIntervalInMilliSecondsCompletedSubscription = null;
        var setPublishIntervalInMilliSecondsCompletedSubscription = null;

        var setReadIntervalInMilliSeconds = function (deviceSensorsId, deviceDatasheetId, readIntervalInMilliSeconds) {
            var data = {
                deviceId: deviceSensorsId,
                deviceDatasheetId: deviceDatasheetId,
                intervalInMilliSeconds: readIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceSensorsConstant.setReadIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var setPublishIntervalInMilliSeconds = function (deviceSensorsId, deviceDatasheetId, publishIntervalInMilliSeconds) {
            var data = {
                deviceId: deviceSensorsId,
                deviceDatasheetId: deviceDatasheetId,
                intervalInMilliSeconds: publishIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceSensorsConstant.setPublishIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setReadIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceSensorsConstant.setReadIntervalInMilliSecondsCompletedTopic, onSetReadIntervalInMilliSecondsCompleted);
            setPublishIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedTopic, onSetPublishIntervalInMilliSecondsCompleted);
        }

        var onSetReadIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSensors = deviceSensorsFinder.getByKey(result.deviceSensorsId, result.deviceDatasheetId);
            deviceSensors.readIntervalInMilliSeconds = result.readIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorsConstant.setReadIntervalInMilliSecondsCompletedEventName + result.deviceSensorsId, result);
        };

        var onSetPublishIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSensors = deviceSensorsFinder.getByKey(result.deviceId, result.deviceDatasheetId);
            deviceSensors.publishIntervalInMilliSeconds = result.publishIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedEventName + result.deviceId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setReadIntervalInMilliSecondsCompletedSubscription.unsubscribe();
            setPublishIntervalInMilliSecondsCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setReadIntervalInMilliSeconds = setReadIntervalInMilliSeconds;
        serviceFactory.setPublishIntervalInMilliSeconds = setPublishIntervalInMilliSeconds;

        return serviceFactory;

    }]);