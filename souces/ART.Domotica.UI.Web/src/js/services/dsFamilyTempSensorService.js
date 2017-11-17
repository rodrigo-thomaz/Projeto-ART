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

    var setAlarmValue = function (dsFamilyTempSensorId, alarmValue, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmValue: alarmValue,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmValue', data).then(function (results) {
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
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmValueViewCompleted', SetAlarmValueCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmBuzzerOnViewCompleted', SetAlarmBuzzerOnCompleted);

        if (!initialized) {
            initialized = true;            
        }
    }  

    var onReadReceived = function (payload) {
        EventDispatcher.trigger('dsFamilyTempSensorService_onReadReceived', JSON.parse(payload.body));
    }

    var onSetScaleCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorById(result.dsFamilyTempSensorId);
        sensor.temperatureScaleId = result.temperatureScaleId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var getSensorById = function (dsFamilyTempSensorId) {
        for (var i = 0; i < espDeviceService.devices.length; i++) {
            var device = espDeviceService.devices[i];
            for (var j = 0; j < device.sensors.length; j++) {
                var sensor = device.sensors[j];
                if (sensor.dsFamilyTempSensorId === dsFamilyTempSensorId) {
                    return sensor;
                }
            }            
        }
    };  

    var onSetResolutionCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var SetAlarmOnCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var SetAlarmValueCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmValueCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var SetAlarmBuzzerOnCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    EventDispatcher.on('stompService_onConnected', onConnected);         

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.setScale = setScale;
    serviceFactory.setResolution = setResolution;

    serviceFactory.setAlarmOn = setAlarmOn;
    serviceFactory.setAlarmValue = setAlarmValue;
    serviceFactory.setAlarmBuzzerOn = setAlarmBuzzerOn;    

    return serviceFactory;

}]);