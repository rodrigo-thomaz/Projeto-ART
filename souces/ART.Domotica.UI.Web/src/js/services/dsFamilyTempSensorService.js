'use strict';
app.factory('dsFamilyTempSensorService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, $rootScope, ngAuthSettings, EventDispatcher, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};    
    
    var getAllResolutions = function () {
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/getAllResolutions').then(function (results) {
            //alert('envio bem sucedido');
        });
    };
    
    var setScale = function (dsFamilyTempSensorId, temperatureScaleId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            temperatureScaleId: temperatureScaleId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setScale', data).then(function (results) {
            return results;
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

    var getResolutionById = function (dsFamilyTempSensorResolutionId) {
        for (var i = 0; i < serviceFactory.resolutions.length; i++) {
            if (serviceFactory.resolutions[i].id === dsFamilyTempSensorResolutionId) {
                return serviceFactory.resolutions[i];
            }
        }
    };

    var onConnected = function () {

        stompService.client.subscribe('/topic/ARTPUBTEMP', onReadReceived);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.GetAllResolutionsViewCompleted', onGetAllResolutionsCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetScaleViewCompleted', onSetScaleCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetResolutionViewCompleted', onSetResolutionCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetLowAlarmViewCompleted', onSetLowAlarmCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetHighAlarmViewCompleted', onSetHighAlarmCompleted);

        if (!initialized) {
            initialized = true;
            getAllResolutions();
        }
    }  

    var onReadReceived = function (payload) {
        EventDispatcher.trigger('dsFamilyTempSensorService_onReadReceived', JSON.parse(payload.body));
    }
    
    var onGetAllResolutionsCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.resolutions.push(data[i]);
        }
    }

    var onSetScaleCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var onSetResolutionCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var onSetLowAlarmCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetLowAlarmCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var onSetHighAlarmCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetHighAlarmCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    EventDispatcher.on('stompService_onConnected', onConnected);         

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.resolutions = [];
    serviceFactory.sensors = [];

    serviceFactory.setScale = setScale;
    serviceFactory.setResolution = setResolution;
    serviceFactory.setHighAlarm = setHighAlarm;
    serviceFactory.setLowAlarm = setLowAlarm;    

    serviceFactory.getResolutionById = getResolutionById;

    return serviceFactory;

}]);