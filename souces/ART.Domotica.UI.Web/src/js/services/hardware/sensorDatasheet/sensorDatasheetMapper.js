'use strict';
app.factory('sensorDatasheetMapper', ['$rootScope', 'sensorDatasheetContext', 'siContext', 'sensorDatasheetFinder', 'siFinder', 'sensorTypeConstant', 'sensorDatasheetConstant', 'sensorUnitMeasurementDefaultConstant', 'sensorDatasheetUnitMeasurementScaleConstant', 'unitMeasurementScaleConstant',
    function ($rootScope, sensorDatasheetContext, siContext, sensorDatasheetFinder, siFinder, sensorTypeConstant, sensorDatasheetConstant, sensorUnitMeasurementDefaultConstant, sensorDatasheetUnitMeasurementScaleConstant, unitMeasurementScaleConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

    var mapper_SensorDatasheet_SensorTypeType_Init = false;
    var mapper_SensorDatasheet_SensorTypeType = function () {
        if (!mapper_SensorDatasheet_SensorTypeType_Init && sensorDatasheetContext.sensorDatasheetLoaded && sensorDatasheetContext.sensorTypeLoaded) {
            mapper_SensorDatasheet_SensorTypeType_Init = true;
            for (var i = 0; i < sensorDatasheetContext.sensorDatasheet.length; i++) {
                var sensorDatasheet = sensorDatasheetContext.sensorDatasheet[i];
                var sensorType = sensorDatasheetFinder.getSensorTypeByKey(sensorDatasheet.sensorTypeId);
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
        if (!mapper_SensorUnitMeasurementDefault_UnitMeasurementScale_Init && sensorDatasheetContext.sensorUnitMeasurementDefaultLoaded && siContext.unitMeasurementScaleLoaded) {
            mapper_SensorUnitMeasurementDefault_UnitMeasurementScale_Init = true;
            for (var i = 0; i < sensorDatasheetContext.sensorUnitMeasurementDefault.length; i++) {
                var sensorUnitMeasurementDefault = sensorDatasheetContext.sensorUnitMeasurementDefault[i];
                var unitMeasurementScale = siFinder.getUnitMeasurementScaleByKey(sensorUnitMeasurementDefault.unitMeasurementId, sensorUnitMeasurementDefault.unitMeasurementTypeId, sensorUnitMeasurementDefault.numericalScalePrefixId, sensorUnitMeasurementDefault.numericalScaleTypeId);
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
        if (!mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet_Init && sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded && sensorDatasheetContext.sensorDatasheetLoaded) {
            mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet_Init = true;
            for (var i = 0; i < sensorDatasheetContext.sensorDatasheetUnitMeasurementScale.length; i++) {
                var sensorDatasheetUnitMeasurementScale = sensorDatasheetContext.sensorDatasheetUnitMeasurementScale[i];
                var sensorDatasheet = sensorDatasheetFinder.getSensorDatasheetByKey(sensorDatasheetUnitMeasurementScale.sensorDatasheetId, sensorDatasheetUnitMeasurementScale.sensorTypeId);
                sensorDatasheetUnitMeasurementScale.sensorDatasheet = sensorDatasheet;
                if (sensorDatasheet.sensorDatasheetUnitMeasurementScales === undefined) {
                    sensorDatasheet.sensorDatasheetUnitMeasurementScales = [];
                }
                sensorDatasheet.sensorDatasheetUnitMeasurementScales.push(sensorDatasheetUnitMeasurementScale);
            }
        }
    };

    var mapper_SensorDatasheet_SensorUnitMeasurementDefault_Init = false;
    var mapper_SensorDatasheet_SensorUnitMeasurementDefault = function () {
        if (!mapper_SensorDatasheet_SensorUnitMeasurementDefault_Init && sensorDatasheetContext.sensorDatasheetLoaded && sensorDatasheetContext.sensorUnitMeasurementDefaultLoaded) {
            mapper_SensorDatasheet_SensorUnitMeasurementDefault_Init = true;
            for (var i = 0; i < sensorDatasheetContext.sensorDatasheet.length; i++) {
                var sensorDatasheet = sensorDatasheetContext.sensorDatasheet[i];
                var sensorUnitMeasurementDefault = sensorDatasheetFinder.getSensorUnitMeasurementDefaultByKey(sensorDatasheet.sensorDatasheetId, sensorDatasheet.sensorTypeId);
                sensorDatasheet.sensorUnitMeasurementDefault = sensorUnitMeasurementDefault;
                sensorUnitMeasurementDefault.sensorDatasheet = sensorDatasheet;
            }
        }
    };

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onSensorTypeGetAllCompleted = function (event, data) {
        sensorTypeGetAllCompletedSubscription();
        sensorDatasheetContext.sensorTypeLoaded = true;
        mapper_SensorDatasheet_SensorTypeType();
    }   

    var onSensorDatasheetGetAllCompleted = function (event, data) {
        sensorDatasheetGetAllCompletedSubscription();
        sensorDatasheetContext.sensorDatasheetLoaded = true;
        mapper_SensorDatasheet_SensorUnitMeasurementDefault();
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet();
    }  

    var onSensorUnitMeasurementDefaultGetAllCompleted = function (event, data) {
        sensorUnitMeasurementDefaultGetAllCompletedSubscription();
        sensorDatasheetContext.sensorUnitMeasurementDefaultLoaded = true;
        mapper_SensorDatasheet_SensorUnitMeasurementDefault();
        mapper_SensorUnitMeasurementDefault_UnitMeasurementScale();
    }  

    var onSensorDatasheetUnitMeasurementScaleGetAllCompleted = function (event, data) {
        sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();
        sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded = true;
        mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet();
    }  

    var onUnitMeasurementScaleGetAllCompleted = function (event, data) {
        unitMeasurementScaleGetAllCompletedSubscription();   
        mapper_SensorUnitMeasurementDefault_UnitMeasurementScale();
    }

    var sensorTypeGetAllCompletedSubscription = $rootScope.$on(sensorTypeConstant.getAllCompletedEventName, onSensorTypeGetAllCompleted);
    var sensorDatasheetGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetConstant.getAllCompletedEventName, onSensorDatasheetGetAllCompleted);
    var sensorUnitMeasurementDefaultGetAllCompletedSubscription = $rootScope.$on(sensorUnitMeasurementDefaultConstant.getAllCompletedEventName, onSensorUnitMeasurementDefaultGetAllCompleted);
    var sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedEventName, onSensorDatasheetUnitMeasurementScaleGetAllCompleted);
    var unitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(unitMeasurementScaleConstant.getAllCompletedEventName, onUnitMeasurementScaleGetAllCompleted);

    $rootScope.$on('$destroy', function () {
        sensorTypeGetAllCompletedSubscription();        
        sensorDatasheetGetAllCompletedSubscription();        
        sensorUnitMeasurementDefaultGetAllCompletedSubscription();        
        sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();   
        unitMeasurementScaleGetAllCompletedSubscription();
    });

    // *** Events Subscriptions
    
    
    return serviceFactory;

}]);