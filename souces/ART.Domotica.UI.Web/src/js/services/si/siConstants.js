'use strict';
app.constant('numericalScalePrefixConstant', {
    getAllApiUri: 'api/si/numericalScalePrefix/getAll',
    getAllCompletedTopic: 'SI.NumericalScalePrefix.GetAllViewCompleted',
    initializedEventName: 'numericalScalePrefixService.onInitialized',
});

app.constant('numericalScaleConstant', {
    getAllApiUri: 'api/si/numericalScale/getAll',
    getAllCompletedTopic: 'SI.NumericalScale.GetAllViewCompleted',
    initializedEventName: 'numericalScaleService.onInitialized',
});

app.constant('numericalScaleTypeCountryConstant', {
    getAllApiUri: 'api/si/numericalScaleTypeCountry/getAll',
    getAllCompletedTopic: 'SI.NumericalScaleTypeCountry.GetAllViewCompleted',
    initializedEventName: 'numericalScaleTypeCountryService.onInitialized',
});

app.constant('numericalScaleTypeConstant', {
    getAllApiUri: 'api/si/numericalScaleType/getAll',
    getAllCompletedTopic: 'SI.NumericalScaleType.GetAllViewCompleted',
    initializedEventName: 'numericalScaleTypeService.onInitialized',
});

app.constant('unitMeasurementScaleConstant', {
    getAllApiUri: 'api/si/unitMeasurementScale/getAll',
    getAllCompletedTopic: 'SI.UnitMeasurementScale.GetAllViewCompleted',
    initializedEventName: 'unitMeasurementScaleService.onInitialized',
});

app.constant('unitMeasurementConstant', {
    getAllApiUri: 'api/si/unitMeasurement/getAll',
    getAllCompletedTopic: 'SI.UnitMeasurement.GetAllViewCompleted',
    initializedEventName: 'unitMeasurementService.onInitialized',
});

app.constant('unitMeasurementTypeConstant', {
    getAllApiUri: 'api/si/unitMeasurementType/getAll',
    getAllCompletedTopic: 'SI.UnitMeasurementType.GetAllViewCompleted',
    initializedEventName: 'unitMeasurementTypeService.onInitialized',
});