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