'use strict';
app.factory('sensorDatasheetMapper', ['$rootScope', 'sensorDatasheetContext', 'sensorTypeConstant', 'sensorDatasheetConstant', 'sensorUnitMeasurementDefaultConstant', 'sensorDatasheetUnitMeasurementScaleConstant', 'unitMeasurementScaleConstant',
    function ($rootScope, sensorDatasheetContext, sensorTypeConstant, sensorDatasheetConstant, sensorUnitMeasurementDefaultConstant, sensorDatasheetUnitMeasurementScaleConstant, unitMeasurementScaleConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

   
    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onSensorTypeGetAllCompleted = function (event, data) {
        sensorTypeGetAllCompletedSubscription();
        sensorDatasheetContext.sensorTypeLoaded = true;
    }   

    var onSensorDatasheetGetAllCompleted = function (event, data) {
        sensorDatasheetGetAllCompletedSubscription();
        sensorDatasheetContext.sensorDatasheetLoaded = true;
    }  

    var onSensorUnitMeasurementDefaultGetAllCompleted = function (event, data) {
        sensorUnitMeasurementDefaultGetAllCompletedSubscription();
        sensorDatasheetContext.sensorUnitMeasurementDefaultLoaded = true;
    }  

    var onSensorDatasheetUnitMeasurementScaleGetAllCompleted = function (event, data) {
        sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();
        sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded = true;
    }  

    var onUnitMeasurementScaleGetAllCompleted = function (event, data) {
        unitMeasurementScaleGetAllCompletedSubscription();        
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