'use strict';
app.factory('termometroMQTTService', ['$http', 'mqttService', function ($http, mqttService) {

    // mqttService

    mqttService.init();    

    mqttService.onSuccess = onSuccess;
    mqttService.onFailure = onFailure;
    mqttService.onMessageArrived = onMessageArrived;
    mqttService.onMessageDelivered = onMessageDelivered;

    mqttService.connect();

    // serviceFactory

    var serviceFactory = {};
        
    serviceFactory.onTemperatureReceived = onTemperatureReceived;
    serviceFactory.setResolution = setResolution;
    serviceFactory.setLowAlarm = setLowAlarm;
    serviceFactory.setHighAlarm = setHighAlarm;

    return serviceFactory;

    // codes

    function onSuccess() {

        mqttService.subscribe('ARTPUBTEMP', { qos: 1 });

        var message = new Paho.MQTT.Message("Hello");
        message.destinationName = "/World";
        mqttService.send(message);
    }

    function onFailure(message) {
        console.log("CONNECTION FAILURE - " + message.errorMessage);
    }

    function onMessageArrived(message) {
        switch (message.destinationName) {
            case "ARTPUBTEMP":
                raiseTemperatureReceived(message.payloadString);
                break;
            default:
        }
    }

    function onMessageDelivered(message) {
        console.log("messageDeliveredCallback !!!");
    }    

    function raiseTemperatureReceived(payloadString) {
        //console.log("temperatureReceivedCallback:" + payloadString);
        var sensors = JSON.parse(payloadString);
        serviceFactory.onTemperatureReceived(sensors);
    }

    function onTemperatureReceived(sonsors) {
        //console.log("onTemperatureReceived:" + sensors);
    }

    function setResolution(deviceAddress, value) {
        var json = {
            deviceAddress: deviceAddress,
            value: value,
        };        
        var message = new Paho.MQTT.Message(JSON.stringify(json));
        message.destinationName = "ART_SET_RESOLUTION";
        mqttService.send(message);
    }

    function setLowAlarm(deviceAddress, value) {
        var json = {
            deviceAddress: deviceAddress,
            value: value,
        };
        var message = new Paho.MQTT.Message(JSON.stringify(json));
        message.destinationName = "ART_SET_LOW_ALARM";
        mqttService.send(message);
    }

    function setHighAlarm(deviceAddress, value) {
        var json = {
            deviceAddress: deviceAddress,
            value: value,
        };
        var message = new Paho.MQTT.Message(JSON.stringify(json));
        message.destinationName = "ART_SET_HIGH_ALARM";
        mqttService.send(message);
    }
}]);