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

    context.sensorDatasheetUnitMeasurementScaleLoaded = false;
    context.sensorDatasheetUnitMeasurementScales = [];

    context.sensorsLoaded = false;
    context.sensors = [];    

    context.deviceSensorsLoaded = false;
    context.deviceSensors = [];    

    context.sensorsInDeviceLoaded = false;
    context.sensorsInDevice = [];    

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

    var getDeviceSensorsByKey = function (deviceSensorsId) {
        for (var i = 0; i < context.deviceSensors.length; i++) {
            var item = context.deviceSensors[i];
            if (item.deviceSensorsId === deviceSensorsId) {
                return item;
            }
        }
    }; 

    var getSensorsInDeviceByKey = function (deviceSensorsId, sensorId) {
        for (var i = 0; i < context.sensorsInDevice.length; i++) {
            var item = context.sensorsInDevice[i];
            if (item.deviceSensorsId === deviceSensorsId && item.sensorId === sensorId) {
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

    var mapper_SensorUnitMeasurementDefault_UnitMeasurementScale_Init = false;
    var mapper_SensorUnitMeasurementDefault_UnitMeasurementScale = function () {
        if (!mapper_SensorUnitMeasurementDefault_UnitMeasurementScale_Init && context.sensorUnitMeasurementDefaultLoaded && siContext.unitMeasurementScaleLoaded) {
            mapper_SensorUnitMeasurementDefault_UnitMeasurementScale_Init = true;
            for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
                var sensorUnitMeasurementDefault = context.sensorUnitMeasurementDefaults[i];
                var unitMeasurementScale = siContext.getUnitMeasurementScaleByKey(sensorUnitMeasurementDefault.unitMeasurementId, sensorUnitMeasurementDefault.unitMeasurementTypeId, sensorUnitMeasurementDefault.numericalScalePrefixId, sensorUnitMeasurementDefault.numericalScaleTypeId);
                sensorUnitMeasurementDefault.unitMeasurementScale = unitMeasurementScale;
                if (unitMeasurementScale.sensorUnitMeasurementDefaults === undefined) {
                    unitMeasurementScale.sensorUnitMeasurementDefaults = [];
                }
                unitMeasurementScale.sensorUnitMeasurementDefaults.push(sensorUnitMeasurementDefault);
            }
        }
    };    

    var mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet_Init = false;
    var mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet = function () {
        if (!mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet_Init && context.sensorDatasheetUnitMeasurementScaleLoaded && context.sensorDatasheetLoaded) {
            mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet_Init = true;
            for (var i = 0; i < context.sensorDatasheetUnitMeasurementScales.length; i++) {
                var sensorDatasheetUnitMeasurementScale = context.sensorDatasheetUnitMeasurementScales[i];
                var sensorDatasheet = getSensorDatasheetByKey(sensorDatasheetUnitMeasurementScale.sensorDatasheetId, sensorDatasheetUnitMeasurementScale.sensorTypeId);
                sensorDatasheetUnitMeasurementScale.sensorDatasheet = sensorDatasheet;
                if (sensorDatasheet.sensorDatasheetUnitMeasurementScales === undefined) {
                    sensorDatasheet.sensorDatasheetUnitMeasurementScales = [];
                }
                sensorDatasheet.sensorDatasheetUnitMeasurementScales.push(sensorDatasheetUnitMeasurementScale);
            }
        }
    };

    // *** Watches ***

    // SI

    siContext.$watch('unitMeasurementScaleLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementDefault_UnitMeasurementScale();
    }); 

    //

    context.$watch('sensorDatasheetUnitMeasurementScaleLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet();        
    });    

    context.$watch('sensorTypeLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
    });

    context.$watch('sensorDatasheetLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet();
    });

    context.$watch('sensorUnitMeasurementDefaultLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementDefault_UnitMeasurementScale();
    });

    context.$watch('sensorsLoaded', function (newValue, oldValue) {
        
    });

    context.$watch('sensorsInDeviceLoaded', function (newValue, oldValue) {

    });

    context.$watch('deviceSensorsLoaded', function (newValue, oldValue) {

    });    

    // *** Public Methods ***
    
    context.getSensorTypeByKey = getSensorTypeByKey;
    context.getSensorDatasheetByKey = getSensorDatasheetByKey;
    context.getSensorUnitMeasurementDefaultByKey = getSensorUnitMeasurementDefaultByKey;
    context.getSensorByKey = getSensorByKey;
    context.getDeviceSensorsByKey = getDeviceSensorsByKey;
    context.getSensorsInDeviceByKey = getSensorsInDeviceByKey;

    return context;

}]);