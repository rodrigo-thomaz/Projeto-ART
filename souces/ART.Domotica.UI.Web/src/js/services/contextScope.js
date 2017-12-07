'use strict';
app.factory('contextScope', [

    '$rootScope',

    'globalizationContext',
    'globalizationFinder',
    'globalizationMapper',
    'timeZoneService',

    'localeContext',
    'localeFinder',
    'localeMapper', 
    'continentService',
    'countryService',

    'siContext',
    'siFinder',
    'siMapper',   
    'numericalScalePrefixService',
    'numericalScaleService',
    'numericalScaleTypeCountryService',
    'numericalScaleTypeService',
    'unitMeasurementScaleService',
    'unitMeasurementService',
    'unitMeasurementTypeService',

    'sensorDatasheetContext',
    'sensorDatasheetFinder',
    'sensorDatasheetMapper',
    'sensorDatasheetService',
    'sensorDatasheetUnitMeasurementDefaultService',
    'sensorDatasheetUnitMeasurementScaleService',
    'sensorTypeService',

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
    'sensorService',

    function (

        $rootScope,

        globalizationContext,
        globalizationFinder,
        globalizationMapper,
        timeZoneService,

        localeContext,
        localeFinder,
        localeMapper,
        continentService,
        countryService,

        siContext,
        siFinder,
        siMapper,        
        numericalScalePrefixService,
        numericalScaleService,
        numericalScaleTypeCountryService,
        numericalScaleTypeService,
        unitMeasurementScaleService,
        unitMeasurementService,
        unitMeasurementTypeService,

        sensorDatasheetContext,
        sensorDatasheetFinder,
        sensorDatasheetMapper,
        sensorDatasheetService,
        sensorDatasheetUnitMeasurementDefaultService,
        sensorDatasheetUnitMeasurementScaleService,
        sensorTypeService,

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
        sensorTriggerService,
        sensorService
    ) {

    var context = $rootScope.$new();
        
    return context;

}]);