'use strict';
app.factory('sensorTriggerService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'sensorContext', 'sensorFinder', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($http, $log, $rootScope, ngAuthSettings, stompService, sensorContext, sensorFinder, sensorTriggerFinder, sensorTriggerConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var insertCompletedSubscription = null;
        var deleteCompletedSubscription = null;
        var setTriggerOnCompletedSubscription = null;
        var setTriggerValueCompletedSubscription = null;
        var setBuzzerOnCompletedSubscription = null;

        var insertTrigger = function (sensorId, sensorDatasheetId, sensorTypeId, triggerOn, buzzerOn, max, min) {
            var data = {
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                triggerOn: triggerOn,
                buzzerOn: buzzerOn,
                max: max,
                min: min,
            }
            return $http.post(serviceBase + sensorTriggerConstant.insertApiUri, data).then(function (results) {
                return results;
            });
        };

        var deleteTrigger = function (sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId) {
            var data = {
                sensorTriggerId: sensorTriggerId,
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
            }
            return $http.post(serviceBase + sensorTriggerConstant.deleteApiUri, data).then(function (results) {
                return results;
            });
        };

        var setTriggerOn = function (sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId, triggerOn) {
            var data = {
                sensorTriggerId: sensorTriggerId,
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                triggerOn: triggerOn,
            }
            return $http.post(serviceBase + sensorTriggerConstant.setTriggerOnApiUri, data).then(function (results) {
                return results;
            });
        };

        var setTriggerValue = function (sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId, position, triggerValue) {
            var data = {
                sensorTriggerId: sensorTriggerId,
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,                
                position: position,
                triggerValue: triggerValue,
            }
            return $http.post(serviceBase + sensorTriggerConstant.setTriggerValueApiUri, data).then(function (results) {
                return results;
            });
        };

        var setBuzzerOn = function (sensorTriggerId, sensorId, sensorDatasheetId, sensorTypeId, buzzerOn) {
            var data = {
                sensorTriggerId: sensorTriggerId,
                sensorId: sensorId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                buzzerOn: buzzerOn,
            }
            return $http.post(serviceBase + sensorTriggerConstant.setBuzzerOnApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            insertCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.insertCompletedTopic, onInsertCompleted);
            deleteCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.deleteCompletedTopic, onDeleteCompleted);
            setTriggerOnCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.setTriggerOnCompletedTopic, onSetTriggerOnCompleted);
            setTriggerValueCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.setTriggerValueCompletedTopic, onSetTriggerValueCompleted);
            setBuzzerOnCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.setBuzzerOnCompletedTopic, onSetBuzzerOnCompleted);            
        }

        var onInsertCompleted = function (payload) {
            var result = JSON.parse(payload.body);            
            var sensor = sensorFinder.getByKey(result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            sensor.sensorTriggers.push(result);
            sensorContext.sensorTrigger.push(result);
            sensorContext.$digest();
            $rootScope.$emit(sensorTriggerConstant.insertCompletedEventName + result.sensorId, result);
        }

        var onDeleteCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorTrigger = sensorTriggerFinder.getByKey(result.sensorTriggerId, result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            var sensor = sensorFinder.getByKey(result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            //Remove from sensor
            for (var i = 0; i < sensor.sensorTriggers.length; i++) {
                if (sensorTrigger === sensor.sensorTriggers[i]){
                    sensor.sensorTriggers.splice(i, 1);
                    break;
                }
            }
            //Remove from context
            for (var i = 0; i < sensorContext.sensorTrigger.length; i++) {
                if (sensorTrigger === sensorContext.sensorTrigger[i]) {
                    sensorContext.sensorTrigger.splice(i, 1);                    
                    break;
                }
            }
            sensorContext.$digest();
            $rootScope.$emit(sensorTriggerConstant.deleteCompletedEventName + result.sensorId, result);
        }

        var onSetTriggerOnCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorTrigger = sensorTriggerFinder.getByKey(result.sensorTriggerId, result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            sensorTrigger.triggerOn = result.triggerOn;
            sensorContext.$digest();
            $rootScope.$emit(sensorTriggerConstant.setTriggerOnCompletedEventName + result.sensorTriggerId, result);
        }

        var onSetBuzzerOnCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorTrigger = sensorTriggerFinder.getByKey(result.sensorTriggerId, result.sensorId, result.sensorDatasheetId, result.sensorTypeId);
            sensorTrigger.buzzerOn = result.buzzerOn;
            sensorContext.$digest();
            $rootScope.$emit(sensorTriggerConstant.setBuzzerOnCompletedEventName + result.sensorTriggerId, result);
        }

        var onSetTriggerValueCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorTrigger = sensorTriggerFinder.getByKey(result.sensorTriggerId, result.sensorId, result.sensorDatasheetId, result.sensorTypeId);            
            if (result.position === 'High')
                sensorTrigger.max = result.max;
            else if (result.position === 'Low')
                sensorTrigger.min = result.min;
            sensorContext.$digest();
            $rootScope.$emit(sensorTriggerConstant.setTriggerValueCompletedEventName + result.sensorTriggerId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            insertCompletedSubscription.unsubscribe();
            deleteCompletedSubscription.unsubscribe();
            setTriggerOnCompletedSubscription.unsubscribe();
            setTriggerValueCompletedSubscription.unsubscribe();
            setBuzzerOnCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected); 

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.insertTrigger = insertTrigger;
        serviceFactory.deleteTrigger = deleteTrigger;
        serviceFactory.setTriggerOn = setTriggerOn;
        serviceFactory.setTriggerValue = setTriggerValue;
        serviceFactory.setBuzzerOn = setBuzzerOn;

        return serviceFactory;

    }]);