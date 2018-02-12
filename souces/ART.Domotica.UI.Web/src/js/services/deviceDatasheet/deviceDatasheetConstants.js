'use strict';
app.constant('deviceTypeConstant', {
    getAllApiUri: 'api/deviceType/getAll',
    getAllCompletedTopic: 'DeviceType.GetAllViewCompleted',
    getAllCompletedEventName: 'deviceTypeService.GetAllCompleted',
});

app.constant('deviceDatasheetConstant', {
    getAllApiUri: 'api/deviceDatasheet/getAll',
    getAllCompletedTopic: 'DeviceDatasheet.GetAllViewCompleted',
    getAllCompletedEventName: 'deviceDatasheetService.GetAllCompleted',
});