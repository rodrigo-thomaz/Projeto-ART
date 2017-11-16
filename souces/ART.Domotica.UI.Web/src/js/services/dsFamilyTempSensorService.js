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

    var setAlarmOn = function (dsFamilyTempSensorId, alarmOn, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmOn: alarmOn,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmOn', data).then(function (results) {
            return results;
        });
    };

    var setAlarmValue = function (dsFamilyTempSensorId, alarmValue, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmValue: alarmValue,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmValue', data).then(function (results) {
            return results;
        });
    };

    var setAlarmBuzzerOn = function (dsFamilyTempSensorId, alarmBuzzerOn, position) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            alarmBuzzerOn: alarmBuzzerOn,
            position: position,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setAlarmBuzzerOn', data).then(function (results) {
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
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmOnViewCompleted', SetAlarmOnCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmValueViewCompleted', SetAlarmValueCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-DSFamilyTempSensor.SetAlarmBuzzerOnViewCompleted', SetAlarmBuzzerOnCompleted);

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

    var SetAlarmOnCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var SetAlarmValueCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_onSetAlarmValueCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
    }

    var SetAlarmBuzzerOnCompleted = function (payload) {
        var dsFamilyTempSensorId = JSON.parse(payload.body).dsFamilyTempSensorId;
        $rootScope.$emit('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + dsFamilyTempSensorId, JSON.parse(payload.body));
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

    serviceFactory.setAlarmOn = setAlarmOn;
    serviceFactory.setAlarmValue = setAlarmValue;
    serviceFactory.setAlarmBuzzerOn = setAlarmBuzzerOn;    

    serviceFactory.getResolutionById = getResolutionById;

    return serviceFactory;

}]);