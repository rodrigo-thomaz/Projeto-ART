'use strict';
app.factory('sensorService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'sensorContext', 'sensorConstant', 'sensorFinder',
    function ($http, ngAuthSettings, $rootScope, stompService, sensorContext, sensorConstant, sensorFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var _initializing = false;
        var _initialized = false;

        var getAllByApplicationIdCompletedSubscription = null;
        var setLabelCompletedSubscription = null;
        var insertInApplicationCompletedSubscription = null;
        var deleteFromApplicationCompletedSubscription = null;

        var onConnected = function () {

            getAllByApplicationIdCompletedSubscription = stompService.subscribe(sensorConstant.getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);
            setLabelCompletedSubscription = stompService.subscribeAllViews(sensorConstant.setLabelCompletedTopic, onSetLabelCompleted);
            insertInApplicationCompletedSubscription = stompService.subscribeAllViews(sensorConstant.insertInApplicationCompletedTopic, onInsertInApplicationCompleted);
            deleteFromApplicationCompletedSubscription = stompService.subscribeAllViews(sensorConstant.deleteFromApplicationCompletedTopic, onDeleteFromApplicationCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAllByApplicationId();
            }
        }

        var initialized = function () {
            return _initialized;
        };

        var getAllByApplicationId = function () {
            return $http.post(serviceBase + sensorConstant.getAllByApplicationIdApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var setLabel = function (sensorId, sensorDatasheetId, sensorTypeId, label) {
            var data = {
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                label: label,
            }
            return $http.post(serviceBase + sensorConstant.setLabelApiUri, data).then(function (results) {
                return results;
            });
        };

        var onGetAllByApplicationIdCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                sensorContext.sensor.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllByApplicationIdCompletedSubscription.unsubscribe();

            $rootScope.$emit(sensorConstant.getAllByApplicationIdCompletedEventName);
        }

        var onSetLabelCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensor = sensorFinder.getByKey(result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            sensor.label = result.label;
            sensorContext.$digest();
            $rootScope.$emit(sensorConstant.setLabelCompletedEventName + result.sensorId, result);
        }

        var onInsertInApplicationCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            for (var i = 0; i < data.length; i++) {
                sensorContext.sensor.push(data[i]);
            }            
            sensorContext.$digest();
            $rootScope.$emit(sensorConstant.insertInApplicationCompletedEventName);
        }

        var onDeleteFromApplicationCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < sensorContext.sensor.length; j++) {
                    if (data[i].sensorId === sensorContext.sensor[j].sensorId) {
                        sensorContext.sensor.splice(j, 1);
                        break;
                    }
                }
            }
            sensorContext.$digest();
            $rootScope.$emit(sensorConstant.deleteFromApplicationCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setLabelCompletedSubscription.unsubscribe();
            insertInApplicationCompletedSubscription.unsubscribe();
            deleteFromApplicationCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.initialized = initialized;
        serviceFactory.setLabel = setLabel;

        return serviceFactory;

    }]);