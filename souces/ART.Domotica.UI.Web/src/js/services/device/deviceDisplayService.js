'use strict';
app.factory('deviceDisplayService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceDisplayFinder', 'deviceDisplayConstant',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceContext, deviceDisplayFinder, deviceDisplayConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setEnabledCompletedSubscription = null;

        var setEnabled = function (deviceTypeId, deviceDatasheetId, deviceId, value) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                value: value,
            }
            return $http.post(serviceBase + deviceDisplayConstant.setEnabledApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setEnabledCompletedSubscription = stompService.subscribeAllViews(deviceDisplayConstant.setEnabledCompletedTopic, onSetEnabledCompleted);
        }

        var onSetEnabledCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceDisplay = deviceDisplayFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceDisplay.enabled = result.value;
            deviceContext.$digest();
            $rootScope.$emit(deviceDisplayConstant.setEnabledCompletedEventName + result.deviceId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setEnabledCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory    

        serviceFactory.setEnabled = setEnabled;

        return serviceFactory;

    }]);