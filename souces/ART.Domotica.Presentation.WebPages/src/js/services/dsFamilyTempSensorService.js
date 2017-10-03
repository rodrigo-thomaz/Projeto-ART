'use strict';
app.factory('dsFamilyTempSensorService', ['$http', 'ngAuthSettings', 'stompService', function ($http, ngAuthSettings, stompService) {

    //var _serviceBase = ngAuthSettings.distributedServicesUri;
    var serviceBase = "http://localhost:47039/";

    var resolutionsInitialized = false;

    var serviceFactory = {};

    var onConnected = function (frame) {
        setSubscribes();
    }

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

        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 9, mode: '9 bits', resolution: '0.5°C', conversionTime: '93.75 ms', value: 9 });
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 10, mode: '10 bits', resolution: '0.25°C', conversionTime: '187.5 ms', value: 10 });
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 11, mode: '11 bits', resolution: '0.125°C', conversionTime: '375 ms', value: 11 });
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 12, mode: '12 bits', resolution: '0.0625°C', conversionTime: '750 ms', value: 12 });        
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
    stompService.onConnected = onConnected;
    setSubscribes();

    // serviceFactory

    serviceFactory.resolutions = [];
    serviceFactory.setResolution = setResolution;
    serviceFactory.setHighAlarm = setHighAlarm;
    serviceFactory.setLowAlarm = setLowAlarm;    

    return serviceFactory;

}]);