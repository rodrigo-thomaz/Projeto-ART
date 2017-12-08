'use strict';
app.factory('countryService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'localeContext', 'countryConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, localeContext, countryConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.countryData) {
            var data = JSON.parse($localStorage.countryData);
            for (var i = 0; i < data.length; i++) {
                localeContext.country.push(data[i]);
            }
            $rootScope.$emit(countryConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(countryConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + countryConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.countryData = dataUTF8;

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                localeContext.country.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(countryConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);