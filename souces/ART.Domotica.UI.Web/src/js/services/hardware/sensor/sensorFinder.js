'use strict';
app.factory('sensorFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};    

    var getSensorByKey = function (sensorId) {
        for (var i = 0; i < context.sensor.length; i++) {
            var item = context.sensor[i];
            if (item.sensorId === sensorId) {
                return item;
            }
        }
    }

    var getSensorTriggerByKey = function (sensorTriggerId) {
        for (var i = 0; i < context.sensorTrigger.length; i++) {
            var item = context.sensorTrigger[i];
            if (item.sensorTriggerId === sensorTriggerId) {
                return item;
            }
        }
    }

    var getSensorUnitMeasurementScaleByKey = function (sensorUnitMeasurementScaleId) {
        for (var i = 0; i < context.sensorUnitMeasurementScale.length; i++) {
            var item = context.sensorUnitMeasurementScale[i];
            if (item.sensorUnitMeasurementScaleId === sensorUnitMeasurementScaleId) {
                return item;
            }
        }
    }

    var getSensorTempDSFamilyByKey = function (sensorTempDSFamilyId) {
        for (var i = 0; i < context.sensorTempDSFamily.length; i++) {
            var item = context.sensorTempDSFamily[i];
            if (item.sensorTempDSFamilyId === sensorTempDSFamilyId) {
                return item;
            }
        }
    }

    var getSensorTempDSFamilyResolutionByKey = function (sensorTempDSFamilyResolutionId) {
        for (var i = 0; i < context.sensorTempDSFamilyResolution.length; i++) {
            var item = context.sensorTempDSFamilyResolution[i];
            if (item.sensorTempDSFamilyResolutionId === sensorTempDSFamilyResolutionId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getSensorByKey = getSensorByKey;
    serviceFactory.getSensorTriggerByKey = getSensorTriggerByKey;
    serviceFactory.getSensorUnitMeasurementScaleByKey = getSensorUnitMeasurementScaleByKey;
    serviceFactory.getSensorTempDSFamilyByKey = getSensorTempDSFamilyByKey;
    serviceFactory.getSensorTempDSFamilyResolutionByKey = getSensorTempDSFamilyResolutionByKey;

    return serviceFactory;

}]);