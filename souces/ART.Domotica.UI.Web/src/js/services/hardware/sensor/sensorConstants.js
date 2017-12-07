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

app.constant('sensorTempDSFamilyConstant', {
        
    setResolutionApiUri: 'api/sensorTempDSFamily/setResolution',
    setResolutionCompletedTopic: 'SensorTempDSFamily.SetResolutionViewCompleted',
    setResolutionCompletedEventName: 'sensorTempDSFamilyService_onSetResolutionCompleted_Id_',

});

app.constant('sensorTempDSFamilyResolutionConstant', {

    getAllApiUri: 'api/sensorTempDSFamily/getAllResolutions',
    getAllCompletedTopic: 'SensorTempDSFamily.GetAllResolutionsViewCompleted',
    getAllCompletedEventName: 'sensorTempDSFamilyResolutionService.onGetAllCompleted',

});