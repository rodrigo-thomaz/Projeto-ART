'use strict';
app.factory('dsFamilyTempSensorService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'EventDispatcher', 'stompService', 'espDeviceService', function ($http, $log, $rootScope, ngAuthSettings, EventDispatcher, stompService, espDeviceService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};   
    
    var setScale = function (dsFamilyTempSensorId, temperatureScaleId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            temperatureScaleId: temperatureScaleId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setScale', data).then(function (results) {
            return results;
        });
    };

    var setResolution = function (dsFamilyTempSensorId, dsFamilyTempSensorResolutionId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            dsFamilyTempSensorResolutionId: dsFamilyTempSensorResolutionId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setResolution', data).then(function (results) {
            return results;
        });
    };

    var setAlarmOn = function (dsFamilyTempSensorId, alarmOn, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmOn: alarmOn,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmOn', data).then(function (results) {
            return results;
        });
    };

    var setAlarmCelsius = function (dsFamilyTempSensorId, alarmCelsius, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmCelsius: alarmCelsius,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmCelsius', data).then(function (results) {
            return results;
        });
    };

    var setAlarmBuzzerOn = function (dsFamilyTempSensorId, alarmBuzzerOn, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmBuzzerOn: alarmBuzzerOn,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmBuzzerOn', data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {

        stompService.client.subscribe('/topic/ARTPUBTEMP', onReadReceived);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetScaleViewCompleted', onSetScaleCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetResolutionViewCompleted', onSetResolutionCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmOnViewCompleted', SetAlarmOnCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmCelsiusViewCompleted', SetAlarmCelsiusCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmBuzzerOnViewCompleted', SetAlarmBuzzerOnCompleted);

        if (!initialized) {
            initialized = true;            
        }
    }  

    var onReadReceived = function (payload) {
        EventDispatcher.trigger('dsFamilyTempSensorService_onReadReceived', JSON.parse(payload.body));
    }

    var getSensorFromPayload = function (payloadObject) {     
        var device = espDeviceService.getDeviceById(payloadObject.deviceId);
        for (var i = 0; i < device.sensors.length; i++) {
            var sensor = device.sensors[i];
            if (sensor.dsFamilyTempSensorId === payloadObject.dsFamilyTempSensorId) {
                return sensor;
            }
        }
    };  

    var onSetScaleCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        sensor.temperatureScaleId = result.temperatureScaleId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }    

    var onSetResolutionCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        sensor.dsFamilyTempSensorResolutionId = result.dsFamilyTempSensorResolutionId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var SetAlarmOnCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if (result.position = 0)
            sensor.lowAlarm.alarmOn = result.alarmOn;
        else if (result.position = 1)
            sensor.highAlarm.alarmOn = result.alarmOn;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var SetAlarmCelsiusCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if(result.position = 0)
            sensor.lowAlarm.alarmCelsius = result.alarmCelsius;
        else if (result.position = 1)
            sensor.highAlarm.alarmCelsius = result.alarmCelsius;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmCelsiusCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var SetAlarmBuzzerOnCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if (result.position = 0)
            sensor.lowAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
        else if (result.position = 1)
            sensor.highAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
        $rootScope.$emit('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    EventDispatcher.on('stompService_onConnected', onConnected);         

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.setScale = setScale;
    serviceFactory.setResolution = setResolution;

    serviceFactory.setAlarmOn = setAlarmOn;
    serviceFactory.setAlarmCelsius = setAlarmCelsius;
    serviceFactory.setAlarmBuzzerOn = setAlarmBuzzerOn;    

    return serviceFactory;

}]);