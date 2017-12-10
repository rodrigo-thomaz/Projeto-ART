'use strict';
app.factory('sensorMapper', [
    '$rootScope',
    'sensorContext',
    'sensorConstant',
    'sensorTempDSFamilyResolutionConstant',
    'sensorFinder',
    'siContext',
    'unitMeasurementScaleFinder',
    'sensorDatasheetContext',
    'sensorDatasheetFinder',
    'sensorTempDSFamilyResolutionFinder',
    function (
        $rootScope,
        sensorContext,
        sensorConstant,
        sensorTempDSFamilyResolutionConstant,
        sensorFinder,
        siContext,
        unitMeasurementScaleFinder,
        sensorDatasheetContext,
        sensorDatasheetFinder,
        sensorTempDSFamilyResolutionFinder) {

        var serviceFactory = {};

        var addSensorAggregates = function (sensor) {
            //sensorTempDSFamily
            sensorContext.sensorTempDSFamily.push(sensor.sensorTempDSFamily);
            //sensorUnitMeasurementScale
            sensorContext.sensorUnitMeasurementScale.push(sensor.sensorUnitMeasurementScale);
            //sensorTrigger
            for (var i = 0; i < sensor.sensorTriggers.length; i++) {
                sensorContext.sensorTrigger.push(sensor.sensorTriggers[i]);
            }
        }

        var removeSensorAggregates = function (sensor) {
            //sensorTempDSFamily
            for (var i = 0; i < sensorContext.sensorTempDSFamily.length; i++) {
                if (sensor.sensorTempDSFamily === sensorContext.sensorTempDSFamily[i]) {
                    sensorContext.sensorTempDSFamily.splice(i, 1);
                    break;
                }
            }
            //sensorUnitMeasurementScale
            for (var i = 0; i < sensorContext.sensorUnitMeasurementScale.length; i++) {
                if (sensor.sensorUnitMeasurementScale === sensorContext.sensorUnitMeasurementScale[i]) {
                    sensorContext.sensorUnitMeasurementScale.splice(i, 1);
                    break;
                }
            }
            //sensorTrigger
            for (var i = 0; i < sensor.sensorTriggers.length; i++) {
                for (var j = 0; j < sensorContext.sensorTrigger.length; j++) {
                    if (sensor.sensorTriggers[i] === sensorContext.sensorTrigger[j]) {
                        sensorContext.sensorTrigger.splice(j, 1);
                        break;
                    }
                }
            }
        }

        sensorContext.$watchCollection('sensor', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var sensor = newValues[i];
                addSensorAggregates(sensor);
                sensor.sensorDatasheet = function () { return sensorDatasheetFinder.getByKey(this.sensorDatasheetId, this.sensorTypeId); }
                                
                //sensorTempDSFamily
                var sensorTempDSFamily = sensor.sensorTempDSFamily;
                sensorTempDSFamily.sensor = sensor;
                //sensorUnitMeasurementScale
                var sensorUnitMeasurementScale = sensor.sensorUnitMeasurementScale;
                sensorUnitMeasurementScale.sensor = sensor;
                //sensorTriggers
                for (var j = 0; j < sensor.sensorTriggers.length; j++) {
                    var sensorTrigger = sensor.sensorTriggers[j];
                    sensorTrigger.sensor = sensor;
                }
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var sensor = oldValues[i];
                removeSensorAggregates(sensor);                
            }
        });

        sensorContext.$watchCollection('sensorTempDSFamily', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                setSensorTempDSFamilyResolutionInSensorTempDSFamily(newValues[i]);
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var sensorTempDSFamily = oldValues[i];
                var sensorTempDSFamilyResolution = sensorTempDSFamilyResolutionFinder.getByKey(sensorTempDSFamily.sensorTempDSFamilyResolution.sensorTempDSFamilyResolutionId);
                for (var j = 0; j < sensorTempDSFamilyResolution.sensorTempDSFamilies.length; j++) {
                    if (sensorTempDSFamily === sensorTempDSFamilyResolution.sensorTempDSFamilies[j]) {
                        sensorTempDSFamilyResolution.sensorTempDSFamilies.splice(j, 1);
                        break;
                    }
                }
            }
        });

        sensorContext.$watchCollection('sensorUnitMeasurementScale', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                setUnitMeasurementScaleInSensorUnitMeasurementScale(newValues[i]);
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var sensorUnitMeasurementScale = oldValues[i];
                var unitMeasurementScale = unitMeasurementScaleFinder.getByKey(sensorUnitMeasurementScale.unitMeasurementScale.unitMeasurementId, sensorUnitMeasurementScale.unitMeasurementScale.unitMeasurementTypeId, sensorUnitMeasurementScale.unitMeasurementScale.numericalScalePrefixId, sensorUnitMeasurementScale.unitMeasurementScale.numericalScaleTypeId);
                for (var j = 0; j < unitMeasurementScale.sensorUnitMeasurementScales.length; j++) {
                    if (sensorUnitMeasurementScale === unitMeasurementScale.sensorUnitMeasurementScales[j]) {
                        unitMeasurementScale.sensorUnitMeasurementScales.splice(j, 1);
                        break;
                    }
                }
            }
        });

        var setSensorTempDSFamilyResolutionInSensorTempDSFamily = function (sensorTempDSFamily) {
            if (sensorTempDSFamily.sensorTempDSFamilyResolution) return;
            var sensorTempDSFamilyResolution = sensorTempDSFamilyResolutionFinder.getByKey(sensorTempDSFamily.sensorTempDSFamilyResolutionId);
            sensorTempDSFamily.sensorTempDSFamilyResolution = sensorTempDSFamilyResolution;
            delete sensorTempDSFamily.sensorTempDSFamilyResolutionId; // removendo a foreing key
            if (sensorTempDSFamilyResolution.sensorTempDSFamilies === undefined) {
                sensorTempDSFamilyResolution.sensorTempDSFamilies = [];
            }
            sensorTempDSFamilyResolution.sensorTempDSFamilies.push(sensorTempDSFamily);
        }

        var setUnitMeasurementScaleInSensorUnitMeasurementScale = function (sensorUnitMeasurementScale) {
            if (sensorUnitMeasurementScale.unitMeasurementScale) return;
            var unitMeasurementScale = unitMeasurementScaleFinder.getByKey(sensorUnitMeasurementScale.unitMeasurementId, sensorUnitMeasurementScale.unitMeasurementTypeId, sensorUnitMeasurementScale.numericalScalePrefixId, sensorUnitMeasurementScale.numericalScaleTypeId);
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

        // *** Navigation Properties Mappers ***

        var mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init = false;
        var mapper_SensorTempDSFamily_SensorTempDSFamilyResolution = function () {
            if (!mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init && sensorContext.sensorTempDSFamilyLoaded && sensorContext.sensorTempDSFamilyResolutionLoaded) {
                mapper_SensorTempDSFamily_SensorTempDSFamilyResolution_Init = true;
                sensorTempDSFamilyResolutionLoadedUnbinding();
                for (var i = 0; i < sensorContext.sensorTempDSFamily.length; i++) {
                    setSensorTempDSFamilyResolutionInSensorTempDSFamily(sensorContext.sensorTempDSFamily[i]);
                }
            }
        };

        var mapper_SensorUnitMeasurementScale_UnitMeasurementScale_Init = false;
        var mapper_SensorUnitMeasurementScale_UnitMeasurementScale = function () {
            if (!mapper_SensorUnitMeasurementScale_UnitMeasurementScale_Init && sensorContext.sensorUnitMeasurementScaleLoaded && siContext.unitMeasurementScaleLoaded) {
                mapper_SensorUnitMeasurementScale_UnitMeasurementScale_Init = true;
                unitMeasurementScaleLoadedUnbinding();
                for (var i = 0; i < sensorContext.sensorUnitMeasurementScale.length; i++) {
                    setUnitMeasurementScaleInSensorUnitMeasurementScale(sensorContext.sensorUnitMeasurementScale[i]);
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
        }

        var onSensorTempDSFamilyResolutionGetAllCompleted = function (event, data) {
            sensorTempDSFamilyResolutionGetAllCompletedSubscription();
            sensorContext.sensorTempDSFamilyResolutionLoaded = true;
        }

        var sensorGetAllByApplicationIdCompletedSubscription = $rootScope.$on(sensorConstant.getAllByApplicationIdCompletedEventName, onSensorGetAllByApplicationIdCompleted);
        var sensorTempDSFamilyResolutionGetAllCompletedSubscription = $rootScope.$on(sensorTempDSFamilyResolutionConstant.getAllCompletedEventName, onSensorTempDSFamilyResolutionGetAllCompleted);

        $rootScope.$on('$destroy', function () {
            sensorTypeGetAllByApplicationIdCompletedSubscription();
            sensorTempDSFamilyResolutionGetAllCompletedSubscription();
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