'use strict';
app.factory('deviceNTPService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceNTPFinder', 'deviceNTPConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceNTPFinder, deviceNTPConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setTimeZoneCompletedSubscription = null;
        var setUpdateIntervalInMilliSecondCompletedSubscription = null;

        var setTimeZone = function (deviceTypeId, deviceDatasheetId, deviceId, timeZoneId) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                timeZoneId: timeZoneId,
            }
            return $http.post(serviceBase + deviceNTPConstant.setTimeZoneApiUri, data).then(function (results) {
                return results;
            });
        };

        var setUpdateIntervalInMilliSecond = function (deviceTypeId, deviceDatasheetId, deviceId, updateIntervalInMilliSecond) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
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
            var deviceNTP = deviceNTPFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceNTP.timeZoneId = result.timeZoneId;
            deviceContext.$digest();
            $rootScope.$emit(deviceNTPConstant.setTimeZoneCompletedEventName + result.deviceNTPId, result);
        };

        var onSetUpdateIntervalInMilliSecondCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceNTP = deviceNTPFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceNTP.updateIntervalInMilliSecond = result.updateIntervalInMilliSecond;
            deviceContext.$digest();
            $rootScope.$emit(deviceNTPConstant.setUpdateIntervalInMilliSecondCompletedEventName + result.deviceNTPId, result);
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