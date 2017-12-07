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

    'sensorContext',
    'sensorFinder',
    'sensorMapper',

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
        deviceMapper,

        sensorContext,
        sensorFinder,
        sensorMapper
    ) {

    var context = $rootScope.$new();
        
    return context;

}]);