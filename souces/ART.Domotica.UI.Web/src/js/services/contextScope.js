'use strict';
app.factory('contextScope', [

    'globalizationContext',
    'timeZoneFinder',
    'globalizationMapper',
    'timeZoneService',

    'localeContext',
    'continentFinder',
    'countryFinder',
    'localeMapper',
    'continentService',
    'countryService',

    'siContext',

    'numericalScaleFinder',    
    'numericalScalePrefixFinder',
    'numericalScaleTypeFinder',
    'numericalScaleTypeCountryFinder',
    'unitMeasurementScaleFinder',
    'unitMeasurementFinder',
    'unitMeasurementTypeFinder',    

    'siMapper',
    'numericalScalePrefixService',
    'numericalScaleService',
    'numericalScaleTypeCountryService',
    'numericalScaleTypeService',
    'unitMeasurementScaleService',
    'unitMeasurementService',
    'unitMeasurementTypeService',    

    'sensorDatasheetContext',
    'sensorDatasheetMapper',

    'sensorDatasheetFinder',
    'sensorDatasheetUnitMeasurementDefaultFinder',
    'sensorDatasheetUnitMeasurementScaleFinder',
    'sensorTypeFinder',

    'sensorDatasheetService',
    'sensorDatasheetUnitMeasurementDefaultService',
    'sensorDatasheetUnitMeasurementScaleService',
    'sensorTypeService',

    'deviceDatasheetContext',
    'deviceDatasheetMapper',
    'deviceDatasheetFinder',
    'deviceDatasheetService',

    'sensorContext',

    'sensorFinder',
    'sensorUnitMeasurementScaleFinder',
    'sensorTriggerFinder',
    'sensorTempDSFamilyResolutionFinder',
    'sensorTempDSFamilyFinder',

    'sensorMapper',
    'sensorTempDSFamilyResolutionService',
    'sensorTempDSFamilyService',
    'sensorUnitMeasurementScaleService',
    'sensorTriggerService',
    'sensorService',

    'deviceContext',

    'deviceSensorsFinder',
    'deviceWiFiFinder',
    'deviceNTPFinder',
    'deviceDebugFinder',
    'sensorInDeviceFinder',
    'deviceFinder',

    'deviceMapper',
    'deviceSensorsService',
    'deviceWiFiService',
    'deviceNTPService',
    'deviceDebugService',
    'sensorInDeviceService',
    'deviceService',

    function (

        globalizationContext,
        timeZoneFinder,
        globalizationMapper,
        timeZoneService,

        localeContext,
        continentFinder,
        countryFinder,
        localeMapper,
        continentService,
        countryService,

        siContext,

        numericalScaleFinder,
        numericalScalePrefixFinder,
        numericalScaleTypeFinder,
        numericalScaleTypeCountryFinder,
        unitMeasurementScaleFinder,
        unitMeasurementFinder,
        unitMeasurementTypeFinder,

        siMapper,
        numericalScalePrefixService,
        numericalScaleService,
        numericalScaleTypeCountryService,
        numericalScaleTypeService,
        unitMeasurementScaleService,
        unitMeasurementService,
        unitMeasurementTypeService,
               
        sensorDatasheetContext,        
        sensorDatasheetMapper,

        sensorDatasheetFinder,
        sensorDatasheetUnitMeasurementDefaultFinder,
        sensorDatasheetUnitMeasurementScaleFinder,
        sensorTypeFinder,

        sensorDatasheetService,
        sensorDatasheetUnitMeasurementDefaultService,
        sensorDatasheetUnitMeasurementScaleService,
        sensorTypeService,

        deviceDatasheetContext,
        deviceDatasheetMapper,
        deviceDatasheetFinder,
        deviceDatasheetService,

        sensorContext,

        sensorFinder,
        sensorUnitMeasurementScaleFinder,
        sensorTriggerFinder,
        sensorTempDSFamilyResolutionFinder,
        sensorTempDSFamilyFinder,

        sensorMapper,
        sensorTempDSFamilyResolutionService,
        sensorTempDSFamilyService,
        sensorUnitMeasurementScaleService,
        sensorTriggerService,
        sensorService,

        deviceContext,

        deviceSensorsFinder,
        deviceWiFiFinder,
        deviceNTPFinder,
        deviceDebugFinder,
        sensorInDeviceFinder,
        deviceFinder,

        deviceMapper,
        deviceSensorsService,
        deviceWiFiService,
        deviceNTPService,
        deviceDebugService,
        sensorInDeviceService,
        deviceService
        
    ) {

        var result = {};

        return result;

    }]);