'use strict';
app.factory('dsFamilyTempSensorService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initResolutions = false;

    var serviceFactory = {};    

    var getAll = function (applicationId) {
        var data = {
            applicationId: applicationId,
            session: stompService.session,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/getAll', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var getResolutions = function () {
        var data = {
            session: stompService.session,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/getResolutions', data).then(function (results) {
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

    var onConnected = function () {

        stompService.client.subscribe('/topic/ARTPUBTEMP', onReadReceived);
        stompService.client.subscribe('/topic/' + stompService.session + '-GetResolutionsCompleted', onGetResolutionsCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-GetAllCompleted', onGetAllCompleted);

        if (!initResolutions) {
            initResolutions = true;
            getResolutions();
        }

        /////////////////////////////////////////////
        getAll('4ee0c742-b8a4-e711-9bee-707781d470bc');
    }  

    var onReadReceived = function (payload) {
        EventDispatcher.trigger('dsFamilyTempSensorService_onReadReceived', JSON.parse(payload.body));
    }

    var onGetAllCompleted = function (payload) {
        var data = JSON.parse(payload.body);        
    }

    var onGetResolutionsCompleted = function (payload) {
        var data = JSON.parse(payload.body);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.resolutions.push(data[i]);
        }
    }

    EventDispatcher.on('stompService_onConnected', onConnected);         

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.resolutions = [];
    serviceFactory.getAll = getAll;
    serviceFactory.setResolution = setResolution;
    serviceFactory.setHighAlarm = setHighAlarm;
    serviceFactory.setLowAlarm = setLowAlarm;    

    return serviceFactory;

}]);