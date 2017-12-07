'use strict';
app.factory('sensorMapper', ['$rootScope', 'sensorContext', 'sensorConstant',
    function ($rootScope, sensorContext, sensorConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

    var loadAll = function () {

        for (var i = 0; i < sensorContext.sensor.length; i++) {

            var sensor = sensorContext.sensor[i];

            var sensorTempDSFamily = sensor.sensorTempDSFamily;
            sensorTempDSFamily.sensor = sensor;
            sensorContext.sensorTempDSFamily.push(sensorTempDSFamily);

            var sensorUnitMeasurementScale = sensor.sensorUnitMeasurementScale;
            sensorUnitMeasurementScale.sensor = sensor;
            sensorContext.sensorUnitMeasurementScale.push(sensorUnitMeasurementScale);
            
            for (var j = 0; j < sensor.sensorTriggers.length; j++) {
                var sensorTrigger = sensor.sensorTriggers[j];
                sensorTrigger.sensor = sensor;
                delete sensorTrigger.sensorId; // removendo a foreing key
                sensorContext.sensorTrigger.push(sensorTrigger);
            }
        }

        sensorContext.deviceLoaded = true;
        sensorContext.sensorTriggerLoaded = true;
        sensorContext.sensorUnitMeasurementScaleLoaded = true;
        sensorContext.sensorTempDSFamilyLoaded = true;
        sensorContext.sensorTempDSFamilyResolutionLoaded = true;
    }


    var mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init = false;
    var mapper_SensorTempDSFamily_SensorTempDSFamilyResolution = function () {
        if (!mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init && sensorContext.sensorTempDSFamilyLoaded && sensorContext.sensorTempDSFamilyResolutionLoaded) {
            mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init = true;
            for (var i = 0; i < sensorContext.sensorTempDSFamily.length; i++) {
                var sensorTempDSFamily = sensorContext.sensorTempDSFamily[i];
                var sensorTempDSFamilyResolution = sensorFinder.getSensorTempDSFamilyResolutionByKey(sensorTempDSFamily.sensorTempDSFamilyResolutionId);
                sensorTempDSFamily.sensorTempDSFamilyResolution = sensorTempDSFamilyResolution;
                delete sensorTempDSFamily.sensorTempDSFamilyResolutionId; // removendo a foreing key
                if (sensorTempDSFamilyResolution.sensorTempDSFamilies === undefined) {
                    sensorTempDSFamilyResolution.sensorTempDSFamilies = [];
                }
                sensorTempDSFamilyResolution.sensorTempDSFamilies.push(sensorTempDSFamily);
            }
        }
    };  

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onSensorGetAllByApplicationIdCompleted = function (event, data) {
        sensorGetAllByApplicationIdCompletedSubscription();
        loadAll();
    }   

    var sensorGetAllByApplicationIdCompletedSubscription = $rootScope.$on(sensorConstant.getAllByApplicationIdCompletedEventName, onSensorGetAllByApplicationIdCompleted);

    $rootScope.$on('$destroy', function () {
        sensorTypeGetAllByApplicationIdCompletedSubscription();        
    });

    // *** Events Subscriptions
    
    
    return serviceFactory;

}]);