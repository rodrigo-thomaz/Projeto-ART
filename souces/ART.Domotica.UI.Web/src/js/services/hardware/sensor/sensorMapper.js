'use strict';
app.factory('sensorMapper', [
    '$rootScope',
    'sensorContext',
    'sensorConstant',
    'sensorTempDSFamilyResolutionConstant',
    'sensorFinder',
    'siContext',
    'sensorDatasheetUnitMeasurementScaleFinder',
    'sensorDatasheetContext',
    'sensorDatasheetFinder',
    'sensorTempDSFamilyResolutionFinder',
    'sensorInDeviceFinder',
    'sensorUnitMeasurementScaleFinder',
    'sensorTempDSFamilyFinder',
    function (
        $rootScope,
        sensorContext,
        sensorConstant,
        sensorTempDSFamilyResolutionConstant,
        sensorFinder,
        siContext,
        sensorDatasheetUnitMeasurementScaleFinder,
        sensorDatasheetContext,
        sensorDatasheetFinder,
        sensorTempDSFamilyResolutionFinder,
        sensorInDeviceFinder,
        sensorUnitMeasurementScaleFinder,
        sensorTempDSFamilyFinder) {

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
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                removeSensorAggregates(oldValues[i]);
            }
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var sensor = newValues[i];
                addSensorAggregates(sensor);
                sensor.sensorDatasheet = function () { return sensorDatasheetFinder.getByKey(this.sensorDatasheetId, this.sensorTypeId); }
                sensor.sensorInDevice = function () { return sensorInDeviceFinder.getBySensorKey(this.sensorId, this.sensorDatasheetId, this.sensorTypeId); }
            }            
        });

        sensorContext.$watchCollection('sensorTempDSFamily', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorTempDSFamily = newValues[i];
                sensorTempDSFamily.sensor = function () { return sensorFinder.getByKey(this.sensorTempDSFamilyId, this.sensorDatasheetId, this.sensorTypeId); }
                sensorTempDSFamily.sensorTempDSFamilyResolution = function () { return sensorTempDSFamilyResolutionFinder.getByKey(this.sensorTempDSFamilyResolutionId); }
            }
        });

        sensorContext.$watchCollection('sensorTempDSFamilyResolution', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorTempDSFamilyResolution = newValues[i];
                sensorTempDSFamilyResolution.sensorsTempDSFamily = function () { return sensorTempDSFamilyFinder.getBySensorTempDSFamilyResolutionKey(this.sensorTempDSFamilyResolutionId); }
            }
        });

        sensorContext.$watchCollection('sensorUnitMeasurementScale', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorUnitMeasurementScale = newValues[i];
                sensorUnitMeasurementScale.sensor = function () { return sensorFinder.getByKey(this.sensorUnitMeasurementScaleId, this.sensorDatasheetId, this.sensorTypeId); }
                sensorUnitMeasurementScale.sensorDatasheetUnitMeasurementScale = function () { return sensorDatasheetUnitMeasurementScaleFinder.getByKey(this.sensorDatasheetId, this.sensorTypeId, this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
            }
        });                        
        
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
        
        return serviceFactory;

    }]);