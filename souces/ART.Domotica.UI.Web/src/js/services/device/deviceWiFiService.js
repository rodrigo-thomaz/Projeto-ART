'use strict';
app.factory('deviceWiFiService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceWiFiFinder', 'deviceWiFiConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceWiFiFinder, deviceWiFiConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setHostNameCompletedSubscription = null;

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

        var onConnected = function () {
            setHostNameCompletedSubscription = stompService.subscribeAllViews(deviceWiFiConstant.setHostNameCompletedTopic, onSetHostNameCompleted);
        }

        var onSetHostNameCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceWiFi = deviceWiFiFinder.getByKey(result.deviceWiFiId, result.deviceDatasheetId);
            deviceWiFi.hostName = result.hostName;
            deviceContext.$digest();
            $rootScope.$emit(deviceWiFiConstant.setHostNameCompletedEventName + result.deviceWiFiId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setHostNameCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setHostName = setHostName;

        return serviceFactory;

    }]);