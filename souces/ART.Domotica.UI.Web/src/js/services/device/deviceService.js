'use strict';
app.factory('deviceService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceConstant', 'deviceContext', 'deviceMapper', 'deviceFinder',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceConstant, deviceContext, deviceMapper, deviceFinder) {

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var serviceFactory = {};

        var _initializing = false;
        var _initialized = false;

        var getAllByApplicationIdCompletedSubscription = null;
        var insertInApplicationCompletedSubscription = null;
        var deleteFromApplicationCompletedSubscription = null;
        var getByPinCompletedSubscription = null;
        var setLabelCompletedSubscription = null;

        var onConnected = function () {

            getAllByApplicationIdCompletedSubscription = stompService.subscribeView(deviceConstant.getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);
            insertInApplicationCompletedSubscription = stompService.subscribeAllViews(deviceConstant.insertInApplicationCompletedTopic, onInsertInApplicationCompleted);
            deleteFromApplicationCompletedSubscription = stompService.subscribeAllViews(deviceConstant.deleteFromApplicationCompletedTopic, onDeleteFromApplicationCompleted);
            getByPinCompletedSubscription = stompService.subscribeView(deviceConstant.getByPinCompletedTopic, onGetByPinCompleted);
            setLabelCompletedSubscription = stompService.subscribeAllViews(deviceConstant.setLabelCompletedTopic, onSetLabelCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAllByApplicationId();
            }
        }

        var initialized = function () {
            return _initialized;
        };

        var getAllByApplicationId = function () {
            return $http.post(serviceBase + deviceConstant.getAllByApplicationIdApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var getByPin = function (pin) {
            var data = {
                pin: pin
            };
            return $http.post(serviceBase + deviceConstant.getByPinApiUri, data).then(function successCallback(response) {
                //alert('envio bem sucedido');
            });
        };

        var insertInApplication = function (pin) {
            var data = {
                pin: pin
            };
            return $http.post(serviceBase + deviceConstant.insertInApplicationApiUri, data).then(function successCallback(response) {
                //alert('envio bem sucedido');
            });
        };

        var deleteFromApplication = function (deviceId, deviceDatasheetId) {
            var data = {
                deviceId: deviceId,
                deviceDatasheetId: deviceDatasheetId,
            };
            return $http.post(serviceBase + deviceConstant.deleteFromApplicationApiUri, data).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var setLabel = function (deviceId, deviceDatasheetId, label) {
            var data = {
                deviceId: deviceId,
                deviceDatasheetId: deviceDatasheetId,
                label: label,
            }
            return $http.post(serviceBase + deviceConstant.setLabelApiUri, data).then(function (results) {
                return results;
            });
        };                

        var onGetAllByApplicationIdCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                deviceContext.device.push(data[i]);
            }

            deviceContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllByApplicationIdCompletedSubscription.unsubscribe();

            $rootScope.$emit(deviceConstant.getAllByApplicationIdCompletedEventName);
        }

        var onGetByPinCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            $rootScope.$emit(deviceConstant.getByPinCompletedEventName, data);
        }

        var onInsertInApplicationCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            deviceContext.device.push(data);
            deviceContext.$digest();
            $rootScope.$emit(deviceConstant.insertInApplicationCompletedEventName);
        }

        var onDeleteFromApplicationCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            for (var i = 0; i < deviceContext.device.length; i++) {
                if (deviceContext.device[i].deviceId === data.deviceId) {
                    deviceContext.device.splice(i, 1);                   
                    break;
                }
            }
            deviceContext.$digest();
            $rootScope.$emit(deviceConstant.deleteFromApplicationCompletedEventName);
        }

        var onSetLabelCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var device = deviceFinder.getByKey(result.deviceId, result.deviceDatasheetId);
            device.label = result.label;
            deviceContext.$digest();
            $rootScope.$emit(deviceConstant.setLabelCompletedEventName + result.deviceId, result);
        }        

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.initialized = initialized;

        serviceFactory.getByPin = getByPin;
        serviceFactory.insertInApplication = insertInApplication;
        serviceFactory.deleteFromApplication = deleteFromApplication;
        serviceFactory.setLabel = setLabel;

        return serviceFactory;

    }]);