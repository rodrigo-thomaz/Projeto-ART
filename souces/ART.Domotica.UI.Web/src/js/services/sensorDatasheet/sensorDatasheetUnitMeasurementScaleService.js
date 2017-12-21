'use strict';
app.factory('sensorDatasheetUnitMeasurementScaleService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'sensorDatasheetContext', 'sensorDatasheetUnitMeasurementScaleConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, sensorDatasheetContext, sensorDatasheetUnitMeasurementScaleConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.sensorDatasheetUnitMeasurementScaleData) {
            var data = JSON.parse(Base64.decode($localStorage.sensorDatasheetUnitMeasurementScaleData));
            for (var i = 0; i < data.length; i++) {
                sensorDatasheetContext.sensorDatasheetUnitMeasurementScale.push(data[i]);
            }
            $rootScope.$emit(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server        

        var _initializing = false;
        var _initialized = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + sensorDatasheetUnitMeasurementScaleConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.sensorDatasheetUnitMeasurementScaleData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                sensorDatasheetContext.sensorDatasheetUnitMeasurementScale.push(data[i]);
            }

            sensorDatasheetContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);