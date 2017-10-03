'use strict';
app.factory('dsFamilyTempSensorService', ['$http', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var resolutionsInitialized = false;

    var serviceFactory = {};

    EventDispatcher.on('stompService_onConnected', function (frame) {
        setSubscribes();
    });   

    var setSubscribes = function () {

        if (!stompService.client.connected) return;

        stompService.client.subscribe('/topic/ARTPUBTEMP', onReadReceived);

        stompService.client.subscribe('/topic/' + stompService.session + '-GetResolutionsCompleted', function (payload) {
            var data = JSON.parse(payload.body);
            for (var i = 0; i < data.length; i++) {
                serviceFactory.resolutions.push(data[i]);
            }
        });

        if (!resolutionsInitialized) {
            resolutionsInitialized = true;
            getResolutions();
        }
    }    

    var getResolutions = function () {
        return $http.get(serviceBase + 'api/dsFamilyTempSensor/getResolutions/' + stompService.session).then(function (results) {
            //alert('envio bem sucedido');
        });
    };
    
    var setResolution = function (dsFamilyTempSensorId, dsFamilyTempSensorResolutionId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            dsFamilyTempSensorResolutionId: dsFamilyTempSensorResolutionId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setResolution', data).then(function (results) {
            return results;
        });
    };

    var setHighAlarm = function (dsFamilyTempSensorId, highAlarm) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            highAlarm: highAlarm,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setHighAlarm', data).then(function (results) {
            return results;
        });
    };

    var setLowAlarm = function (dsFamilyTempSensorId, lowAlarm) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            lowAlarm: lowAlarm,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setLowAlarm', data).then(function (results) {
            return results;
        });
    };

    var onReadReceived = function (payload) {
        if (serviceFactory.onReadReceived != null) {
            var sensors = JSON.parse(payload.body)
            serviceFactory.onReadReceived(sensors);
        }
    }

    // stompService
    setSubscribes();

    // serviceFactory

    serviceFactory.resolutions = [];
    serviceFactory.setResolution = setResolution;
    serviceFactory.setHighAlarm = setHighAlarm;
    serviceFactory.setLowAlarm = setLowAlarm;    

    return serviceFactory;

}]);