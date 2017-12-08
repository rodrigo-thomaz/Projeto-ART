'use strict';
app.factory('numericalScaleTypeCountryService', ['$http', 'ngAuthSettings', 'numericalScaleTypeCountryConstant', '$rootScope', 'stompService', 'siContext',
    function ($http, ngAuthSettings, numericalScaleTypeCountryConstant, $rootScope, stompService, siContext) {

        var serviceFactory = {};

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(numericalScaleTypeCountryConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + numericalScaleTypeCountryConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));


            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                siContext.numericalScaleTypeCountry.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(numericalScaleTypeCountryConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);