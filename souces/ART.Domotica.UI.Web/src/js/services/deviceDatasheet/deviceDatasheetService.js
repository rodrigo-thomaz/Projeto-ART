'use strict';
app.factory('deviceDatasheetService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'deviceDatasheetContext', 'deviceDatasheetConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, deviceDatasheetContext, deviceDatasheetConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.deviceDatasheetData) {
            var data = JSON.parse(Base64.decode($localStorage.deviceDatasheetData));
            for (var i = 0; i < data.length; i++) {
                deviceDatasheetContext.deviceDatasheet.push(data[i]);
            }
            $rootScope.$emit(deviceDatasheetConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initializing = false;
        var _initialized = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribeView(deviceDatasheetConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + deviceDatasheetConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.deviceDatasheetData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                deviceDatasheetContext.deviceDatasheet.push(data[i]);
            }

            deviceDatasheetContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(deviceDatasheetConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);