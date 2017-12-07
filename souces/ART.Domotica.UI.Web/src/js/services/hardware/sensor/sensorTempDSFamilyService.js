'use strict';
app.factory('sensorTempDSFamilyService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'sensorFinder', 'sensorTempDSFamilyConstant', 'unitMeasurementService', 'unitMeasurementConverter', 'deviceService', function ($http, $log, $rootScope, ngAuthSettings, stompService, sensorFinder, sensorTempDSFamilyConstant, unitMeasurementService, unitMeasurementConverter, deviceService) {

    var serviceFactory = {};   

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;    

    var setResolutionCompletedSubscription = null;

    var setResolution = function (sensorTempDSFamilyId, sensorTempDSFamilyResolutionId) {
        var data = {
            sensorTempDSFamilyId: sensorTempDSFamilyId,
            sensorTempDSFamilyResolutionId: sensorTempDSFamilyResolutionId,
        }
        return $http.post(serviceBase + sensorTempDSFamilyConstant.setResolutionApiUri, data).then(function (results) {
            return results;
        });
    };

    var onConnected = function () {

        setResolutionCompletedSubscription = stompService.subscribeAllViews(sensorTempDSFamilyConstant.setResolutionCompletedTopic, onSetResolutionCompleted);
                
        if (!initialized) {
            initialized = true;            
        }
    }      

    var onSetResolutionCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = sensorFinder.getSensorByKey(result.sensorTempDSFamilyId);
        sensor.sensorTempDSFamilyResolutionId = result.sensorTempDSFamilyResolutionId;
        $rootScope.$emit(sensorTempDSFamilyConstant.setResolutionCompletedEventName + result.sensorTempDSFamilyId, result);
    }     

    $rootScope.$on('$destroy', function () {
        clearOnConnected();

        setResolutionCompletedSubscription();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected); 

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory

    serviceFactory.setResolution = setResolution;
    
    return serviceFactory;

}]);