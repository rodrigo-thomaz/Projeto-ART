'use strict';
app.constant('sensorConstant', {

    getAllByApplicationIdApiUri: 'api/Sensor/getAllByApplicationId',
    getAllByApplicationIdCompletedTopic: 'Sensor.GetAllByApplicationIdViewCompleted',
    getAllByApplicationIdCompletedEventName: 'sensorService.onGetAllByApplicationIdCompleted_Id_',

    setLabelApiUri: 'api/sensor/setLabel',
    setLabelCompletedTopic: 'Sensor.SetLabelViewCompleted',
    setLabelCompletedEventName: 'sensorService.onSetLabelCompleted_Id_',

    insertInApplicationCompletedTopic: 'Sensor.InsertInApplicationViewCompleted',
    insertInApplicationCompletedEventName: 'sensorService.onInsertInApplicationCompleted',

    deleteFromApplicationCompletedTopic: 'Sensor.DeleteFromApplicationViewCompleted',
    deleteFromApplicationCompletedEventName: 'sensorService.onDeleteFromApplicationCompleted',
});

app.constant('sensorTempDSFamilyConstant', {

    setResolutionApiUri: 'api/sensorTempDSFamily/setResolution',
    setResolutionCompletedTopic: 'SensorTempDSFamily.SetResolutionViewCompleted',
    setResolutionCompletedEventName: 'sensorTempDSFamilyService.onSetResolutionCompleted_Id_',

});

app.constant('sensorTempDSFamilyResolutionConstant', {

    getAllApiUri: 'api/sensorTempDSFamily/getAllResolutions',
    getAllCompletedTopic: 'SensorTempDSFamily.GetAllResolutionsViewCompleted',
    getAllCompletedEventName: 'sensorTempDSFamilyResolutionService.onGetAllCompleted',

});

app.constant('sensorUnitMeasurementScaleConstant', {

    setDatasheetUnitMeasurementScaleApiUri: 'api/sensorUnitMeasurementScale/setDatasheetUnitMeasurementScale',
    setDatasheetUnitMeasurementScaleCompletedTopic: 'SensorUnitMeasurementScale.SetDatasheetUnitMeasurementScaleViewCompleted',
    setDatasheetUnitMeasurementScaleCompletedEventName: 'sensorUnitMeasurementScaleService.SetDatasheetUnitMeasurementScaleCompleted_Id_',

    setUnitMeasurementNumericalScaleTypeCountryApiUri: 'api/sensorUnitMeasurementScale/setUnitMeasurementNumericalScaleTypeCountry',
    setUnitMeasurementNumericalScaleTypeCountryCompletedTopic: 'SensorUnitMeasurementScale.SetUnitMeasurementNumericalScaleTypeCountryViewCompleted',
    setUnitMeasurementNumericalScaleTypeCountryCompletedEventName: 'sensorUnitMeasurementScaleService.SetUnitMeasurementNumericalScaleTypeCountryCompleted_Id_',

    setRangeApiUri: 'api/sensorUnitMeasurementScale/setRange',
    setRangeCompletedTopic: 'SensorUnitMeasurementScale.SetRangeViewCompleted',
    setRangeCompletedEventName: 'sensorUnitMeasurementScaleService.SetRangeCompleted_Id_',

    setChartLimiterApiUri: 'api/sensorUnitMeasurementScale/setChartLimiter',
    setChartLimiterCompletedTopic: 'SensorUnitMeasurementScale.SetChartLimiterViewCompleted',
    setChartLimiterCompletedEventName: 'sensorUnitMeasurementScaleService.SetChartLimiterCompleted_Id_',

});

app.constant('sensorTriggerConstant', {

    insertApiUri: 'api/sensorTrigger/insertTrigger',
    insertCompletedTopic: 'SensorTrigger.InsertViewCompleted',
    insertCompletedEventName: 'sensorTriggerService.onInsertCompleted_Id_',

    deleteApiUri: 'api/sensorTrigger/deleteTrigger',
    deleteCompletedTopic: 'SensorTrigger.DeleteViewCompleted',
    deleteCompletedEventName: 'sensorTriggerService.onDeleteCompleted_Id_',

    setTriggerOnApiUri: 'api/sensorTrigger/setTriggerOn',
    setTriggerOnCompletedTopic: 'SensorTrigger.SetTriggerOnViewCompleted',
    setTriggerOnCompletedEventName: 'sensorTriggerService.onSetTriggerOnCompleted_Id_',

    setBuzzerOnApiUri: 'api/sensorTrigger/setBuzzerOn',
    setBuzzerOnCompletedTopic: 'SensorTrigger.SetBuzzerOnViewCompleted',
    setBuzzerOnCompletedEventName: 'sensorTriggerService.SetBuzzerOnCompleted_Id_',

    setTriggerValueApiUri: 'api/sensorTrigger/setTriggerValue',
    setTriggerValueCompletedTopic: 'SensorTrigger.SetTriggerValueViewCompleted',
    setTriggerValueCompletedEventName: 'sensorTriggerService.onSetTriggerValueCompleted_Id_',

});