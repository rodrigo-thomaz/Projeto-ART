'use strict';
app.factory('unitMeasurementScaleService', ['$http', 'ngAuthSettings', 'unitMeasurementScaleConstant', '$rootScope', '$localStorage', 'stompService', 'siContext',
    function ($http, ngAuthSettings, unitMeasurementScaleConstant, $rootScope, $localStorage, stompService, siContext) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.unitMeasurementScaleData) {
            var data = JSON.parse($localStorage.unitMeasurementScaleData);
            for (var i = 0; i < data.length; i++) {
                siContext.unitMeasurementScale.push(data[i]);
            }
            $rootScope.$emit(unitMeasurementScaleConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(unitMeasurementScaleConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + unitMeasurementScaleConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.unitMeasurementScaleData = dataUTF8;
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                siContext.unitMeasurementScale.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(unitMeasurementScaleConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);