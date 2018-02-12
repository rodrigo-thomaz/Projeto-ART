'use strict';
app.factory('deviceWiFiService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceWiFiFinder', 'deviceWiFiConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceWiFiFinder, deviceWiFiConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var messageIoTReceivedSubscription = null;
        var setHostNameCompletedSubscription = null;
        var setPublishIntervalInMilliSecondsCompletedSubscription = null;

        var setHostName = function (deviceTypeId, deviceDatasheetId, deviceId, hostName) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                hostName: hostName,
            }
            return $http.post(serviceBase + deviceWiFiConstant.setHostNameApiUri, data).then(function (results) {
                return results;
            });
        };

        var setPublishIntervalInMilliSeconds = function (deviceTypeId, deviceDatasheetId, deviceId, publishIntervalInMilliSeconds) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                intervalInMilliSeconds: publishIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceWiFiConstant.setPublishIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            messageIoTReceivedSubscription = stompService.subscribeDevice(deviceWiFiConstant.messageIoTTopic, onMessageIoTReceived);            
            setHostNameCompletedSubscription = stompService.subscribeAllViews(deviceWiFiConstant.setHostNameCompletedTopic, onSetHostNameCompleted);
            setPublishIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceWiFiConstant.setPublishIntervalInMilliSecondsCompletedTopic, onSetPublishIntervalInMilliSecondsCompleted);            
        }

        var onMessageIoTReceived = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            var deviceWiFi = deviceWiFiFinder.getByKey(data.deviceTypeId, data.deviceDatasheetId, data.deviceId);
            if(angular.isUndefined(deviceWiFi)) return;
            deviceWiFi.epochTimeUtc = data.epochTimeUtc;
            deviceWiFi.wifiQuality = data.wifiQuality;
            deviceWiFi.localIPAddress = data.localIPAddress;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.messageIoTEventName + data.deviceId, data);
        }

        var onSetHostNameCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceWiFi = deviceWiFiFinder.getByKey(data.deviceTypeId, data.deviceDatasheetId, data.deviceId);
            deviceWiFi.hostName = result.hostName;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.setHostNameCompletedEventName + result.deviceWiFiId, result);
        };

        var onSetPublishIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceWiFi = deviceWiFiFinder.getByKey(data.deviceTypeId, data.deviceDatasheetId, data.deviceId);
            deviceWiFi.publishIntervalInMilliSeconds = result.publishIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.setPublishIntervalInMilliSecondsCompletedEventName + result.deviceId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            messageIoTReceivedSubscription.unsubscribe();
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