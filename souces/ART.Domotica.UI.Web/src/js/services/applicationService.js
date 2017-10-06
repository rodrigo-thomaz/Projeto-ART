'use strict';
app.factory('applicationService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var initialized = false;

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-GetAllCompleted', onGetAllCompleted);
        if (!initialized) {
            initialized = true;
            getAll();
        }
    }   

    var getAll = function () {
        return $http.post(serviceBase + 'api/application/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetAllCompleted = function (payload) {        
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.applications.push(data[i]);
        }
    }

    EventDispatcher.on('stompService_onConnected', onConnected);               

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.applications = [];  

    return serviceFactory;   

}]);