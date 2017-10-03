'use strict';
app.factory('temperatureScaleService', ['$http', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var scalesInitialized = false;

    var serviceFactory = {};

    EventDispatcher.on('stompService_onConnected', function (frame) {
        setSubscribes();
    });   

    var setSubscribes = function () {

        if (!stompService.client.connected) return;        

        stompService.client.subscribe('/topic/' + stompService.session + '-GetScalesCompleted', function (payload) {
            var data = JSON.parse(payload.body);
            for (var i = 0; i < data.length; i++) {
                serviceFactory.scales.push(data[i]);
            }
        });

        if (!scalesInitialized) {
            scalesInitialized = true;
            getScales();
        }
    }    

    var getScales = function () {
        return $http.get(serviceBase + 'api/temperatureScale/getScales/' + stompService.session).then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    // stompService
    setSubscribes();

    // serviceFactory

    serviceFactory.scales = [];  

    return serviceFactory;

}]);