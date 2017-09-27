'use strict';
app.factory('mqttService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    
    // so we can use the member attributes inside our functions
    var client = {};

    // initialize attributes
    client._location = ngAuthSettings.wsbrokerUri;
    client._port = ngAuthSettings.wsport;
    client._id = "myclientid_" + parseInt(Math.random() * 100, 10);
    client._client = null;
    client._isConnected = false;
    client._userName = "test";
    client._password = "test";
    client._timeout = 3;
    client._mqttVersion = 3;

    // member functions
    client.init = init;
    client.connect = connect;
    client.disconnect = disconnect;
    client.send = send;
    client.startTrace = startTrace;
    client.stopTrace = stopTrace;
    client.subscribe = subscribe;
    client.unsubscribe = unsubscribe;
    client.onMessageArrived = onMessageArrived;
    client.onMessageDelivered = onMessageDelivered;

    return client;

    // onConnectionLost callback

    function _call(cb, args) {
        if (client._client) {
            cb.apply(this, args);
        } else {
            console.log('mqttService: Client must be initialized first.  Call init() function.');
        }
    }

    function onConnectionLost(resp) {
        console.log("mqttService: Connection lost on ", client._id, ", error code: ", resp);
        client._isConnected = false;
    }

    // connects to the MQTT Server
    function connect(successCallback, failureCallback) {

        var options = {};

        options.onSuccess = successCallback;
        options.onFailure = failureCallback;
        options.userName = client._userName;
        options.password = client._password;
        options.timeout = client._timeout;
        options.mqttVersion = client._mqttVersion;

        _call(_connect, [options]);
    }

    function _connect(options) {
        client._client.connect(options);
        client._isConnected = client._client.isConnected();
    }

    function disconnect() {
        _call(_disconnect);
    }

    function _disconnect() {
        client._client.disconnect();
        client._isConnected = false;
    }

    function init() {
        // initialize attributes
        //client._id = id;

        // create the client and callbacks
        client._client = new Paho.MQTT.Client(client._location, Number(client._port), "/ws", client._id);
        client._client.onConnectionLost = onConnectionLost;
        client._client.onMessageArrived = onMessageArrived;
        client._client.onMessageDelivered = onMessageDelivered;
    }

    function onMessageArrived(message) {
        _call(_onMessageArrived, [message]);
    }

    function _onMessageArrived(message) {
        //console.log("onMessageArrived:" + message.payloadString);
        client.onMessageArrived(message);
    }

    function onMessageDelivered(message) {
        _call(_onMessageDelivered, [message]);
    }

    function _onMessageDelivered(message) {
        //console.log("onMessageArrived:" + message.payloadString);
        client.onMessageDelivered(message);
    }

    function send(message) {
        _call(_send, [message]);
    }

    function _send(message) {
        client._client.send(message);
    }

    function startTrace() {
        _call(_startTrace);
    }

    function _startTrace() {
        client._client.startTrace();
    }

    function stopTrace() {
        _call(_stopTrace);
    }

    function _stopTrace() {
        client._client.stopTrace();
    }

    function subscribe(filter, options) {
        _call(_subscribe, [filter, options]);
    }

    function _subscribe(filter, options) {
        client._client.subscribe(filter, options);
    }

    function unsubscribe(filter, options) {
        _call(_unsubscribe, [filter, options]);
    }

    function _unsubscribe(filter, options) {
        client._client.unsubscribe(filter, options);
    }

}]);