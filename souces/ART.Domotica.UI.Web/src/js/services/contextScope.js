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
    'deviceTypeFinder',
    'deviceTypeService',
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

    'deviceSensorFinder',
    'deviceWiFiFinder',
    'deviceNTPFinder',
    'deviceSerialFinder',
    'deviceDebugFinder',
    'sensorInDeviceFinder',
    'deviceFinder',

    'deviceMapper',
    'deviceSensorService',
    'deviceWiFiService',
    'deviceNTPService',
    'deviceSerialService',
    'deviceDebugService',
    'sensorInDeviceService',
    'deviceInApplicationService',
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
        deviceTypeFinder,
        deviceTypeService,
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

        deviceSensorFinder,
        deviceWiFiFinder,
        deviceNTPFinder,
        deviceSerialFinder,
        deviceDebugFinder,
        sensorInDeviceFinder,
        deviceFinder,

        deviceMapper,
        deviceSensorService,
        deviceWiFiService,
        deviceNTPService,
        deviceSerialService,
        deviceDebugService,
        sensorInDeviceService,
        deviceInApplicationService,
        deviceService
        
    ) {

        var result = {};

        return result;

    }]);