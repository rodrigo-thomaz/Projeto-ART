'use strict';
app.factory('deviceNTPService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceFinder', 'deviceNTPConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceFinder, deviceNTPConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setTimeZoneCompletedSubscription = null;
        var setUpdateIntervalInMilliSecondCompletedSubscription = null;

        var setTimeZone = function (deviceId, timeZoneId) {
            var data = {
                deviceId: deviceId,
                timeZoneId: timeZoneId,
            }
            return $http.post(serviceBase + deviceNTPConstant.setTimeZoneApiUri, data).then(function (results) {
                return results;
            });
        };

        var setUpdateIntervalInMilliSecond = function (deviceId, updateIntervalInMilliSecond) {
            var data = {
                deviceId: deviceId,
                updateIntervalInMilliSecond: updateIntervalInMilliSecond,
            }
            return $http.post(serviceBase + deviceNTPConstant.setUpdateIntervalInMilliSecondApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setTimeZoneCompletedSubscription = stompService.subscribeAllViews(deviceNTPConstant.setTimeZoneCompletedTopic, onSetTimeZoneCompleted);
            setUpdateIntervalInMilliSecondCompletedSubscription = stompService.subscribeAllViews(deviceNTPConstant.setUpdateIntervalInMilliSecondCompletedTopic, onSetUpdateIntervalInMilliSecondCompleted);
        }

        var onSetTimeZoneCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var device = deviceFinder.getByKey(result.deviceId);
            device.deviceNTP.timeZoneId = result.timeZoneId;
            $rootScope.$emit(deviceNTPConstant.setTimeZoneCompletedEventName + result.deviceId, result);
        };

        var onSetUpdateIntervalInMilliSecondCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var device = deviceFinder.getByKey(result.deviceId);
            device.updateIntervalInMilliSecond = result.updateIntervalInMilliSecond;
            $rootScope.$emit(deviceNTPConstant.setUpdateIntervalInMilliSecondCompletedEventName + result.deviceId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setTimeZoneCompletedSubscription.unsubscribe();
            setUpdateIntervalInMilliSecondCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setTimeZone = setTimeZone;
        serviceFactory.setUpdateIntervalInMilliSecond = setUpdateIntervalInMilliSecond;

        return serviceFactory;

    }]);