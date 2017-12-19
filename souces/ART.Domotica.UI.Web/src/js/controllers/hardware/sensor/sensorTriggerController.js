'use strict';
app.controller('sensorTriggerController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'debounce', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, debounce, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

        $scope.sensorTrigger = null;

        $scope.triggerOnView = null;
        $scope.buzzerOnView = null;
        $scope.maxView = null;
        $scope.minView = null;

        $scope.init = function (sensorTrigger) {

            $scope.sensorTrigger = sensorTrigger;

            $scope.triggerOnView = sensorTrigger.triggerOn;
            $scope.buzzerOnView = sensorTrigger.buzzerOn;
            $scope.maxView = sensorTrigger.max;
            $scope.minView = sensorTrigger.min;

            initializeTriggerOnViewWatch();
            initializeBuzzerOnViewWatch();
            initializeMaxViewWatch();
            initializeMinViewWatch();

            clearOnSetTriggerOnCompleted = $rootScope.$on(sensorTriggerConstant.setTriggerOnCompletedEventName + sensorTrigger.sensorTriggerId, onSetTriggerOnCompleted);
            clearOnSetBuzzerOnCompleted = $rootScope.$on(sensorTriggerConstant.setBuzzerOnCompletedEventName + sensorTrigger.sensorTriggerId, onSetBuzzerOnCompleted);
            clearOnSetTriggerValueCompleted = $rootScope.$on(sensorTriggerConstant.setTriggerValueCompletedEventName + sensorTrigger.sensorTriggerId, onSetTriggerValueCompleted);
        };

        var triggerOnViewWatch = null;
        var buzzerOnViewWatch = null;
        var maxViewWatch = null;
        var minViewWatch = null;

        var initializeTriggerOnViewWatch = function () {
            triggerOnViewWatch = $scope.$watch('triggerOnView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorTriggerService.setTriggerOn(
                    $scope.sensorTrigger.sensorTriggerId,
                    $scope.sensorTrigger.sensorId,
                    $scope.sensorTrigger.sensorDatasheetId,
                    $scope.sensorTrigger.sensorTypeId,
                    newValue);
            });
        }

        var initializeBuzzerOnViewWatch = function () {
            buzzerOnViewWatch = $scope.$watch('buzzerOnView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorTriggerService.setBuzzerOn(
                    $scope.sensorTrigger.sensorTriggerId,
                    $scope.sensorTrigger.sensorId,
                    $scope.sensorTrigger.sensorDatasheetId,
                    $scope.sensorTrigger.sensorTypeId,
                    newValue);
            });
        }

        var initializeMaxViewWatch = function () {
            maxViewWatch = $scope.$watch('maxView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                setTriggerValue('Max', newValue);
            });
        }

        var initializeMinViewWatch = function () {
            minViewWatch = $scope.$watch('minView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                setTriggerValue('Min', newValue);
            });
        }

        var setTriggerValue = debounce(1000, function (position, triggerValue) {
            sensorTriggerService.setTriggerValue(
                $scope.sensorTrigger.sensorTriggerId,
                $scope.sensorTrigger.sensorId,
                $scope.sensorTrigger.sensorDatasheetId,
                $scope.sensorTrigger.sensorTypeId,
                position,
                triggerValue);
        });

        var clearOnSetTriggerOnCompleted = null;
        var clearOnSetBuzzerOnCompleted = null;
        var clearOnSetTriggerValueCompleted = null;

        $scope.$on('$destroy', function () {

            clearOnSetTriggerOnCompleted();
            clearOnSetBuzzerOnCompleted();
            clearOnSetTriggerValueCompleted();

            triggerOnViewWatch();
            buzzerOnViewWatch();
            maxViewWatch();
            minViewWatch();

        });

        var onSetTriggerOnCompleted = function (event, data) {
            triggerOnViewWatch();
            $scope.triggerOnView = data.triggerOn;
            $scope.$apply();            
            initializeTriggerOnViewWatch();
            toaster.pop('success', 'Sucesso', 'Trigger On alterada');
        };

        var onSetBuzzerOnCompleted = function (event, data) {
            buzzerOnViewWatch();
            $scope.buzzerOnView = data.buzzerOn;
            $scope.$apply();
            initializeBuzzerOnViewWatch();
            toaster.pop('success', 'Sucesso', 'Buzzer On alterado');
        };

        var onSetTriggerValueCompleted = function (event, data) {
            if (data.position === 'Max') {
                maxViewWatch();
                $scope.maxView = data.triggerValue;
                $scope.$apply();
                initializeMaxViewWatch();
            }
            else if (data.position === 'Min') {
                minViewWatch();
                $scope.minView = data.triggerValue;
                $scope.$apply();
                initializeMinViewWatch();
            }            
            toaster.pop('success', 'Sucesso', 'Trigger value alterado');
        };

    }]);

