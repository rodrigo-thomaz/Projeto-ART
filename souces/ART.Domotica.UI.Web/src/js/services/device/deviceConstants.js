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

app.constant('deviceDebugConstant', {

    setRemoteEnabledApiUri: 'api/deviceDebug/setRemoteEnabled',
    setRemoteEnabledCompletedTopic: 'DeviceDebug.SetRemoteEnabledViewCompleted',
    setRemoteEnabledCompletedEventName: 'deviceDebugService.onSetRemoteEnabledCompleted_Id_',

    setResetCmdEnabledApiUri: 'api/deviceDebug/setResetCmdEnabled',
    setResetCmdEnabledCompletedTopic: 'DeviceDebug.SetResetCmdEnabledViewCompleted',
    setResetCmdEnabledCompletedEventName: 'deviceDebugService.onSetResetCmdEnabledCompleted_Id_',

    setSerialEnabledApiUri: 'api/deviceDebug/setSerialEnabled',
    setSerialEnabledCompletedTopic: 'DeviceDebug.SetSerialEnabledViewCompleted',
    setSerialEnabledCompletedEventName: 'deviceDebugService.onSetSerialEnabledCompleted_Id_',

    setShowColorsApiUri: 'api/deviceDebug/setShowColors',
    setShowColorsCompletedTopic: 'DeviceDebug.SetShowColorsViewCompleted',
    setShowColorsCompletedEventName: 'deviceDebugService.onSetShowColorsCompleted_Id_',

    setShowDebugLevelApiUri: 'api/deviceDebug/setShowDebugLevel',
    setShowDebugLevelCompletedTopic: 'DeviceDebug.SetShowDebugLevelViewCompleted',
    setShowDebugLevelCompletedEventName: 'deviceDebugService.onSetShowDebugLevelCompleted_Id_',

    setShowProfilerApiUri: 'api/deviceDebug/setShowProfiler',
    setShowProfilerCompletedTopic: 'DeviceDebug.SetShowProfilerViewCompleted',
    setShowProfilerCompletedEventName: 'deviceDebugService.onSetShowProfilerCompleted_Id_',

    setShowTimeApiUri: 'api/deviceDebug/setShowTime',
    setShowTimeCompletedTopic: 'DeviceDebug.SetShowTimeViewCompleted',
    setShowTimeCompletedEventName: 'deviceDebugService.onSetShowTimeCompleted_Id_',

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