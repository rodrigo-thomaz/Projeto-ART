'use strict';
app.factory('hardwaresInApplicationService ', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-HardwaresInApplication.GetListCompleted', onGetListCompleted);
    }

    var getList = function () {
        return $http.post(serviceBase + 'api/hardwaresInApplication/getList').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetListCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        EventDispatcher.trigger('hardwaresInApplicationService_onGetListCompleted', data);
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.getList = getList;

    return serviceFactory;

}]);