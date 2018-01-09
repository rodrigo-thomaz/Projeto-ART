'use strict';
app.factory('deviceSensorsService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSensorsConstant', 'deviceSensorsFinder',
    function ($http, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSensorsConstant, deviceSensorsFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setPublishIntervalInMilliSecondsCompletedSubscription = null;

        var setPublishIntervalInMilliSeconds = function (deviceSensorsId, deviceDatasheetId, publishIntervalInMilliSeconds) {
            var data = {
                deviceSensorsId: deviceSensorsId,
                deviceDatasheetId: deviceDatasheetId,
                publishIntervalInMilliSeconds: publishIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceSensorsConstant.setPublishIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setPublishIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedTopic, onSetPublishIntervalInMilliSecondsCompleted);
        }

        var onSetPublishIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSensors = deviceSensorsFinder.getByKey(result.deviceSensorsId, result.deviceDatasheetId);
            deviceSensors.publishIntervalInMilliSeconds = result.publishIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedEventName + result.deviceSensorsId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setPublishIntervalInMilliSecondsCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setPublishIntervalInMilliSeconds = setPublishIntervalInMilliSeconds;

        return serviceFactory;

    }]);