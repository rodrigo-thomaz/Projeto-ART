'use strict';
app.factory('deviceNTPService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'espDeviceService', function ($http, $log, ngAuthSettings, $rootScope, stompService, espDeviceService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};    

    var setTimeZone = function (deviceId, timeZoneId) {
        var data = {
            deviceId: deviceId,
            timeZoneId: timeZoneId,
        }
        return $http.post(serviceBase + 'api/deviceNTP/setTimeZone', data).then(function (results) {
            return results;
        });
    };

    var setUpdateIntervalInMilliSecond = function (deviceId, updateIntervalInMilliSecond) {
        var data = {
            deviceId: deviceId,
            updateIntervalInMilliSecond: updateIntervalInMilliSecond,
        }
        return $http.post(serviceBase + 'api/deviceNTP/setUpdateIntervalInMilliSecond', data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {
        stompService.subscribeAllViews('DeviceNTP.SetTimeZoneViewCompleted', onSetTimeZoneCompleted);
        stompService.subscribeAllViews('DeviceNTP.SetUpdateIntervalInMilliSecondViewCompleted', onSetUpdateIntervalInMilliSecondCompleted);
    }

    var onSetTimeZoneCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var device = espDeviceService.getDeviceById(result.deviceId);
        device.deviceNTP.timeZoneId = result.timeZoneId;
        $rootScope.$emit('espDeviceService_onSetTimeZoneCompleted_Id_' + result.deviceId, result);
    };

    var onSetUpdateIntervalInMilliSecondCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var device = espDeviceService.getDeviceById(result.deviceId);
        device.updateIntervalInMilliSecond = result.updateIntervalInMilliSecond;
        $rootScope.$emit('espDeviceService_onSetUpdateIntervalInMilliSecondCompleted_Id_' + result.deviceId, result);
    }
    
    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory    

    serviceFactory.setTimeZone = setTimeZone;
    serviceFactory.setUpdateIntervalInMilliSecond = setUpdateIntervalInMilliSecond;

    return serviceFactory;

}]);