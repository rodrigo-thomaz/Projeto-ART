'use strict';

app.controller('termometroController', ['$scope', 'termometroService', function ($scope, termometroService) {

    $scope.options = {
        chart: {
            type: 'discreteBarChart',
            height: 240,
            margin: {
                top: 20,
                right: 20,
                bottom: 60,
                left: 55
            },
            x: function (d) { return d.label; },
            y: function (d) { return d.value; },
            showValues: true,
            valueFormat: function (d) {
                return d3.format(',.4f')(d);
            },
            transitionDuration: 500,
            xAxis: {
                axisLabel: 'X Axis'
            },
            yAxis: {
                axisLabel: 'Y Axis',
                axisLabelDistance: 30
            }
        }
    };

    $scope.data = [{
        key: "Temperatura",
        values: []
    }]

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

            $scope.data[0].values.push({ "label": new Date(data.epochTime * 1000).toLocaleTimeString(), "value": data.tempCelsius });
            
            $scope.$apply();

            console.log(data);
        });        
    }

    serviceSample();

    signalRSample();

    socketioSample();

}]);
