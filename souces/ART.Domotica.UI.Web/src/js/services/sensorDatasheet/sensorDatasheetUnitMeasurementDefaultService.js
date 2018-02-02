'use strict';
app.factory('sensorDatasheetUnitMeasurementDefaultService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'sensorDatasheetContext', 'sensorDatasheetUnitMeasurementDefaultConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, sensorDatasheetContext, sensorDatasheetUnitMeasurementDefaultConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.sensorDatasheetUnitMeasurementDefaultData) {
            var data = JSON.parse(Base64.decode($localStorage.sensorDatasheetUnitMeasurementDefaultData));
            for (var i = 0; i < data.length; i++) {
                sensorDatasheetContext.sensorDatasheetUnitMeasurementDefault.push(data[i]);
            }
            $rootScope.$emit(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server
        
        var _initializing = false;
        var _initialized = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribeView(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + sensorDatasheetUnitMeasurementDefaultConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.sensorDatasheetUnitMeasurementDefaultData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                sensorDatasheetContext.sensorDatasheetUnitMeasurementDefault.push(data[i]);
            }

            sensorDatasheetContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);