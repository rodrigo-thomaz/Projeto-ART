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
    'deviceNTPFinder',
    'sensorInDeviceFinder',
    'deviceFinder',

    'deviceMapper',
    'deviceSensorsService',
    'deviceNTPService',
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
        deviceNTPFinder,
        sensorInDeviceFinder,
        deviceFinder,

        deviceMapper,
        deviceSensorsService,
        deviceNTPService,
        sensorInDeviceService,
        deviceService
        
    ) {

        var result = {};

        return result;

    }]);