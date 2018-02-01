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

            stompService.client.subscribe(deviceWiFiConstant.messageIoTTopic, onMessageIoTReceived);
        }

        var onMessageIoTReceived = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            var deviceWiFi = deviceWiFiFinder.getByKey(data.deviceId, data.deviceDatasheetId);
            deviceWiFi.epochTimeUtc = data.epochTimeUtc;
            deviceWiFi.wifiQuality = data.wifiQuality;
            deviceWiFi.localIPAddress = data.localIPAddress;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.messageIoTEventName + data.deviceId, data);
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