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
    'deviceSensorsService',
    'deviceNTPService',
    'sensorsInDeviceService',
    'deviceService',

    'sensorContext',
    'sensorFinder',
    'sensorMapper',
    'sensorTempDSFamilyResolutionService',
    'sensorTempDSFamilyService',
    'sensorUnitMeasurementScaleService',
    'sensorTriggerService',

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
        deviceSensorsService,
        deviceNTPService,
        sensorsInDeviceService,
        deviceService,

        sensorContext,
        sensorFinder,
        sensorMapper,
        sensorTempDSFamilyResolutionService,
        sensorTempDSFamilyService,
        sensorUnitMeasurementScaleService,
        sensorTriggerService
    ) {

    var context = $rootScope.$new();
        
    return context;

}]);