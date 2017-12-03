'use strict';
app.factory('dsFamilyTempSensorService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'unitMeasurementService', 'unitMeasurementConverter', 'espDeviceService', function ($http, $log, $rootScope, ngAuthSettings, stompService, unitMeasurementService, unitMeasurementConverter, espDeviceService) {

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

    var setResolution = function (dsFamilyTempSensorId, dsFamilyTempSensorResolutionId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            dsFamilyTempSensorResolutionId: dsFamilyTempSensorResolutionId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setResolution', data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {

        stompService.subscribeAllViews('DSFamilyTempSensor.SetResolutionViewCompleted', onSetResolutionCompleted);
                
        if (!initialized) {
            initialized = true;            
        }
    }      

    var onSetResolutionCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getById(result.deviceId, result.dsFamilyTempSensorId);
        sensor.dsFamilyTempSensorResolutionId = result.dsFamilyTempSensorResolutionId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + result.dsFamilyTempSensorId, result);
    }     

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory

    serviceFactory.getById = getById;
    serviceFactory.setResolution = setResolution;
    
    return serviceFactory;

}]);