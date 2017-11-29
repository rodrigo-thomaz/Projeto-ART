'use strict';
app.factory('sensorTriggerService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'dsFamilyTempSensorService', function ($http, $log, $rootScope, ngAuthSettings, stompService, dsFamilyTempSensorService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};   
    
    var setAlarmOn = function (dsFamilyTempSensorId, alarmOn, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmOn: alarmOn,
            position: position,
        }
        return $http.post(serviceBase + 'api/sensorTrigger/setAlarmOn', data).then(function (results) {
            return results;
        });
    };

    var setAlarmCelsius = function (dsFamilyTempSensorId, alarmCelsius, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmCelsius: alarmCelsius,
            position: position,
        }
        return $http.post(serviceBase + 'api/sensorTrigger/setAlarmCelsius', data).then(function (results) {
            return results;
        });
    };

    var setAlarmBuzzerOn = function (dsFamilyTempSensorId, alarmBuzzerOn, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmBuzzerOn: alarmBuzzerOn,
            position: position,
        }
        return $http.post(serviceBase + 'api/sensorTrigger/setAlarmBuzzerOn', data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {

        stompService.subscribeAllViews('SensorTrigger.SetAlarmOnViewCompleted', onSetAlarmOnCompleted);
        stompService.subscribeAllViews('SensorTrigger.SetAlarmCelsiusViewCompleted', onSetAlarmCelsiusCompleted);
        stompService.subscribeAllViews('SensorTrigger.SetAlarmBuzzerOnViewCompleted', onSetAlarmBuzzerOnCompleted);

        if (!initialized) {
            initialized = true;            
        }
    }    

    var onSetAlarmOnCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = dsFamilyTempSensorService.getById(result.deviceId, result.dsFamilyTempSensorId);
        if (result.position === 'Low')
            sensor.lowAlarm.alarmOn = result.alarmOn;
        else if (result.position === 'High')
            sensor.highAlarm.alarmOn = result.alarmOn;
        $rootScope.$emit('sensorTriggerService_onSetAlarmOnCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetAlarmCelsiusCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = dsFamilyTempSensorService.getById(result.deviceId, result.dsFamilyTempSensorId);
        if (result.position === 'Low')
            sensor.lowAlarm.alarmCelsius = result.alarmCelsius;
        else if (result.position === 'High')
            sensor.highAlarm.alarmCelsius = result.alarmCelsius;
        $rootScope.$emit('sensorTriggerService_onSetAlarmCelsiusCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetAlarmBuzzerOnCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = dsFamilyTempSensorService.getById(result.deviceId, result.dsFamilyTempSensorId);
        if (result.position === 'Low')
            sensor.lowAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
        else if (result.position === 'High')
            sensor.highAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
        $rootScope.$emit('sensorTriggerService_SetAlarmBuzzerOnCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory

    serviceFactory.setAlarmOn = setAlarmOn;
    serviceFactory.setAlarmCelsius = setAlarmCelsius;
    serviceFactory.setAlarmBuzzerOn = setAlarmBuzzerOn;    

    return serviceFactory;

}]);