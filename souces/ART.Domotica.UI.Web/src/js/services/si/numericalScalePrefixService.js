'use strict';
app.factory('numericalScalePrefixService', ['$http', 'ngAuthSettings', 'numericalScalePrefixConstant', '$rootScope', '$localStorage', 'stompService', 'siContext',
    function ($http, ngAuthSettings, numericalScalePrefixConstant, $rootScope, $localStorage, stompService, siContext) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.numericalScalePrefixData) {
            var data = JSON.parse(Base64.decode($localStorage.numericalScalePrefixData));
            for (var i = 0; i < data.length; i++) {
                siContext.numericalScalePrefix.push(data[i]);
            }
            $rootScope.$emit(numericalScalePrefixConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(numericalScalePrefixConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + numericalScalePrefixConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.numericalScalePrefixData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                siContext.numericalScalePrefix.push(data[i]);
            }

            siContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(numericalScalePrefixConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });
        
        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);
                
        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);