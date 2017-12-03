'use strict';
app.factory('contextScope', ['$rootScope', 'localeContext', 'localeMapper', 'siContext', 'siMapper', function ($rootScope, localeContext, localeMapper, siContext, siMapper) {

    var context = $rootScope.$new();

    // *** Public Properties ***       

    context.sensorTypeLoaded = false;
    context.sensorTypes = [];

    context.sensorDatasheetLoaded = false;
    context.sensorDatasheets = [];

    context.sensorUnitMeasurementDefaultLoaded = false;
    context.sensorUnitMeasurementDefaults = [];

    context.sensorUnitMeasurementScaleLoaded = false;
    context.sensorUnitMeasurementScales = [];

    context.sensorsLoaded = false;
    context.sensors = [];    

    // *** Finders ***        
    
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


    // *** Navigation Properties Mappers ***   

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

    var mapper_SensorUnitMeasurementScale_SensorDatasheet_Init = false;
    var mapper_SensorUnitMeasurementScale_SensorDatasheet = function () {
        if (!mapper_SensorUnitMeasurementScale_SensorDatasheet_Init && context.sensorUnitMeasurementScaleLoaded && context.sensorDatasheetLoaded) {
            mapper_SensorUnitMeasurementScale_SensorDatasheet_Init = true;
            for (var i = 0; i < context.sensorUnitMeasurementScales.length; i++) {
                var sensorUnitMeasurementScale = context.sensorUnitMeasurementScales[i];
                var sensorDatasheet = getSensorDatasheetByKey(sensorUnitMeasurementScale.sensorDatasheetId, sensorUnitMeasurementScale.sensorTypeId);
                sensorUnitMeasurementScale.sensorDatasheet = sensorDatasheet;
                if (sensorDatasheet.sensorUnitMeasurementScales === undefined) {
                    sensorDatasheet.sensorUnitMeasurementScales = [];
                }
                sensorDatasheet.sensorUnitMeasurementScales.push(sensorUnitMeasurementScale);
            }
        }
    };

    // *** Watches ***

    // SI

    siContext.$watch('unitMeasurementLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementDefault_UnitMeasurement();
    }); 

    //

    context.$watch('sensorUnitMeasurementScaleLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementScale_SensorDatasheet();
    });    

    context.$watch('sensorTypeLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorUnitMeasurementDefault_SensorType();
    });

    context.$watch('sensorDatasheetLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorUnitMeasurementScale_SensorDatasheet();
    });

    context.$watch('sensorUnitMeasurementDefaultLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementDefault_UnitMeasurement();
        mapper_SensorUnitMeasurementDefault_SensorType();
    });

    context.$watch('sensorsLoaded', function (newValue, oldValue) {
        
    });

    // *** Public Methods ***
    
    context.getSensorTypeByKey = getSensorTypeByKey;
    context.getSensorDatasheetByKey = getSensorDatasheetByKey;
    context.getSensorUnitMeasurementDefaultByKey = getSensorUnitMeasurementDefaultByKey;
    context.getSensorByKey = getSensorByKey;

    return context;

}]);