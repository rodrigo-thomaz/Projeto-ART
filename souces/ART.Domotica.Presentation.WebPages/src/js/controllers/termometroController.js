'use strict';

app.controller('termometroController', ['$scope', 'termometroService', function ($scope, termometroService) {

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

    function templateSample() {

        $scope.d = [[1, 6.5], [2, 6.5], [3, 7], [4, 8], [5, 7.5], [6, 7], [7, 6.8], [8, 7], [9, 7.2], [10, 7], [11, 6.8], [12, 7]];

        $scope.d0_1 = [[0, 7], [1, 6.5], [2, 12.5], [3, 7], [4, 9], [5, 6], [6, 11], [7, 6.5], [8, 8], [9, 7]];

        $scope.d0_2 = [[0, 4], [1, 4.5], [2, 7], [3, 4.5], [4, 3], [5, 3.5], [6, 6], [7, 3], [8, 4], [9, 3]];

        $scope.d1_1 = [[10, 120], [20, 70], [30, 70], [40, 60]];

        $scope.d1_2 = [[10, 50], [20, 60], [30, 90], [40, 35]];

        $scope.d1_3 = [[10, 80], [20, 40], [30, 30], [40, 20]];

        $scope.d2 = [];

        for (var i = 0; i < 20; ++i) {
            $scope.d2.push([i, Math.round(Math.sin(i) * 100) / 100]);
        }

        $scope.d3 = [
            { label: "iPhone5S", data: 40 },
            { label: "iPad Mini", data: 10 },
            { label: "iPad Mini Retina", data: 20 },
            { label: "iPhone4S", data: 12 },
            { label: "iPad Air", data: 18 }
        ];

        $scope.refreshData = function () {
            $scope.d0_1 = $scope.d0_2;
        };

        $scope.getRandomData = function () {
            var data = [],
                totalPoints = 150;
            if (data.length > 0)
                data = data.slice(1);
            while (data.length < totalPoints) {
                var prev = data.length > 0 ? data[data.length - 1] : 50,
                    y = prev + Math.random() * 10 - 5;
                if (y < 0) {
                    y = 0;
                } else if (y > 100) {
                    y = 100;
                }
                data.push(Math.round(y * 100) / 100);
            }
            // Zip the generated y values with the x values
            var res = [];
            for (var i = 0; i < data.length; ++i) {
                res.push([i, data[i]])
            }
            return res;
        }

        $scope.d4 = $scope.getRandomData();
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

        $scope.medicoesTemp = [];

        //$scope.dataTemp = [[1, 16.5], [2, 16.5], [3, 7], [4, 12], [5, 3.5], [6, 11], [7, 16.8], [8, 19], [9, 17.2], [10, 24], [11, 6.8], [12, 7]];
        //$scope.ticksTemp = [[1, 'Jan'], [2, 'Fev'], [3, 'Mar'], [4, 'Abr'], [5, 'Mai'], [6, 'Jun'], [7, 'Jul'], [8, 'Ago'], [9, 'Set'], [10, 'Out'], [11, 'Nov'], [12, 'Dez']];

        $scope.dataTemp = [];
        $scope.ticksTemp = [];

        socket.on("temp", function (client, data) {

            $scope.dataTemp.push([Number(data.epochTime), data.tempCelsius]);
            $scope.ticksTemp.push([Number(data.epochTime), new Date(data.epochTime * 1000).toLocaleTimeString()]);
            
            $scope.medicoesTemp.push(data);

            console.log(data);
        });

    }

    serviceSample();

    templateSample();

    signalRSample();

    socketioSample();

}]);
