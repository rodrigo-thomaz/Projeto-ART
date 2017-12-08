'use strict';
app.factory('continentService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'localeContext', 'continentConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, localeContext, continentConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.continentData) {
            var data = JSON.parse($localStorage.continentData);
            for (var i = 0; i < data.length; i++) {
                localeContext.continent.push(data[i]);
            }
            $rootScope.$emit(continentConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(continentConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + continentConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.continentData = dataUTF8;

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                localeContext.continent.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(continentConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);