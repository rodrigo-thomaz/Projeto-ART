'use strict';
app.factory('contextScope', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // Public Properties

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

    // Finders

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

    // Navigation Properties Binds

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

    // Watches

    context.$watch('unitOfMeasurementTypeLoaded', function (newValue, oldValue) {
        bind_UnitOfMeasurement_UnitOfMeasurementType();
    });

    context.$watch('unitOfMeasurementLoaded', function (newValue, oldValue) {
        bind_UnitOfMeasurement_UnitOfMeasurementType();
    });    

    // Public Methods

    context.getUnitOfMeasurementTypeByKey = getUnitOfMeasurementTypeByKey;
    context.getUnitOfMeasurementByKey = getUnitOfMeasurementByKey;

    return context;

}]);