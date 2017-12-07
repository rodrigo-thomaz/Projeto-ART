'use strict';
app.factory('sensorMapper', ['$rootScope', 'sensorContext', 'sensorConstant', 'sensorFinder', 'sensorTempDSFamilyResolutionConstant', 'siContext', 'siFinder', 'unitMeasurementScaleConstant',
    function ($rootScope, sensorContext, sensorConstant, sensorFinder, sensorTempDSFamilyResolutionConstant, siContext, siFinder, unitMeasurementScaleConstant) {

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

    var mapper_SensorUnitMeasurementScale_UnitMeasurementScale_Init = false;
    var mapper_SensorUnitMeasurementScale_UnitMeasurementScale = function () {
        if (!mapper_SensorUnitMeasurementScale_UnitMeasurementScale_Init && sensorContext.sensorUnitMeasurementScaleLoaded && siContext.unitMeasurementScaleLoaded) {
            mapper_SensorUnitMeasurementScale_UnitMeasurementScale_Init = true;
            for (var i = 0; i < sensorContext.sensorUnitMeasurementScale.length; i++) {
                var sensorUnitMeasurementScale = sensorContext.sensorUnitMeasurementScale[i];
                var unitMeasurementScale = siFinder.getUnitMeasurementScaleByKey(sensorUnitMeasurementScale.unitMeasurementId, sensorUnitMeasurementScale.unitMeasurementTypeId, sensorUnitMeasurementScale.numericalScalePrefixId, sensorUnitMeasurementScale.numericalScaleTypeId);
                sensorUnitMeasurementScale.unitMeasurementScale = unitMeasurementScale;
                delete sensorUnitMeasurementScale.unitMeasurementId; // removendo a foreing key
                delete sensorUnitMeasurementScale.unitMeasurementTypeId; // removendo a foreing key
                delete sensorUnitMeasurementScale.numericalScalePrefixId; // removendo a foreing key
                delete sensorUnitMeasurementScale.numericalScaleTypeId; // removendo a foreing key
                if (unitMeasurementScale.sensorUnitMeasurementScales === undefined) {
                    unitMeasurementScale.sensorUnitMeasurementScales = [];
                }
                unitMeasurementScale.sensorUnitMeasurementScales.push(sensorUnitMeasurementScale);
            }
        }
    };  

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onSensorGetAllByApplicationIdCompleted = function (event, data) {
        sensorGetAllByApplicationIdCompletedSubscription();
        loadAll();
        mapper_SensorTempDSFamily_SensorTempDSFamilyResolution();
        mapper_SensorUnitMeasurementScale_UnitMeasurementScale();
    }   

    var onSensorTempDSFamilyResolutionGetAllCompleted = function (event, data) {
        mapper_SensorTempDSFamily_SensorTempDSFamilyResolution();
    } 

    var onUnitMeasurementScaleGetAllCompleted = function (event, data) {
        mapper_SensorUnitMeasurementScale_UnitMeasurementScale();
    } 

    var sensorGetAllByApplicationIdCompletedSubscription = $rootScope.$on(sensorConstant.getAllByApplicationIdCompletedEventName, onSensorGetAllByApplicationIdCompleted);
    var sensorTempDSFamilyResolutionGetAllCompletedSubscription = $rootScope.$on(sensorTempDSFamilyResolutionConstant.getAllCompletedEventName, onSensorTempDSFamilyResolutionGetAllCompleted);
    var unitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(unitMeasurementScaleConstant.getAllCompletedEventName, onUnitMeasurementScaleGetAllCompleted);

    $rootScope.$on('$destroy', function () {
        sensorTypeGetAllByApplicationIdCompletedSubscription();      
        sensorTempDSFamilyResolutionGetAllCompletedSubscription();
        sensorUnitMeasurementScaleGetAllCompletedSubscription();
    });

    // *** Events Subscriptions
    
    
    return serviceFactory;

}]);