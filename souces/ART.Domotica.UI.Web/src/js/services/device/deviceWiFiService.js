'use strict';
app.factory('deviceWiFiService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceWiFiFinder', 'deviceWiFiConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceWiFiFinder, deviceWiFiConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setHostNameCompletedSubscription = null;
        var setPublishIntervalInMilliSecondsCompletedSubscription = null;

        var setHostName = function (deviceWiFiId, deviceDatasheetId, hostName) {
            var data = {
                deviceWiFiId: deviceWiFiId,
                deviceDatasheetId: deviceDatasheetId,
                hostName: hostName,
            }
            return $http.post(serviceBase + deviceWiFiConstant.setHostNameApiUri, data).then(function (results) {
                return results;
            });
        };

        var setPublishIntervalInMilliSeconds = function (deviceWiFiId, deviceDatasheetId, publishIntervalInMilliSeconds) {
            var data = {
                deviceId: deviceWiFiId,
                deviceDatasheetId: deviceDatasheetId,
                intervalInMilliSeconds: publishIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceWiFiConstant.setPublishIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setHostNameCompletedSubscription = stompService.subscribeAllViews(deviceWiFiConstant.setHostNameCompletedTopic, onSetHostNameCompleted);
            setPublishIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceWiFiConstant.setPublishIntervalInMilliSecondsCompletedTopic, onSetPublishIntervalInMilliSecondsCompleted);

            stompService.client.subscribe(deviceWiFiConstant.topicMessageIoT, onMessageIoTReceived);
        }

        var onMessageIoTReceived = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            //for (var i = 0; i < deviceContext.device.length; i++) {
            //    var device = deviceContext.device[i];
            //    if (device.deviceId === data.deviceId) {
            //        device.epochTimeUtc = data.epochTimeUtc;
            //        device.wifiQuality = data.wifiQuality;
            //        device.localIPAddress = data.localIPAddress;
                    //updateSensors(device, data.sensorTempDSFamilies);
            //        break;
            //    }
            //}
            deviceContext.$digest();
            $rootScope.$emit('deviceWiFiService_onMessageIoTReceived');
        }

        var onSetHostNameCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceWiFi = deviceWiFiFinder.getByKey(result.deviceWiFiId, result.deviceDatasheetId);
            deviceWiFi.hostName = result.hostName;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.setHostNameCompletedEventName + result.deviceWiFiId, result);
        };

        var onSetPublishIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceWiFi = deviceWiFiFinder.getByKey(result.deviceId, result.deviceDatasheetId);
            deviceWiFi.publishIntervalInMilliSeconds = result.publishIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.setPublishIntervalInMilliSecondsCompletedEventName + result.deviceId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setHostNameCompletedSubscription.unsubscribe();
            setPublishIntervalInMilliSecondsCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setHostName = setHostName;
        serviceFactory.setPublishIntervalInMilliSeconds = setPublishIntervalInMilliSeconds;

        return serviceFactory;

    }]);