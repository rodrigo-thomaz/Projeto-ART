'use strict';
app.factory('temperatureScaleService', ['$http', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};    

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-GetScalesCompleted', onGetScalesCompleted);
        if (!initialized) {
            initialized = true;
            getScales();
        }
    }   

    var getScales = function () {
        return $http.get(serviceBase + 'api/temperatureScale/getScales/' + stompService.session).then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var onGetScalesCompleted = function (payload) {
        var data = JSON.parse(payload.body);
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