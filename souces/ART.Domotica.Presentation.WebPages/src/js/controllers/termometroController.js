'use strict';

app.controller('termometroController', ['$scope', 'termometroService', function ($scope, termometroService) {    

    $scope.current = null;

    $scope.resolution = {};

    $scope.resolutions = [
        { mode: '9 bits', resolution: '0.5°C', conversionTime: '93.75 ms', value: 9 },
        { mode: '10 bits', resolution: '0.25°C', conversionTime: '187.5 ms', value: 10 },
        { mode: '11 bits', resolution: '0.125°C', conversionTime: '375 ms', value: 11 },
        { mode: '12 bits', resolution: '0.0625°C', conversionTime: '750 ms', value: 12 },        
    ];

    $scope.options = {
        chart: {
            type: 'lineChart',
            height: 240,
            margin: {
                top: 20,
                right: 30,
                bottom: 35,
                left: 40
            },
            x: function (d) { return d.epochTime; },
            y: function (d) { return d.tempCelsius; },
            useInteractiveGuideline: true,
            duration: 500,
            xAxis: {
                //axisLabel: 'Tempo',
                tickFormat: function (d) {
                    return new Date(d * 1000).toLocaleTimeString();
                },
                tickPadding: 18,
                axisLabelDistance: 0,
            },
            yAxis: {
                //axisLabel: 'Temperatura',
                tickFormat: function (d) {
                    return d3.format('.02f')(d);
                },
                tickPadding: 5,
                axisLabelDistance: 0,
            },  
            forceY: [15, 35],
        }
    };

    $scope.data = [{
        key: "Temperatura",
        values: []
    }]

    $scope.range = {
        min: 30,
        max: 60
    };

    var wsbroker = "file-server";  // mqtt websocket enabled broker

    var wsport = 15675; // port for above
    var client = new Paho.MQTT.Client(wsbroker, wsport, "/ws",
        "myclientid_" + parseInt(Math.random() * 100, 10));
    client.onConnectionLost = function (responseObject) {
        console.log("CONNECTION LOST - " + responseObject.errorMessage);
    };
    client.onMessageArrived = function (message) {
        switch (message.destinationName) {
            case "ARTPUBTEMP":
                var sensors = JSON.parse(message.payloadString);

                $scope.current = sensors[0];
                $scope.data[0].values.push(sensors[0]);
                console.log(sensors[0]);
                $scope.$apply();

                break;
            default:
        }
    };
    client.onMessageDelivered = function(message) {
        console.log(message);
    };
    var options = {
        userName: 'test',
        password: 'test',
        timeout: 3,
        onSuccess: function () {
            console.log("CONNECTION SUCCESS");
            client.subscribe('ARTPUBTEMP', { qos: 1 });
        },
        onFailure: function (message) {
            console.log("CONNECTION FAILURE - " + message.errorMessage);
        },
        mqttVersion: 3
    };
    
    console.log("CONNECT TO " + wsbroker + ":" + wsport);
    client.connect(options);


    function serviceSample() {

        $scope.lista = [];

        termometroService.get().then(function (results) {

            $scope.lista = results.data;

        }, function (error) {
            if (error.status !== 401) {
                alert(error.data.message);
            }
        });
    }    

    function signalRSample() {

        (function () {

            var deviceHub = $.connection.deviceHub;

            $.connection.hub.logging = true;
            $.connection.hub.start()
                .done(function () {
                    deviceHub.server.sendMessage('vai!!!');
                })
                .fail(function () {
                    alert('erro no start');
                });

            deviceHub.client.hello = function () {
                alert();
            }

            deviceHub.client.registerMessage = function (message) {
                alert(message);
            };

        }());
    }   

    serviceSample();

    signalRSample();

}]);
