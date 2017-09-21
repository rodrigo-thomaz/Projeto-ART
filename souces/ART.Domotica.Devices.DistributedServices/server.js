'use strict';
var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);

var clients = {};

app.get('/', function (req, res) {
    res.send('server is running');
});

//SocketIO vem aqui

http.listen(3000, function () {
    console.log('listening on port 3000');
});

io.on("connection", function (client) {
    console.log('user connected');

    client.on("join", function (deviceId) {
        console.log("Joined: " + deviceId);
        clients[client.id] = deviceId;
        client.emit("update", "You have connected to the server.");
        client.broadcast.emit("update", deviceId + " has joined the server.")
    });

    client.on("sendTemp", function (json) {
        console.log("json: " + json);
        client.broadcast.emit("temp", clients[client.id], json);
    });

    client.on("send", function (msg) {
        console.log("Message: " + msg);
        client.broadcast.emit("chat", clients[client.id], msg);
    });

    client.on("disconnect", function () {
        console.log("Disconnect");
        io.emit("update", clients[client.id] + " has left the server.");
        delete clients[client.id];
    });

});