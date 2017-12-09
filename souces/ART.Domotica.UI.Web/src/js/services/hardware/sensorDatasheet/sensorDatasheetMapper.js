'use strict';
app.factory('sensorDatasheetMapper', ['$rootScope', 'sensorDatasheetContext', 'siContext', 'sensorDatasheetFinder', 'unitMeasurementScaleFinder', 'sensorTypeConstant', 'sensorDatasheetConstant', 'sensorDatasheetUnitMeasurementDefaultConstant', 'sensorDatasheetUnitMeasurementScaleConstant', 
    function ($rootScope, sensorDatasheetContext, siContext, sensorDatasheetFinder, unitMeasurementScaleFinder, sensorTypeConstant, sensorDatasheetConstant, sensorDatasheetUnitMeasurementDefaultConstant, sensorDatasheetUnitMeasurementScaleConstant) {

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

    var mapper_SensorDatasheetUnitMeasurementDefault_UnitMeasurementScale_Init = false;
    var mapper_SensorDatasheetUnitMeasurementDefault_UnitMeasurementScale = function () {
        if (!mapper_SensorDatasheetUnitMeasurementDefault_UnitMeasurementScale_Init && sensorDatasheetContext.sensorDatasheetUnitMeasurementDefaultLoaded && siContext.unitMeasurementScaleLoaded) {
            mapper_SensorDatasheetUnitMeasurementDefault_UnitMeasurementScale_Init = true;
            unitMeasurementScaleLoadedUnbinding();
            for (var i = 0; i < sensorDatasheetContext.sensorDatasheetUnitMeasurementDefault.length; i++) {
                var sensorDatasheetUnitMeasurementDefault = sensorDatasheetContext.sensorDatasheetUnitMeasurementDefault[i];
                var unitMeasurementScale = unitMeasurementScaleFinder.getByKey(sensorDatasheetUnitMeasurementDefault.unitMeasurementId, sensorDatasheetUnitMeasurementDefault.unitMeasurementTypeId, sensorDatasheetUnitMeasurementDefault.numericalScalePrefixId, sensorDatasheetUnitMeasurementDefault.numericalScaleTypeId);
                sensorDatasheetUnitMeasurementDefault.unitMeasurementScale = unitMeasurementScale;
                if (unitMeasurementScale.sensorDatasheetUnitMeasurementDefaults === undefined) {
                    unitMeasurementScale.sensorDatasheetUnitMeasurementDefaults = [];
                }
                unitMeasurementScale.sensorDatasheetUnitMeasurementDefaults.push(sensorDatasheetUnitMeasurementDefault);
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

    var mapper_SensorDatasheet_SensorDatasheetUnitMeasurementDefault_Init = false;
    var mapper_SensorDatasheet_SensorDatasheetUnitMeasurementDefault = function () {
        if (!mapper_SensorDatasheet_SensorDatasheetUnitMeasurementDefault_Init && sensorDatasheetContext.sensorDatasheetLoaded && sensorDatasheetContext.sensorDatasheetUnitMeasurementDefaultLoaded) {
            mapper_SensorDatasheet_SensorDatasheetUnitMeasurementDefault_Init = true;
            for (var i = 0; i < sensorDatasheetContext.sensorDatasheet.length; i++) {
                var sensorDatasheet = sensorDatasheetContext.sensorDatasheet[i];
                var sensorDatasheetUnitMeasurementDefault = sensorDatasheetFinder.getSensorDatasheetUnitMeasurementDefaultByKey(sensorDatasheet.sensorDatasheetId, sensorDatasheet.sensorTypeId);
                sensorDatasheet.sensorDatasheetUnitMeasurementDefault = sensorDatasheetUnitMeasurementDefault;
                sensorDatasheetUnitMeasurementDefault.sensorDatasheet = sensorDatasheet;
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
        mapper_SensorDatasheet_SensorDatasheetUnitMeasurementDefault();
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet();
    }  

    var onSensorDatasheetUnitMeasurementDefaultGetAllCompleted = function (event, data) {
        sensorDatasheetUnitMeasurementDefaultGetAllCompletedSubscription();
        sensorDatasheetContext.sensorDatasheetUnitMeasurementDefaultLoaded = true;
        mapper_SensorDatasheet_SensorDatasheetUnitMeasurementDefault();
        mapper_SensorDatasheetUnitMeasurementDefault_UnitMeasurementScale();
    }  

    var onSensorDatasheetUnitMeasurementScaleGetAllCompleted = function (event, data) {
        sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();
        sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded = true;
        mapper_SensorDatasheetUnitMeasurementScale_SensorDatasheet();
    }  

    var sensorTypeGetAllCompletedSubscription = $rootScope.$on(sensorTypeConstant.getAllCompletedEventName, onSensorTypeGetAllCompleted);
    var sensorDatasheetGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetConstant.getAllCompletedEventName, onSensorDatasheetGetAllCompleted);
    var sensorDatasheetUnitMeasurementDefaultGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedEventName, onSensorDatasheetUnitMeasurementDefaultGetAllCompleted);
    var sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedEventName, onSensorDatasheetUnitMeasurementScaleGetAllCompleted);

    $rootScope.$on('$destroy', function () {
        sensorTypeGetAllCompletedSubscription();        
        sensorDatasheetGetAllCompletedSubscription();        
        sensorDatasheetUnitMeasurementDefaultGetAllCompletedSubscription();        
        sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();   
    });

    // *** Events Subscriptions

    // *** Watches

    var unitMeasurementScaleLoadedUnbinding = siContext.$watch('unitMeasurementScaleLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheetUnitMeasurementDefault_UnitMeasurementScale();
    })

    // *** Watches

    
    return serviceFactory;

}]);