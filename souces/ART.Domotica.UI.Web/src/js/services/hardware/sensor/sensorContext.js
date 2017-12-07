'use strict';
app.factory('sensorContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.sensor = [];
    context.sensorLoaded = false;    

    context.sensorTrigger = [];
    context.sensorTriggerLoaded = false;    

    context.sensorUnitMeasurementScale = [];    
    context.sensorUnitMeasurementScaleLoaded = false;    

    context.sensorTempDSFamily = [];
    context.sensorTempDSFamilyLoaded = false;    

    context.sensorTempDSFamilyResolution = [];
    context.sensorTempDSFamilyResolutionLoaded = false;    

    return context;

}]);