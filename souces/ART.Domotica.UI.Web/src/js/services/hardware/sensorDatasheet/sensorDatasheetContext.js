'use strict';
app.factory('sensorDatasheetContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();
        
    context.sensorTypeLoaded = false;
    context.sensorType = [];

    context.sensorDatasheetLoaded = false;
    context.sensorDatasheet = [];

    context.sensorUnitMeasurementDefaultLoaded = false;
    context.sensorUnitMeasurementDefault = [];

    context.sensorDatasheetUnitMeasurementScaleLoaded = false;
    context.sensorDatasheetUnitMeasurementScale = [];

    return context;

}]);