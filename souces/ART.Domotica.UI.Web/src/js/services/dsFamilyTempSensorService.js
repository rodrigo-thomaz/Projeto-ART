'use strict';
app.factory('dsFamilyTempSensorService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'espDeviceService', function ($http, $log, $rootScope, ngAuthSettings, stompService, espDeviceService) {

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

    var setLabel = function (dsFamilyTempSensorId, label) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            label: label,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setLabel', data).then(function (results) {
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

    var setChartLimiterCelsius = function (dsFamilyTempSensorId, chartLimiterCelsius, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            chartLimiterCelsius: chartLimiterCelsius,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setChartLimiterCelsius', data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {

        stompService.subscribe('DSFamilyTempSensor.SetScaleViewCompleted', onSetScaleCompleted);
        stompService.subscribe('DSFamilyTempSensor.SetResolutionViewCompleted', onSetResolutionCompleted);
        stompService.subscribe('DSFamilyTempSensor.SetLabelViewCompleted', onSetLabelCompleted);
        stompService.subscribe('DSFamilyTempSensor.SetAlarmOnViewCompleted', onSetAlarmOnCompleted);
        stompService.subscribe('DSFamilyTempSensor.SetAlarmCelsiusViewCompleted', onSetAlarmCelsiusCompleted);
        stompService.subscribe('DSFamilyTempSensor.SetAlarmBuzzerOnViewCompleted', onSetAlarmBuzzerOnCompleted);
        stompService.subscribe('DSFamilyTempSensor.SetChartLimiterCelsiusViewCompleted', onSetChartLimiterCelsiusCompleted);

        if (!initialized) {
            initialized = true;            
        }
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

    var onSetLabelCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        sensor.label = result.label;
        $rootScope.$emit('dsFamilyTempSensorService_onSetLabelCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetAlarmOnCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if (result.position === 'Low')
            sensor.lowAlarm.alarmOn = result.alarmOn;
        else if (result.position === 'High')
            sensor.highAlarm.alarmOn = result.alarmOn;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetAlarmCelsiusCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if (result.position === 'Low')
            sensor.lowAlarm.alarmCelsius = result.alarmCelsius;
        else if (result.position === 'High')
            sensor.highAlarm.alarmCelsius = result.alarmCelsius;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmCelsiusCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetAlarmBuzzerOnCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if (result.position === 'Low')
            sensor.lowAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
        else if (result.position === 'High')
            sensor.highAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
        $rootScope.$emit('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetChartLimiterCelsiusCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getSensorFromPayload(result);
        if (result.position === 'Low')
            sensor.lowChartLimiterCelsius = result.chartLimiterCelsius;
        else if (result.position === 'High')
            sensor.highChartLimiterCelsius = result.chartLimiterCelsius;
        $rootScope.$emit('dsFamilyTempSensorService_SetChartLimiterCelsiusCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.setScale = setScale;
    serviceFactory.setResolution = setResolution;
    serviceFactory.setLabel = setLabel;

    serviceFactory.setAlarmOn = setAlarmOn;
    serviceFactory.setAlarmCelsius = setAlarmCelsius;
    serviceFactory.setAlarmBuzzerOn = setAlarmBuzzerOn;    

    serviceFactory.setChartLimiterCelsius = setChartLimiterCelsius;    

    return serviceFactory;

}]);