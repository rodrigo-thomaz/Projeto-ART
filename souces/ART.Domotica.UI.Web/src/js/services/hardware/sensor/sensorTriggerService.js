'use strict';
app.factory('sensorTriggerService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'sensorFinder', 'sensorTriggerConstant',
    function ($http, $log, $rootScope, ngAuthSettings, stompService, sensorFinder, sensorTriggerConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setAlarmOnCompletedSubscription = null;
        var setAlarmCelsiusCompletedSubscription = null;
        var setAlarmBuzzerOnCompletedSubscription = null;

        var setAlarmOn = function (sensorTempDSFamilyId, alarmOn, position) {
            var data = {
                sensorTempDSFamilyId: sensorTempDSFamilyId,
                alarmOn: alarmOn,
                position: position,
            }
            return $http.post(serviceBase + sensorTriggerConstant.setAlarmOnApiUri, data).then(function (results) {
                return results;
            });
        };

        var setAlarmCelsius = function (sensorTempDSFamilyId, alarmCelsius, position) {
            var data = {
                sensorTempDSFamilyId: sensorTempDSFamilyId,
                alarmCelsius: alarmCelsius,
                position: position,
            }
            return $http.post(serviceBase + sensorTriggerConstant.setAlarmCelsiusApiUri, data).then(function (results) {
                return results;
            });
        };

        var setAlarmBuzzerOn = function (sensorTempDSFamilyId, alarmBuzzerOn, position) {
            var data = {
                sensorTempDSFamilyId: sensorTempDSFamilyId,
                alarmBuzzerOn: alarmBuzzerOn,
                position: position,
            }
            return $http.post(serviceBase + sensorTriggerConstant.setAlarmBuzzerOnApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            setAlarmOnCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.setAlarmOnCompletedTopic, onSetAlarmOnCompleted);
            setAlarmCelsiusCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.setAlarmCelsiusCompletedTopic, onSetAlarmCelsiusCompleted);
            setAlarmBuzzerOnCompletedSubscription = stompService.subscribeAllViews(sensorTriggerConstant.setAlarmBuzzerOnCompletedTopic, onSetAlarmBuzzerOnCompleted);            
        }

        var onSetAlarmOnCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensor = sensorFinder.getSensorDSTempFamilyByKey(result.sensorTempDSFamilyId);
            if (result.position === 'Low')
                sensor.lowAlarm.alarmOn = result.alarmOn;
            else if (result.position === 'High')
                sensor.highAlarm.alarmOn = result.alarmOn;
            $rootScope.$emit(sensorTriggerConstant.setAlarmOnCompletedEventNamesetAlarmOnCompletedEventName + result.sensorTempDSFamilyId, result);
        }

        var onSetAlarmCelsiusCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensor = sensorFinder.getSensorDSTempFamilyByKey(result.sensorTempDSFamilyId);
            if (result.position === 'Low')
                sensor.lowAlarm.alarmCelsius = result.alarmCelsius;
            else if (result.position === 'High')
                sensor.highAlarm.alarmCelsius = result.alarmCelsius;
            $rootScope.$emit(sensorTriggerConstant.setAlarmCelsiusCompletedEventName + result.sensorTempDSFamilyId, result);
        }

        var onSetAlarmBuzzerOnCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensor = sensorFinder.getSensorDSTempFamilyByKey(result.sensorTempDSFamilyId);
            if (result.position === 'Low')
                sensor.lowAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
            else if (result.position === 'High')
                sensor.highAlarm.alarmBuzzerOn = result.alarmBuzzerOn;
            $rootScope.$emit(sensorTriggerConstant.setAlarmBuzzerOnCompletedEventName + result.sensorTempDSFamilyId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setAlarmOnCompletedSubscription();
            setAlarmCelsiusCompletedSubscription();
            setAlarmBuzzerOnCompletedSubscription();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected); 

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setAlarmOn = setAlarmOn;
        serviceFactory.setAlarmCelsius = setAlarmCelsius;
        serviceFactory.setAlarmBuzzerOn = setAlarmBuzzerOn;

        return serviceFactory;

    }]);