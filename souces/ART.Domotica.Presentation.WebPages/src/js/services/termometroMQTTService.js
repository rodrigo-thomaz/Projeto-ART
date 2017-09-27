'use strict';
app.factory('termometroMQTTService', ['$http', 'mqttService', function ($http, mqttService) {

    // mqttService

    mqttService.init();

    mqttService.connect(successCallback, failureCallback);

    mqttService.onMessageArrived = messageArrivedCallback;
    mqttService.onMessageDelivered = messageDeliveredCallback;

    // serviceFactory

    var serviceFactory = {};
                
    serviceFactory.onTemperatureReceived = onTemperatureReceived;
    
    return serviceFactory;

    // codes

    function successCallback() {

        mqttService.subscribe('ARTPUBTEMP', { qos: 1 });

        //message = new Paho.MQTT.Message("Hello");
        //message.destinationName = "/World";
        //mqttService.send(message);
    }

    function failureCallback(message) {
        console.log("CONNECTION FAILURE - " + message.errorMessage);
    }

    function messageArrivedCallback(message) {
        switch (message.destinationName) {
            case "ARTPUBTEMP":
                temperatureReceivedCallback(message.payloadString);
                break;
            default:
        }
    }

    function messageDeliveredCallback(message) {
        console.log("messageDeliveredCallback !!!");
    }    

    function temperatureReceivedCallback(payloadString) {
        //console.log("temperatureReceivedCallback:" + payloadString);
        var sensors = JSON.parse(payloadString);
        serviceFactory.onTemperatureReceived(sensors);
    }

    function onTemperatureReceived(sonsors) {
        //console.log("onTemperatureReceived:" + sensors);
    }

}]);