'use strict';
app.factory('espDeviceJoinService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.GetByPinCompleted', onGetByPinCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.InsertInApplicationViewCompleted', onInsertInApplicationCompleted);        
    }

    var getByPin = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/espDevice/getByPin', data).then(function successCallback(response) {

        }, function errorCallback(response) {

        });
    };  

    var insertInApplication = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/espDevice/insertInApplication', data).then(function successCallback(response) {

        }, function errorCallback(response) {

        });
    };     

    var onGetByPinCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);        
        EventDispatcher.trigger('espDeviceService_onGetByPinCompleted', data);
    }

    var onInsertInApplicationCompleted = function (payload) {
        EventDispatcher.trigger('espDeviceService_onInsertInApplicationCompleted');
    }    

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.getByPin = getByPin;

    serviceFactory.insertInApplication = insertInApplication;    

    return serviceFactory;

}]);