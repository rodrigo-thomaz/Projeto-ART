'use strict';
app.factory('deviceTypeService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'deviceDatasheetContext', 'deviceTypeConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, deviceDatasheetContext, deviceTypeConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.deviceTypeData) {
            var data = JSON.parse(Base64.decode($localStorage.deviceTypeData));
            for (var i = 0; i < data.length; i++) {
                deviceDatasheetContext.deviceType.push(data[i]);
            }
            $rootScope.$emit(deviceTypeConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server
        
        var _initializing = false;
        var _initialized = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribeView(deviceTypeConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + deviceTypeConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.deviceTypeData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                deviceDatasheetContext.deviceType.push(data[i]);
            }

            deviceDatasheetContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(deviceTypeConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);