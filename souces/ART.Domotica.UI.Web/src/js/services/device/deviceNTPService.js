'use strict';
app.factory('deviceNTPService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceNTPFinder', 'deviceNTPConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceNTPFinder, deviceNTPConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setTimeZoneCompletedSubscription = null;
        var setUpdateIntervalInMilliSecondCompletedSubscription = null;

        var setTimeZone = function (deviceNTPId, deviceDatasheetId, timeZoneId) {
            var data = {
                deviceNTPId: deviceNTPId,
                deviceDatasheetId: deviceDatasheetId,
                timeZoneId: timeZoneId,
            }
            return $http.post(serviceBase + deviceNTPConstant.setTimeZoneApiUri, data).then(function (results) {
                return results;
            });
        };

        var setUpdateIntervalInMilliSecond = function (deviceNTPId, updateIntervalInMilliSecond) {
            var data = {
                deviceNTPId: deviceNTPId,
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
            var deviceNTP = deviceNTPFinder.getByKey(result.deviceNTPId, result.deviceDatasheetId);
            deviceNTP.timeZoneId = result.timeZoneId;
            deviceContext.$digest();
            $rootScope.$emit(deviceNTPConstant.setTimeZoneCompletedEventName + result.deviceNTPId, result);
        };

        var onSetUpdateIntervalInMilliSecondCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceNTP = deviceNTPFinder.getByKey(result.deviceNTPId, result.deviceDatasheetId);
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