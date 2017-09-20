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

    $scope.cost = 40;
    $scope.range = {
        min: 30,
        max: 60
    };
    $scope.currencyFormatting = function (value) {
        return "$" + value.toString();
    }

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

    function socketioSample() {

        var socket = io.connect("http://localhost:3000");
        var ready = false;
        
        $("#submit").submit(function (e) {
            e.preventDefault();
            $("#nick").fadeOut();
            $("#chat").fadeIn();
            var name = $("#nickname").val();
            var time = new Date();
            $("#name").html(name);
            $("#time").html('First login: ' + time.getHours() + ':' + time.getMinutes());

            ready = true;
            socket.emit("join", name);
        });

        socket.on("update", function (msg) {
            if (ready) {
                $('.chat').append('<li class="info">' + msg + '</li>')
            }
        });

        $("#textarea").keypress(function (e) {
            if (e.which == 13) {
                var text = $("#textarea").val();
                $("#textarea").val('');
                var time = new Date();
                $(".chat").append('<li class="self"><div class="msg"><span>'
                    + $("#nickname").val() + ':</span>    <p>' + text + '</p><time>' +
                    time.getHours() + ':' + time.getMinutes() + '</time></div></li>');
                socket.emit("send", text);
            }
        });

        socket.on("chat", function (client, msg) {
            if (ready) {
                var time = new Date();
                $(".chat").append('<li class="other"><div class="msg"><span>' +
                    client + ':</span><p>' + msg + '</p><time>' + time.getHours() + ':' +
                    time.getMinutes() + '</time></div></li>');
            }
        });
                        
        socket.on("temp", function (client, data) {
            $scope.current = data;
            $scope.data[0].values.push(data);            
            $scope.$apply();
            console.log(data);
        });        
    }

    serviceSample();

    signalRSample();

    socketioSample();

}]);
