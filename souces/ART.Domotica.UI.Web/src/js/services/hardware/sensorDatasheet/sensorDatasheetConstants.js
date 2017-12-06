'use strict';
app.constant('sensorTypeConstant', {
    getAllApiUri: 'api/sensorType/getAll',
    getAllCompletedTopic: 'SensorType.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorTypeService.GetAllCompleted',
});

app.constant('sensorDatasheetConstant', {
    getAllApiUri: 'api/sensorDatasheet/getAll',
    getAllCompletedTopic: 'SensorDatasheet.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorDatasheetService.GetAllCompleted',
});

app.constant('sensorDatasheetUnitMeasurementDefaultConstant', {
    getAllApiUri: 'api/sensorDatasheetUnitMeasurementDefault/getAll',
    getAllCompletedTopic: 'SensorDatasheetUnitMeasurementDefault.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorDatasheetUnitMeasurementDefaultService.GetAllCompleted',
});

app.constant('sensorDatasheetUnitMeasurementScaleConstant', {
    getAllApiUri: 'api/sensorDatasheetUnitMeasurementScale/getAll',
    getAllCompletedTopic: 'SensorDatasheetUnitMeasurementScale.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorDatasheetUnitMeasurementScaleService.GetAllCompleted',
});