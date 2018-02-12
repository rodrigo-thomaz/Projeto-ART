'use strict';
app.factory('deviceInApplicationService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceInApplicationConstant', 'deviceContext', 'deviceMapper', 'deviceFinder',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceInApplicationConstant, deviceContext, deviceMapper, deviceFinder) {

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var serviceFactory = {};

        var insertCompletedSubscription = null;
        var removeCompletedSubscription = null;

        var onConnected = function () {
            insertCompletedSubscription = stompService.subscribeAllViews(deviceInApplicationConstant.insertCompletedTopic, onInsertCompleted);
            removeCompletedSubscription = stompService.subscribeAllViews(deviceInApplicationConstant.removeCompletedTopic, onRemoveCompleted);
        }

        var insert = function (pin) {
            var data = {
                pin: pin
            };
            return $http.post(serviceBase + deviceInApplicationConstant.insertApiUri, data).then(function successCallback(response) {
                //alert('envio bem sucedido');
            });
        };

        var remove = function (deviceTypeId, deviceDatasheetId, deviceId) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
            };
            return $http.post(serviceBase + deviceInApplicationConstant.removeApiUri, data).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onInsertCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            deviceContext.device.push(data);
            deviceContext.$digest();
            $rootScope.$emit(deviceInApplicationConstant.insertCompletedEventName);
        }

        var onRemoveCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            for (var i = 0; i < deviceContext.device.length; i++) {
                if (deviceContext.device[i].item.deviceTypeId === data.item.deviceTypeId && deviceContext.device[i].item.deviceDatasheetId === data.item.deviceDatasheetId && deviceContext.device[i].deviceId === data.deviceId) {
                    deviceContext.device.splice(i, 1);                   
                    break;
                }
            }
            deviceContext.$digest();
            $rootScope.$emit(deviceInApplicationConstant.removeCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.insert = insert;
        serviceFactory.remove = remove;

        return serviceFactory;

    }]);