'use strict';
app.factory('termometroStompService', ['$http', '$stomp', '$log', function ($http, $stomp, $log) {

    $stomp.setDebug(function (args) {
        //$log.debug(args)
    })
        
    connect();

    var serviceFactory = {}; 

    serviceFactory.onReadReceived = onReadReceived;
    serviceFactory.setResolution = setResolution;
    serviceFactory.setLowAlarm = setLowAlarm;
    serviceFactory.setHighAlarm = setHighAlarm;    

    return serviceFactory;    

    function connect() {

        return $stomp
            .connect('http://file-server:15674/stomp', JSON.parse('{"login":"test","passcode":"test"}'))
            .then(function (frame) {
                onConnected(frame);
                return frame;
            })
    };

    function onConnected (frame) {
        var subscription = $stomp.subscribe('/topic/ARTPUBTEMP', function (payload, headers, res) {
            onReadReceived(payload);
        }, {
                'headers': 'are awesome'
            })
    }
    
    function onReadReceived(payload) {
        if (serviceFactory.onReadReceived != null) {
            serviceFactory.onReadReceived(payload);
        }        
    }

    function setResolution (deviceAddress, value) {
        var json = {
            'deviceAddress': deviceAddress,
            'value': value,
        };
        $stomp.send('/topic/ART_SET_RESOLUTION', json, {
                priority: 9,
                //custom: 42 // Custom Headers
        })
    }

    function setLowAlarm (deviceAddress, value) {
        var json = {
            deviceAddress: deviceAddress,
            value: value,
        };
        $stomp.send('/topic/ART_SET_LOW_ALARM', json, {
            priority: 9,
            //custom: 42 // Custom Headers
        })
    }

    function setHighAlarm (deviceAddress, value) {
        var json = {
            deviceAddress: deviceAddress,
            value: value,
        };
        $stomp.send('/topic/ART_SET_HIGH_ALARM', json, {
            priority: 9,
            //custom: 42 // Custom Headers
        })
    }
    
}]);