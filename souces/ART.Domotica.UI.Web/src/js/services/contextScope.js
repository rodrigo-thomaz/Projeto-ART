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
            var item = context.unitOfMeasurementTypes[i];
            if (item.unitOfMeasurementTypeId === unitOfMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitOfMeasurementByKey = function (unitOfMeasurementId, unitOfMeasurementTypeId) {
        for (var i = 0; i < context.unitOfMeasurements.length; i++) {
            var item = context.unitOfMeasurements[i];
            if (item.unitOfMeasurementId === unitOfMeasurementId && item.unitOfMeasurementTypeId === unitOfMeasurementTypeId) {
                return item;
            }
        }
    }

    var getSensorTypeByKey = function (sensorTypeId) {
        for (var i = 0; i < context.sensorTypes.length; i++) {
            var item = context.sensorTypes[i];
            if (item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getSensorDatasheetByKey = function (sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorDatasheets.length; i++) {
            var item = context.sensorDatasheets[i];
            if (item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getSensorUnitOfMeasurementDefaultByKey = function (sensorUnitOfMeasurementDefaultId, sensorTypeId) {
        for (var i = 0; i < context.sensorUnitOfMeasurementDefaults.length; i++) {
            var item = context.sensorUnitOfMeasurementDefaults[i];
            if (item.sensorUnitOfMeasurementDefaultId === sensorUnitOfMeasurementDefaultId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };

    var getSensorByKey = function (sensorId) {
        for (var i = 0; i < context.sensors.length; i++) {
            var item = context.sensors[i];
            if (item.sensorId === sensorId) {
                return item;
            }
        }
    };

    // Navigation Properties Mappers

    var mapper_UnitOfMeasurement_UnitOfMeasurementType_Init = false;
    var mapper_UnitOfMeasurement_UnitOfMeasurementType = function () {
        if (!mapper_UnitOfMeasurement_UnitOfMeasurementType_Init && context.unitOfMeasurementTypeLoaded && context.unitOfMeasurementLoaded) {
            mapper_UnitOfMeasurement_UnitOfMeasurementType_Init = true;
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

    var mapper_SensorDatasheet_SensorTypeType_Init = false;
    var mapper_SensorDatasheet_SensorTypeType = function () {
        if (!mapper_SensorDatasheet_SensorTypeType_Init && context.sensorDatasheetLoaded && context.sensorTypeLoaded) {
            mapper_SensorDatasheet_SensorTypeType_Init = true;
            for (var i = 0; i < context.sensorDatasheets.length; i++) {
                var sensorDatasheet = context.sensorDatasheets[i];
                var sensorType = getSensorTypeByKey(sensorDatasheet.sensorTypeId);
                sensorDatasheet.sensorType = sensorType;
                if (sensorType.sensorDatasheets === undefined) {
                    sensorType.sensorDatasheets = [];
                }
                sensorType.sensorDatasheets.push(sensorDatasheet);
            }
        }
    };

    var mapper_SensorUnitOfMeasurementDefault_UnitOfMeasurement_Init = false;
    var mapper_SensorUnitOfMeasurementDefault_UnitOfMeasurement = function () {
        if (!mapper_SensorUnitOfMeasurementDefault_UnitOfMeasurement_Init && context.sensorUnitOfMeasurementDefaultLoaded && context.unitOfMeasurementLoaded) {
            mapper_SensorUnitOfMeasurementDefault_UnitOfMeasurement_Init = true;
            for (var i = 0; i < context.sensorUnitOfMeasurementDefaults.length; i++) {
                var sensorUnitOfMeasurementDefault = context.sensorUnitOfMeasurementDefaults[i];
                var unitOfMeasurement = getUnitOfMeasurementByKey(sensorUnitOfMeasurementDefault.unitOfMeasurementId, sensorUnitOfMeasurementDefault.unitOfMeasurementTypeId);
                sensorUnitOfMeasurementDefault.unitOfMeasurement = unitOfMeasurement;
                if (unitOfMeasurement.sensorUnitOfMeasurementDefaults === undefined) {
                    unitOfMeasurement.sensorUnitOfMeasurementDefaults = [];
                }
                unitOfMeasurement.sensorUnitOfMeasurementDefaults.push(sensorUnitOfMeasurementDefault);
            }
        }
    };

    var mapper_SensorUnitOfMeasurementDefault_SensorType_Init = false;
    var mapper_SensorUnitOfMeasurementDefault_SensorType = function () {
        if (!mapper_SensorUnitOfMeasurementDefault_SensorType_Init && context.sensorUnitOfMeasurementDefaultLoaded && context.sensorTypeLoaded) {
            mapper_SensorUnitOfMeasurementDefault_SensorType_Init = true;
            for (var i = 0; i < context.sensorUnitOfMeasurementDefaults.length; i++) {
                var sensorUnitOfMeasurementDefault = context.sensorUnitOfMeasurementDefaults[i];
                var sensorType = getSensorTypeByKey(sensorUnitOfMeasurementDefault.sensorTypeId);
                sensorUnitOfMeasurementDefault.sensorType = sensorType;
                if (sensorType.sensorUnitOfMeasurementDefaults === undefined) {
                    sensorType.sensorUnitOfMeasurementDefaults = [];
                }
                sensorType.sensorUnitOfMeasurementDefaults.push(sensorUnitOfMeasurementDefault);
            }
        }
    };

    // Watches

    context.$watch('unitOfMeasurementTypeLoaded', function (newValue, oldValue) {
        mapper_UnitOfMeasurement_UnitOfMeasurementType();
    });

    context.$watch('unitOfMeasurementLoaded', function (newValue, oldValue) {
        mapper_UnitOfMeasurement_UnitOfMeasurementType();
        mapper_SensorUnitOfMeasurementDefault_UnitOfMeasurement();
    });    

    context.$watch('sensorTypeLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorUnitOfMeasurementDefault_SensorType();
    });

    context.$watch('sensorDatasheetLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
    });

    context.$watch('sensorUnitOfMeasurementDefaultLoaded', function (newValue, oldValue) {
        mapper_SensorUnitOfMeasurementDefault_UnitOfMeasurement();
        mapper_SensorUnitOfMeasurementDefault_SensorType();
    });

    context.$watch('sensorsLoaded', function (newValue, oldValue) {
        
    });

    // Public Methods

    context.getUnitOfMeasurementTypeByKey = getUnitOfMeasurementTypeByKey;
    context.getUnitOfMeasurementByKey = getUnitOfMeasurementByKey;
    context.getSensorTypeByKey = getSensorTypeByKey;
    context.getSensorDatasheetByKey = getSensorDatasheetByKey;
    context.getSensorUnitOfMeasurementDefaultByKey = getSensorUnitOfMeasurementDefaultByKey;
    context.getSensorByKey = getSensorByKey;

    return context;

}]);