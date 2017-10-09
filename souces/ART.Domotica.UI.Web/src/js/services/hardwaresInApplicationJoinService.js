'use strict';
app.factory('hardwaresInApplicationJoinService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        //stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.GetListCompleted', onGetListCompleted);
    }
        
    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    //serviceFactory.getList = getList;

    return serviceFactory;

}]);