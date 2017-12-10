'use strict';
app.factory('contextScope', [

    'globalizationContext',
    'globalizationFinder',
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

    function (

        globalizationContext,
        globalizationFinder,
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

        deviceContext,

        deviceSensorsFinder,
        deviceNTPFinder,
        sensorInDeviceFinder,
        deviceFinder,

        deviceMapper,
        deviceSensorsService,
        deviceNTPService,
        sensorInDeviceService,
        deviceService,

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
        sensorService
    ) {

        var result = {};

        return result;

    }]);