'use strict';
app.factory('sensorTempDSFamilyService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'unitMeasurementService', 'unitMeasurementConverter', 'espDeviceService', function ($http, $log, $rootScope, ngAuthSettings, stompService, unitMeasurementService, unitMeasurementConverter, espDeviceService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};   

    var getByKey = function (deviceBaseId, sensorTempDSFamilyId) {
        var device = espDeviceService.getDeviceById(deviceBaseId);
        for (var i = 0; i < device.sensors.length; i++) {
            var sensor = device.sensors[i];
            if (sensor.sensorTempDSFamilyId === sensorTempDSFamilyId) {
                return sensor;
            }
        }
    };

    var setResolution = function (sensorTempDSFamilyId, sensorTempDSFamilyResolutionId) {
        var data = {
            sensorTempDSFamilyId: sensorTempDSFamilyId,
            sensorTempDSFamilyResolutionId: sensorTempDSFamilyResolutionId,
        }
        return $http.post(serviceBase + 'api/sensorTempDSFamily/setResolution', data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {

        stompService.subscribeAllViews('SensorTempDSFamily.SetResolutionViewCompleted', onSetResolutionCompleted);
                
        if (!initialized) {
            initialized = true;            
        }
    }      

    var onSetResolutionCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getByKey(result.deviceId, result.sensorTempDSFamilyId);
        sensor.sensorTempDSFamilyResolutionId = result.sensorTempDSFamilyResolutionId;
        $rootScope.$emit('sensorTempDSFamilyService_onSetResolutionCompleted_Id_' + result.sensorTempDSFamilyId, result);
    }     

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory

    serviceFactory.getByKey = getByKey;
    serviceFactory.setResolution = setResolution;
    
    return serviceFactory;

}]);