'use strict';
app.factory('deviceSensorsService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSensorsConstant', 'deviceSensorsFinder',
    function ($http, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSensorsConstant, deviceSensorsFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setPublishIntervalInSecondsCompletedSubscription = null;

        var setPublishIntervalInSeconds = function (deviceSensorsId, publishIntervalInSeconds) {
            var data = {
                deviceSensorsId: deviceSensorsId,
                publishIntervalInSeconds: publishIntervalInSeconds,
            }
            return $http.post(serviceBase + deviceSensorsConstant.setPublishIntervalInSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setPublishIntervalInSecondsCompletedSubscription = stompService.subscribeAllViews(deviceSensorsConstant.setPublishIntervalInSecondsCompletedTopic, onSetPublishIntervalInSecondsCompleted);
        }

        var onSetPublishIntervalInSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSensors = deviceSensorsFinder.getByKey(result.deviceSensorsId, result.deviceDatasheetId);
            deviceSensors.publishIntervalInSeconds = result.publishIntervalInSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorsConstant.setPublishIntervalInSecondsCompletedEventName + result.deviceSensorsId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setPublishIntervalInSecondsCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setPublishIntervalInSeconds = setPublishIntervalInSeconds;

        return serviceFactory;

    }]);