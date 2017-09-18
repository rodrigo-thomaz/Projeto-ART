'use strict';
app.controller('homeController', ['$scope', 'homeService', function ($scope, homeService) {

    $scope.medicoesTemp = [];

    $scope.lista = [];

    homeService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    });

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

    $(document).ready(function () {

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
            if (ready) {
                $scope.medicoesTemp.push(data);
                console.log(data);
            }
        });

    });

   


}]);