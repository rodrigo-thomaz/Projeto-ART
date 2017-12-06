'use strict';
app.factory('contextScope', [
    '$rootScope',

    'localeContext',
    'localeFinder',
    'localeMapper',    

    'siContext',
    'siFinder',
    'siMapper',

    'globalizationContext',
    'globalizationFinder',
    'globalizationMapper',
    'timeZoneService',

    'sensorDatasheetContext',
    'sensorDatasheetFinder',
    'sensorDatasheetMapper',

    'deviceContext',
    'deviceFinder',
    'deviceMapper',

    function (
        $rootScope,

        localeContext,
        localeFinder,
        localeMapper,

        siContext,
        siFinder,
        siMapper,

        globalizationContext,
        globalizationFinder,
        globalizationMapper,
        timeZoneService,

        sensorDatasheetContext,
        sensorDatasheetFinder,
        sensorDatasheetMapper,

        deviceContext,
        deviceFinder,
        deviceMapper
    ) {

    var context = $rootScope.$new();

    // *** Public Properties ***           

    context.sensorUnitMeasurementScaleLoaded = false;
    context.sensorUnitMeasurementScales = [];    

    context.sensorsLoaded = false;
    context.sensors = [];    

    // *** Finders ***      

    var getSensorByKey = function (sensorId) {
        for (var i = 0; i < context.sensors.length; i++) {
            var item = context.sensors[i];
            if (item.sensorId === sensorId) {
                return item;
            }
        }
    };    

    // *** Navigation Properties Mappers ***      
    

    // *** Watches ***
    

    context.$watch('sensorsLoaded', function (newValue, oldValue) {
        
    });

    // *** Public Methods ***

    context.getSensorByKey = getSensorByKey;

    return context;

}]);