'use strict';
app.factory('sensorMapper', ['$rootScope', 'sensorContext', 'sensorConstant', 'sensorFinder', 'siContext', 'siFinder',
    function ($rootScope, sensorContext, sensorConstant, sensorFinder, siContext, siFinder) {

        var serviceFactory = {};

        sensorContext.$watchCollection('sensor', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var sensor = newValues[i];
                //sensorTempDSFamily
                var sensorTempDSFamily = sensor.sensorTempDSFamily;
                sensorTempDSFamily.sensor = sensor;
                sensorContext.sensorTempDSFamily.push(sensorTempDSFamily);
                //sensorUnitMeasurementScale
                var sensorUnitMeasurementScale = sensor.sensorUnitMeasurementScale;
                sensorUnitMeasurementScale.sensor = sensor;
                sensorContext.sensorUnitMeasurementScale.push(sensorUnitMeasurementScale);
                //sensorTriggers
                for (var j = 0; j < sensor.sensorTriggers.length; j++) {
                    var sensorTrigger = sensor.sensorTriggers[j];
                    sensorTrigger.sensor = sensor;
                    delete sensorTrigger.sensorId; // removendo a foreing key
                    sensorContext.sensorTrigger.push(sensorTrigger);
                }
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var sensor = oldValues[i];
                //sensorTempDSFamily
                for (var j = 0; j < sensorContext.sensorTempDSFamily.length; j++) {
                    if (sensor.sensorTempDSFamily === sensorContext.sensorTempDSFamily[j]) {
                        sensorContext.sensorTempDSFamily.splice(j, 1);
                        break;
                    }
                }
                //sensorUnitMeasurementScale
                for (var j = 0; j < sensorContext.sensorUnitMeasurementScale.length; j++) {
                    if (sensor.sensorUnitMeasurementScale === sensorContext.sensorUnitMeasurementScale[j]) {
                        sensorContext.sensorUnitMeasurementScale.splice(j, 1);
                        break;
                    }
                }
                //sensorTrigger
                for (var j = 0; j < sensor.sensorTriggers.length; j++) {
                    for (var k = 0; k < sensorContext.sensorTrigger.length; k++) {
                        if (sensor.sensorTriggers[j] === sensorContext.sensorTrigger[k]) {
                            sensorContext.sensorTrigger.splice(k, 1);
                            break;
                        }
                    }
                }
            }
        });

        // *** Navigation Properties Mappers ***
        
        var mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init = false;
        var mapper_SensorTempDSFamily_SensorTempDSFamilyResolution = function () {
            if (!mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init && sensorContext.sensorTempDSFamilyLoaded && sensorContext.sensorTempDSFamilyResolutionLoaded) {
                mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init = true;
                sensorTempDSFamilyResolutionLoadedUnbinding();
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
                unitMeasurementScaleLoadedUnbinding();
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

            sensorContext.sensorLoaded = true;
            sensorContext.sensorTriggerLoaded = true;
            sensorContext.sensorUnitMeasurementScaleLoaded = true;
            sensorContext.sensorTempDSFamilyLoaded = true;
            sensorContext.sensorTempDSFamilyResolutionLoaded = true;

            mapper_SensorTempDSFamily_SensorTempDSFamilyResolution();
            mapper_SensorUnitMeasurementScale_UnitMeasurementScale();
        }

        var sensorGetAllByApplicationIdCompletedSubscription = $rootScope.$on(sensorConstant.getAllByApplicationIdCompletedEventName, onSensorGetAllByApplicationIdCompleted);

        $rootScope.$on('$destroy', function () {
            sensorTypeGetAllByApplicationIdCompletedSubscription();
        });

        // *** Events Subscriptions


        // *** Watches

        var sensorTempDSFamilyResolutionLoadedUnbinding = sensorContext.$watch('sensorTempDSFamilyResolutionLoaded', function (newValue, oldValue) {
            mapper_SensorTempDSFamily_SensorTempDSFamilyResolution();
        })

        var unitMeasurementScaleLoadedUnbinding = siContext.$watch('unitMeasurementScaleLoaded', function (newValue, oldValue) {
            mapper_SensorUnitMeasurementScale_UnitMeasurementScale();
        })

        // *** Watches

        return serviceFactory;

    }]);