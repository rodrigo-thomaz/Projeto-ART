'use strict';
app.constant('deviceConstant', {

    getAllByApplicationIdApiUri: 'api/espDevice/getAllByApplicationId',
    getAllByApplicationIdCompletedTopic: 'ESPDevice.GetAllByApplicationIdViewCompleted',
    getAllByApplicationIdCompletedEventName: 'deviceService.onGetAllByApplicationIdCompleted_Id_',

    insertInApplicationApiUri: 'api/espDevice/insertInApplication',
    insertInApplicationCompletedTopic: 'ESPDevice.InsertInApplicationViewCompleted',
    insertInApplicationCompletedEventName: 'deviceService.onInsertInApplicationCompleted',

    deleteFromApplicationApiUri: 'api/espDevice/deleteFromApplication',
    deleteFromApplicationCompletedTopic: 'ESPDevice.DeleteFromApplicationViewCompleted',
    deleteFromApplicationCompletedEventName: 'deviceService.onDeleteFromApplicationCompleted',

    getByPinApiUri: 'api/espDevice/getByPin',
    getByPinCompletedTopic: 'ESPDevice.GetByPinViewCompleted',
    getByPinCompletedEventName: 'deviceService.onGetByPinCompleted',

    setLabelApiUri: 'api/espDevice/setLabel',
    setLabelCompletedTopic: 'ESPDevice.SetLabelViewCompleted',
    setLabelCompletedEventName: 'deviceService.onSetLabelCompleted_Id_',

});

app.constant('deviceNTPConstant', {

    setTimeZoneApiUri: 'api/deviceNTP/setTimeZone',
    setTimeZoneCompletedTopic: 'DeviceNTP.SetTimeZoneViewCompleted',
    setTimeZoneCompletedEventName: 'deviceService.onSetTimeZoneCompleted_Id_',

    setUpdateIntervalInMilliSecondApiUri: 'api/deviceNTP/setUpdateIntervalInMilliSecond',
    setUpdateIntervalInMilliSecondCompletedTopic: 'DeviceNTP.SetUpdateIntervalInMilliSecondViewCompleted',
    setUpdateIntervalInMilliSecondCompletedEventName: 'deviceNTPService.onSetUpdateIntervalInMilliSecondCompleted_Id_',   

});

app.constant('deviceWiFiConstant', {

    setHostNameApiUri: 'api/deviceWiFi/setHostName',
    setHostNameCompletedTopic: 'DeviceWiFi.SetHostNameViewCompleted',
    setHostNameCompletedEventName: 'deviceWiFiService.onSetHostNameCompleted_Id_',

});

app.constant('deviceSensorsConstant', {

    setPublishIntervalInSecondsApiUri: 'api/deviceSensors/setPublishIntervalInSeconds',
    setPublishIntervalInSecondsCompletedTopic: 'DeviceSensors.SetPublishIntervalInSecondsViewCompleted',
    setPublishIntervalInSecondsCompletedEventName: 'deviceSensorsService.onSetPublishIntervalInSecondsCompleted_Id_',    

});

app.constant('sensorInDeviceConstant', {

    setOrdinationApiUri: 'api/sensorInDevice/setOrdination',
    setOrdinationCompletedTopic: 'SensorInDevice.SetOrdinationViewCompleted',
    setOrdinationCompletedEventName: 'sensorInDeviceService.onSetOrdinationCompleted_Id_',

});