'use strict';
app.factory('numericalScaleTypeService', ['$http', 'ngAuthSettings', 'numericalScaleTypeConstant', '$rootScope', '$localStorage', 'stompService', 'siContext',
    function ($http, ngAuthSettings, numericalScaleTypeConstant, $rootScope, $localStorage, stompService, siContext) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.numericalScaleTypeData) {
            var data = JSON.parse($localStorage.numericalScaleTypeData);
            for (var i = 0; i < data.length; i++) {
                siContext.numericalScaleType.push(data[i]);
            }
            $rootScope.$emit(numericalScaleTypeConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(numericalScaleTypeConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + numericalScaleTypeConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.numericalScaleTypeData = dataUTF8;
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                siContext.numericalScaleType.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(numericalScaleTypeConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);