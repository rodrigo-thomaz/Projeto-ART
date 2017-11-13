'use strict';
app.factory('temperatureScaleService', ['$http', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};    

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-TemperatureScale.GetAllViewCompleted', onGetAllCompleted);
        if (!initialized) {
            initialized = true;
            getAll();
        }
    }   

    var getAll = function () {
        return $http.post(serviceBase + 'api/temperatureScale/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.scales.push(data[i]);
        }
    }

    EventDispatcher.on('stompService_onConnected', onConnected);               

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.scales = [];  

    return serviceFactory;

}]);