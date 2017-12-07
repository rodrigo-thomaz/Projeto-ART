'use strict';
app.constant('sensorConstant', {

    getAllByApplicationIdApiUri: 'api/Sensor/getAllByApplicationId',
    getAllByApplicationIdCompletedTopic: 'Sensor.GetAllByApplicationIdViewCompleted',
    getAllByApplicationIdCompletedEventName: 'sensorService.onGetAllByApplicationIdCompleted_Id_',

    setLabelApiUri: 'api/sensor/setLabel',
    setLabelCompletedTopic: 'Sensor.SetLabelViewCompleted',
    setLabelCompletedEventName: 'sensorService.onSetLabelCompleted_Id_',

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

    setValueApiUri: 'api/sensorUnitMeasurementScale/setValue',
    setValueCompletedTopic: 'SensorUnitMeasurementScale.SetValueViewCompleted',
    setValueCompletedEventName: 'sensorUnitMeasurementScaleService.SetValueCompleted_Id_',

});

app.constant('sensorTriggerConstant', {

    setAlarmOnApiUri: 'api/sensorTrigger/setAlarmOn',
    setAlarmOnCompletedTopic: 'SensorTrigger.SetAlarmOnViewCompleted',
    setAlarmOnCompletedEventName: 'sensorTriggerService.onSetAlarmOnCompleted_Id_',

    setAlarmCelsiusApiUri: 'api/sensorTrigger/setAlarmCelsius',
    setAlarmCelsiusCompletedTopic: 'SensorTrigger.SetAlarmCelsiusViewCompleted',
    setAlarmCelsiusCompletedEventName: 'sensorTriggerService.onSetAlarmCelsiusCompleted_Id_',

    setAlarmBuzzerOnApiUri: 'api/sensorTrigger/setAlarmBuzzerOn',
    setAlarmBuzzerOnCompletedTopic: 'SensorTrigger.SetAlarmBuzzerOnViewCompleted',
    setAlarmBuzzerOnCompletedEventName: 'sensorTriggerService.SetAlarmBuzzerOnCompleted_Id_',

});