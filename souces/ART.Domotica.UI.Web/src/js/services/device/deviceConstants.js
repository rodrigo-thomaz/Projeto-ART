'use strict';
app.constant('deviceConstant', {

    getAllByApplicationIdApiUri: 'api/espDevice/getAllByApplicationId',
    getAllByApplicationIdCompletedTopic: 'ESPDevice.GetAllByApplicationIdViewCompleted',
    getAllByApplicationIdCompletedEventName: 'deviceService.onGetAllByApplicationIdCompleted_Id_',    

    getByPinApiUri: 'api/espDevice/getByPin',
    getByPinCompletedTopic: 'ESPDevice.GetByPinViewCompleted',
    getByPinCompletedEventName: 'deviceService.onGetByPinCompleted',

    setLabelApiUri: 'api/espDevice/setLabel',
    setLabelCompletedTopic: 'ESPDevice.SetLabelViewCompleted',
    setLabelCompletedEventName: 'deviceService.onSetLabelCompleted_Id_',

});

app.constant('deviceInApplicationConstant', {

    insertApiUri: 'api/deviceInApplication/insert',
    insertCompletedTopic: 'DeviceInApplication.InsertViewCompleted',
    insertCompletedEventName: 'deviceInApplicationService.onInsertCompleted',

    removeApiUri: 'api/deviceInApplication/remove',
    removeCompletedTopic: 'DeviceInApplication.RemoveViewCompleted',
    removeCompletedEventName: 'deviceInApplicationService.onRemoveCompleted',

});

app.constant('deviceNTPConstant', {

    setTimeZoneApiUri: 'api/deviceNTP/setTimeZone',
    setTimeZoneCompletedTopic: 'DeviceNTP.SetTimeZoneViewCompleted',
    setTimeZoneCompletedEventName: 'deviceService.onSetTimeZoneCompleted_Id_',

    setUpdateIntervalInMilliSecondApiUri: 'api/deviceNTP/setUpdateIntervalInMilliSecond',
    setUpdateIntervalInMilliSecondCompletedTopic: 'DeviceNTP.SetUpdateIntervalInMilliSecondViewCompleted',
    setUpdateIntervalInMilliSecondCompletedEventName: 'deviceNTPService.onSetUpdateIntervalInMilliSecondCompleted_Id_',   

});

app.constant('deviceSerialConstant', {

    setEnabledApiUri: 'api/deviceSerial/setEnabled',
    setEnabledCompletedTopic: 'DeviceSerial.SetEnabledViewCompleted',
    setEnabledCompletedEventName: 'deviceSerialService.onSetEnabledCompleted_Id_',   

    setPinApiUri: 'api/deviceSerial/setPin',
    setPinCompletedTopic: 'DeviceSerial.SetPinViewCompleted',
    setPinCompletedEventName: 'deviceSerialService.onSetPinCompleted_Id_',   

});

app.constant('deviceWiFiConstant', {

    setHostNameApiUri: 'api/deviceWiFi/setHostName',
    setHostNameCompletedTopic: 'DeviceWiFi.SetHostNameViewCompleted',
    setHostNameCompletedEventName: 'deviceWiFiService.onSetHostNameCompleted_Id_',

    setPublishIntervalInMilliSecondsApiUri: 'api/deviceWiFi/setPublishIntervalInMilliSeconds',
    setPublishIntervalInMilliSecondsCompletedTopic: 'DeviceWiFi.SetPublishIntervalInMilliSecondsViewCompleted',
    setPublishIntervalInMilliSecondsCompletedEventName: 'deviceWiFiService.onSetPublishIntervalInMilliSecondsCompleted_Id_',    

    messageIoTTopic: 'DeviceWiFi.MessageIoT',    
    messageIoTEventName: 'deviceWiFiService.onMessageIoTReceived',    

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

    setReadIntervalInMilliSecondsApiUri: 'api/deviceSensors/setReadIntervalInMilliSeconds',
    setReadIntervalInMilliSecondsCompletedTopic: 'DeviceSensors.SetReadIntervalInMilliSecondsViewCompleted',
    setReadIntervalInMilliSecondsCompletedEventName: 'deviceSensorsService.onSetReadIntervalInMilliSecondsCompleted_Id_',    

    setPublishIntervalInMilliSecondsApiUri: 'api/deviceSensors/setPublishIntervalInMilliSeconds',
    setPublishIntervalInMilliSecondsCompletedTopic: 'DeviceSensors.SetPublishIntervalInMilliSecondsViewCompleted',
    setPublishIntervalInMilliSecondsCompletedEventName: 'deviceSensorsService.onSetPublishIntervalInMilliSecondsCompleted_Id_',    

    messageIoTTopic: 'DeviceSensors.MessageIoT',    
    messageIoTEventName: 'deviceSensorsService.onMessageIoTReceived',  
});

app.constant('sensorInDeviceConstant', {

    setOrdinationApiUri: 'api/sensorInDevice/setOrdination',
    setOrdinationCompletedTopic: 'SensorInDevice.SetOrdinationViewCompleted',
    setOrdinationCompletedEventName: 'sensorInDeviceService.onSetOrdinationCompleted_Id_',

});