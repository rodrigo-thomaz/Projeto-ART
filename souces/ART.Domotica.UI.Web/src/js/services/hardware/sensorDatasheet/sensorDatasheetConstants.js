'use strict';
app.constant('sensorTypeConstants', {
    getAllApiUri: 'api/sensorType/getAll',
    getAllCompletedTopic: 'SensorType.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorTypeService.GetAllCompleted',
});

app.constant('sensorDatasheetConstants', {
    getAllApiUri: 'api/sensorDatasheet/getAll',
    getAllCompletedTopic: 'SensorDatasheet.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorDatasheetService.GetAllCompleted',
});

app.constant('sensorUnitMeasurementDefaultConstants', {
    getAllApiUri: 'api/sensorUnitMeasurementDefault/getAll',
    getAllCompletedTopic: 'SensorUnitMeasurementDefault.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorUnitMeasurementDefaultService.GetAllCompleted',
});

app.constant('sensorDatasheetUnitMeasurementScaleConstants', {
    getAllApiUri: 'api/sensorDatasheetUnitMeasurementScale/getAll',
    getAllCompletedTopic: 'SensorDatasheetUnitMeasurementScale.GetAllViewCompleted',
    getAllCompletedEventName: 'sensorDatasheetUnitMeasurementScaleService.GetAllCompleted',
});