'use strict';
app.factory('contextScope', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // Public Properties

    // Locale

    context.continents = [];    
    context.continentLoaded = false;    

    context.countries = [];    
    context.countryLoaded = false;    

    //SI

    context.numericalScales = [];    
    context.numericalScaleLoaded = false;

    context.numericalScalePrefixes = [];    
    context.numericalScalePrefixLoaded = false;    

    context.numericalScaleTypes = [];    
    context.numericalScaleTypeLoaded = false;    

    context.numericalScaleTypeCountries = []; 
    context.numericalScaleTypeCountryLoaded = false;    
    
    context.unitMeasurementScales = [];
    context.unitMeasurementScaleLoaded = [];

    context.unitMeasurementTypeLoaded = false;
    context.unitMeasurementTypes = [];    

    context.unitMeasurementLoaded = false;
    context.unitMeasurements = [];

    //

    context.sensorTypeLoaded = false;
    context.sensorTypes = [];

    context.sensorDatasheetLoaded = false;
    context.sensorDatasheets = [];

    context.sensorUnitMeasurementDefaultLoaded = false;
    context.sensorUnitMeasurementDefaults = [];

    context.sensorsLoaded = false;
    context.sensors = [];    

    // Finders

    var getUnitMeasurementTypeByKey = function (unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurementTypes.length; i++) {
            var item = context.unitMeasurementTypes[i];
            if (item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementByKey = function (unitMeasurementId, unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurements.length; i++) {
            var item = context.unitMeasurements[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId) {
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

    var getSensorUnitMeasurementDefaultByKey = function (sensorUnitMeasurementDefaultId, sensorTypeId) {
        for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
            var item = context.sensorUnitMeasurementDefaults[i];
            if (item.sensorUnitMeasurementDefaultId === sensorUnitMeasurementDefaultId && item.sensorTypeId === sensorTypeId) {
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

    var mapper_UnitMeasurement_UnitMeasurementType_Init = false;
    var mapper_UnitMeasurement_UnitMeasurementType = function () {
        if (!mapper_UnitMeasurement_UnitMeasurementType_Init && context.unitMeasurementTypeLoaded && context.unitMeasurementLoaded) {
            mapper_UnitMeasurement_UnitMeasurementType_Init = true;
            for (var i = 0; i < context.unitMeasurements.length; i++) {
                var unitMeasurement = context.unitMeasurements[i];
                var unitMeasurementType = getUnitMeasurementTypeByKey(unitMeasurement.unitMeasurementTypeId);
                unitMeasurement.unitMeasurementType = unitMeasurementType;
                if (unitMeasurementType.unitMeasurements === undefined) {
                    unitMeasurementType.unitMeasurements = [];
                }
                unitMeasurementType.unitMeasurements.push(unitMeasurement);
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

    var mapper_SensorUnitMeasurementDefault_UnitMeasurement_Init = false;
    var mapper_SensorUnitMeasurementDefault_UnitMeasurement = function () {
        if (!mapper_SensorUnitMeasurementDefault_UnitMeasurement_Init && context.sensorUnitMeasurementDefaultLoaded && context.unitMeasurementLoaded) {
            mapper_SensorUnitMeasurementDefault_UnitMeasurement_Init = true;
            for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
                var sensorUnitMeasurementDefault = context.sensorUnitMeasurementDefaults[i];
                var unitMeasurement = getUnitMeasurementByKey(sensorUnitMeasurementDefault.unitMeasurementId, sensorUnitMeasurementDefault.unitMeasurementTypeId);
                sensorUnitMeasurementDefault.unitMeasurement = unitMeasurement;
                if (unitMeasurement.sensorUnitMeasurementDefaults === undefined) {
                    unitMeasurement.sensorUnitMeasurementDefaults = [];
                }
                unitMeasurement.sensorUnitMeasurementDefaults.push(sensorUnitMeasurementDefault);
            }
        }
    };

    var mapper_SensorUnitMeasurementDefault_SensorType_Init = false;
    var mapper_SensorUnitMeasurementDefault_SensorType = function () {
        if (!mapper_SensorUnitMeasurementDefault_SensorType_Init && context.sensorUnitMeasurementDefaultLoaded && context.sensorTypeLoaded) {
            mapper_SensorUnitMeasurementDefault_SensorType_Init = true;
            for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
                var sensorUnitMeasurementDefault = context.sensorUnitMeasurementDefaults[i];
                var sensorType = getSensorTypeByKey(sensorUnitMeasurementDefault.sensorTypeId);
                sensorUnitMeasurementDefault.sensorType = sensorType;
                if (sensorType.sensorUnitMeasurementDefaults === undefined) {
                    sensorType.sensorUnitMeasurementDefaults = [];
                }
                sensorType.sensorUnitMeasurementDefaults.push(sensorUnitMeasurementDefault);
            }
        }
    };

    // Watches

    context.$watch('unitMeasurementTypeLoaded', function (newValue, oldValue) {
        mapper_UnitMeasurement_UnitMeasurementType();
    });

    context.$watch('unitMeasurementLoaded', function (newValue, oldValue) {
        mapper_UnitMeasurement_UnitMeasurementType();
        mapper_SensorUnitMeasurementDefault_UnitMeasurement();
    });    

    context.$watch('sensorTypeLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorUnitMeasurementDefault_SensorType();
    });

    context.$watch('sensorDatasheetLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
    });

    context.$watch('sensorUnitMeasurementDefaultLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementDefault_UnitMeasurement();
        mapper_SensorUnitMeasurementDefault_SensorType();
    });

    context.$watch('sensorsLoaded', function (newValue, oldValue) {
        
    });

    // Public Methods

    context.getUnitMeasurementTypeByKey = getUnitMeasurementTypeByKey;
    context.getUnitMeasurementByKey = getUnitMeasurementByKey;
    context.getSensorTypeByKey = getSensorTypeByKey;
    context.getSensorDatasheetByKey = getSensorDatasheetByKey;
    context.getSensorUnitMeasurementDefaultByKey = getSensorUnitMeasurementDefaultByKey;
    context.getSensorByKey = getSensorByKey;

    return context;

}]);