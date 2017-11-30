'use strict';
app.factory('contextScope', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.unitOfMeasurementTypeLoaded = false;
    context.unitOfMeasurementTypes = [];    

    context.unitOfMeasurementLoaded = false;
    context.unitOfMeasurements = [];

    context.sensorTypeLoaded = false;
    context.sensorTypes = [];

    context.sensorDatasheetLoaded = false;
    context.sensorDatasheets = [];

    context.sensorUnitOfMeasurementDefaultLoaded = false;
    context.sensorUnitOfMeasurementDefaults = [];

    context.sensorsLoaded = false;
    context.sensors = [];



    context.$watch('unitOfMeasurementTypeLoaded', function (newValue, oldValue) {
        bind_UnitOfMeasurement_UnitOfMeasurementType();
    });

    context.$watch('unitOfMeasurementLoaded', function (newValue, oldValue) {
        bind_UnitOfMeasurement_UnitOfMeasurementType();
    });

    var bind_UnitOfMeasurement_UnitOfMeasurementType = function () {
        if (context.unitOfMeasurementTypeLoaded && context.unitOfMeasurementLoaded) {
            for (var i = 0; i < context.unitOfMeasurements.length; i++) {
                var unitOfMeasurement = context.unitOfMeasurements[i];
                var unitOfMeasurementType = getUnitOfMeasurementTypeByKey(unitOfMeasurement.unitOfMeasurementTypeId);
                unitOfMeasurement.unitOfMeasurementType = unitOfMeasurementType;
                if (unitOfMeasurementType.unitOfMeasurements === undefined) {
                    unitOfMeasurementType.unitOfMeasurements = [];
                }
                unitOfMeasurementType.unitOfMeasurements.push(unitOfMeasurement);
            }
        }
    };

    var getUnitOfMeasurementTypeByKey = function (unitOfMeasurementTypeId) {
        for (var i = 0; i < context.unitOfMeasurementTypes.length; i++) {
            if (context.unitOfMeasurementTypes[i].unitOfMeasurementTypeId === unitOfMeasurementTypeId) {
                return context.unitOfMeasurementTypes[i];
            }
        }
    }

    var getUnitOfMeasurementByKey = function (unitOfMeasurementId, unitOfMeasurementTypeId) {
        for (var i = 0; i < context.unitOfMeasurements.length; i++) {
            if (context.unitOfMeasurements[i].unitOfMeasurementId === unitOfMeasurementId && context.unitOfMeasurements[i].unitOfMeasurementTypeId === unitOfMeasurementTypeId) {
                return context.unitOfMeasurements[i];
            }
        }
    }

    context.$watchCollection('unitOfMeasurementTypes', function (newValues, oldValues) {

    });

    context.$watchCollection('unitOfMeasurements', function (newValues, oldValues) {

    });

    context.$watchCollection('sensorTypes', function (newValues, oldValues) {

    });

    context.$watchCollection('sensorDatasheets', function (newValues, oldValues) {

    });

    context.$watchCollection('sensorUnitOfMeasurementDefaults', function (newValues, oldValues) {

    });

    context.$watchCollection('sensors', function (newValues, oldValues) {

    });

    context.getUnitOfMeasurementTypeByKey = getUnitOfMeasurementTypeByKey;
    context.getUnitOfMeasurementByKey = getUnitOfMeasurementByKey;

    return context;

}]);