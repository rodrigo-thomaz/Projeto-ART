'use strict';
app.constant('sensorConstant', {

    getAllByApplicationIdApiUri: 'api/Sensor/getAllByApplicationId',
    getAllByApplicationIdCompletedTopic: 'Sensor.GetAllByApplicationIdViewCompleted',
    getAllByApplicationIdCompletedEventName: 'sensorService.onGetAllByApplicationIdCompleted_Id_',

    setLabelApiUri: 'api/sensor/setLabel',
    setLabelCompletedTopic: 'Sensor.SetLabelViewCompleted',
    setLabelCompletedEventName: 'sensorService.onSetLabelCompleted_Id_',

    setSensorUnitMeasurementScaleApiUri: 'api/sensor/setSensorUnitMeasurementScale',
    setSensorUnitMeasurementScaleCompletedTopic: 'Sensor.SetSensorUnitMeasurementScaleViewCompleted',
    setSensorUnitMeasurementScaleCompletedEventName: 'sensorService.onSetSensorUnitMeasurementScaleCompleted_Id_',

});