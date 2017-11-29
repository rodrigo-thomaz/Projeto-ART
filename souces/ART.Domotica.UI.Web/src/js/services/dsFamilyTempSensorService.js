'use strict';
app.factory('dsFamilyTempSensorService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'espDeviceService', function ($http, $log, $rootScope, ngAuthSettings, stompService, espDeviceService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};   

    var getById = function (deviceBaseId, dsFamilyTempSensorId) {
        var device = espDeviceService.getDeviceById(deviceBaseId);
        for (var i = 0; i < device.sensors.length; i++) {
            var sensor = device.sensors[i];
            if (sensor.dsFamilyTempSensorId === dsFamilyTempSensorId) {
                return sensor;
            }
        }
    };

    var setUnitOfMeasurement = function (dsFamilyTempSensorId, unitOfMeasurementId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            unitOfMeasurementId: unitOfMeasurementId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setUnitOfMeasurement', data).then(function (results) {
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

    var onConnected = function () {

        stompService.subscribeAllViews('DSFamilyTempSensor.SetUnitOfMeasurementViewCompleted', onSetUnitOfMeasurementCompleted);
        stompService.subscribeAllViews('DSFamilyTempSensor.SetResolutionViewCompleted', onSetResolutionCompleted);
        stompService.subscribeAllViews('DSFamilyTempSensor.SetLabelViewCompleted', onSetLabelCompleted);
        
        if (!initialized) {
            initialized = true;            
        }
    }  

    var onSetUnitOfMeasurementCompleted = function (payload) {
        var result = JSON.parse(payload.body);        
        var sensor = getById(result.deviceId, result.dsFamilyTempSensorId);
        sensor.unitOfMeasurementId = result.unitOfMeasurementId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetUnitOfMeasurementCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }    

    var onSetResolutionCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getById(result.deviceId, result.dsFamilyTempSensorId);
        sensor.dsFamilyTempSensorResolutionId = result.dsFamilyTempSensorResolutionId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }

    var onSetLabelCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getById(result.deviceId, result.dsFamilyTempSensorId);
        sensor.label = result.label;
        $rootScope.$emit('dsFamilyTempSensorService_onSetLabelCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }    

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory

    serviceFactory.getById = getById;

    serviceFactory.setUnitOfMeasurement = setUnitOfMeasurement;
    serviceFactory.setResolution = setResolution;
    serviceFactory.setLabel = setLabel;   

    return serviceFactory;

}]);