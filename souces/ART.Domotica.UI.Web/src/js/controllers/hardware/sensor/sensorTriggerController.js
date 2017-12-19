'use strict';
app.controller('sensorTriggerController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

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
                sensorTriggerService.setTriggerValue(
                    $scope.sensorTrigger.sensorTriggerId,
                    $scope.sensorTrigger.sensorId,
                    $scope.sensorTrigger.sensorDatasheetId,
                    $scope.sensorTrigger.sensorTypeId,
                    'High',
                    newValue);
            });
        }

        var initializeMinViewWatch = function () {
            minViewWatch = $scope.$watch('minView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorTriggerService.setTriggerValue(
                    newValue.sensorTriggerId,
                    newValue.sensorId,
                    newValue.sensorDatasheetId,
                    newValue.sensorTypeId,
                    'Low',
                    newValue.triggerValue);
            });
        }

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
            $scope.triggerOnView = data.triggerOn;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'gatilho adicionado');
        };

        var onSetBuzzerOnCompleted = function (event, data) {
            $scope.buzzerOnView = data.buzzerOn;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'gatilho excluído');
        };

        var onSetTriggerValueCompleted = function (event, data) {
            if (position === 'High')
                $scope.maxView = data.max;
            else if (position === 'Low')
                $scope.minView = data.min;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'gatilho excluído');
        };

    }]);

