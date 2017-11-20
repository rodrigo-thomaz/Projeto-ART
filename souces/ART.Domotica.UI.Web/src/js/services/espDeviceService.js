'use strict';
app.factory('espDeviceService', ['$http', '$log', 'ngAuthSettings', 'EventDispatcher', 'stompService', function ($http, $log, ngAuthSettings, EventDispatcher, stompService) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var initialized = false;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.GetListInApplicationViewCompleted', onGetListInApplicationCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.InsertInApplicationViewCompleted', onInsertInApplicationCompleted);        
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.DeleteFromApplicationViewCompleted', onDeleteFromApplicationCompleted);
        stompService.client.subscribe('/topic/' + stompService.session + '-ESPDevice.GetByPinViewCompleted', onGetByPinCompleted);        

        stompService.client.subscribe('/topic/ARTPUBTEMP', onReadReceived);

        if (!initialized) {
            initialized = true;
            getListInApplication();
        }
    }

    var getListInApplication = function () {
        return $http.post(serviceBase + 'api/espDevice/getListInApplication').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var getByPin = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/espDevice/getByPin', data).then(function successCallback(response) {
            //alert('envio bem sucedido');
        });
    };  

    var insertInApplication = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + 'api/espDevice/insertInApplication', data).then(function successCallback(response) {
            //alert('envio bem sucedido');
        });
    };     

    var deleteFromApplication = function (deviceInApplicationId) {
        var data = {
            deviceInApplicationId: deviceInApplicationId
        };
        return $http.post(serviceBase + 'api/espDevice/deleteFromApplication', data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };  

    var getDeviceById = function (deviceId) {
        for (var i = 0; i < serviceFactory.devices.length; i++) {
            if (serviceFactory.devices[i].deviceId === deviceId) {
                return serviceFactory.devices[i];
            }
        }
    }; 

    var onReadReceived = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < serviceFactory.devices.length; i++) {
            var device = serviceFactory.devices[i];
            if (device.deviceInApplicationId === data.deviceInApplicationId) {
                device.epochTimeUtc = data.epochTimeUtc;
                device.wifiQuality = data.wifiQuality;
                updateSensors(device, data.dsFamilyTempSensors);
                break;
            }
        } 
    }

    var updateSensors = function (device, newSensors) {
        var oldSensors = device.sensors;
        for (var i = 0; i < oldSensors.length; i++) {
            for (var j = 0; j < newSensors.length; j++) {
                if (oldSensors[i].dsFamilyTempSensorId === newSensors[j].dsFamilyTempSensorId) {
                    oldSensors[i].isConnected = newSensors[j].isConnected;
                    oldSensors[i].tempCelsius = newSensors[j].tempCelsius;

                    //Chart

                    oldSensors[i].chart[1].key = 'Temperatura ' + oldSensors[i].tempCelsius + ' °C';

                    oldSensors[i].chart[0].values.push({
                        epochTime: device.epochTimeUtc,
                        temperature: oldSensors[i].highAlarm.alarmCelsius,
                    });

                    oldSensors[i].chart[1].values.push({
                        epochTime: device.epochTimeUtc,
                        temperature: oldSensors[i].tempCelsius,
                    });

                    oldSensors[i].chart[2].values.push({
                        epochTime: device.epochTimeUtc,
                        temperature: oldSensors[i].lowAlarm.alarmCelsius,
                    });
                    
                    if (oldSensors[i].chart[0].values.length > 60)
                        oldSensors[i].chart[0].values.shift();

                    if (oldSensors[i].chart[1].values.length > 60)
                        oldSensors[i].chart[1].values.shift();

                    if (oldSensors[i].chart[2].values.length > 60)
                        oldSensors[i].chart[2].values.shift();

                    break;
                }
            }
        }
    }

    var chartLine = function (key) {
        this.key = key;
        this.values = [];
    }

    var onGetListInApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            var device = data[i];
            device.createDate = new Date(device.createDate * 1000).toLocaleString();
            serviceFactory.devices.push(device);
            for (var j = 0; j < device.sensors.length; j++) {
                var sensor = device.sensors[j];
                sensor.chart = [];
                sensor.chart.push(new chartLine("Máximo"));
                sensor.chart.push(new chartLine("Temperatura"));
                sensor.chart.push(new chartLine("Mínimo")); 
            }
        }
    }

    var onGetByPinCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        EventDispatcher.trigger('espDeviceService_onGetByPinCompleted', data);
    }

    var onInsertInApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        data.createDate = new Date(data.createDate * 1000).toLocaleString();
        serviceFactory.devices.push(data);
        EventDispatcher.trigger('espDeviceService_onInsertInApplicationCompleted');
    }  

    var onDeleteFromApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < serviceFactory.devices.length; i++) {
            if (serviceFactory.devices[i].deviceInApplicationId === data.deviceInApplicationId) {
                serviceFactory.devices.splice(i, 1);
                EventDispatcher.trigger('espDeviceService_onDeleteFromApplicationCompleted');
                break;
            }
        }       
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    // stompService
    if (stompService.client.connected)
        onConnected();

    // serviceFactory

    serviceFactory.getByPin = getByPin;
    serviceFactory.getDeviceById = getDeviceById;
    serviceFactory.insertInApplication = insertInApplication;    
    serviceFactory.deleteFromApplication = deleteFromApplication;

    serviceFactory.devices = [];  

    return serviceFactory;

}]);