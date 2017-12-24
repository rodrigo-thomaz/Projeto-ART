'use strict';
app.controller('sensorTriggersController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

        var _sensor = null;

        $scope.sensorTriggers = null;

        var triggerOnDefault = false;
        var buzzerOnDefault = false;
        var maxDefault = 125;
        var minDefault = -55;

        $scope.init = function (sensor) {

            _sensor = sensor;
            
            if (angular.isArray(sensor.sensorTriggers)) {
                $scope.sensorTriggers = sensor.sensorTriggers;
            }
            else {
                $scope.sensorTriggers = [];
            }

            clearOnInsertCompleted = $rootScope.$on(sensorTriggerConstant.insertCompletedEventName + _sensor.sensorId, onInsertCompleted);                
            clearOnDeleteCompleted = $rootScope.$on(sensorTriggerConstant.deleteCompletedEventName + _sensor.sensorId, onDeleteCompleted);                
        };

        $scope.insert = function () {            
            sensorTriggerService.insertTrigger(
                _sensor.sensorId,
                _sensor.sensorDatasheetId,
                _sensor.sensorTypeId,
                triggerOnDefault,
                buzzerOnDefault,
                maxDefault,
                minDefault);
        }

        $scope.remove = function (sensorTrigger) {
            sensorTriggerService.deleteTrigger(
                sensorTrigger.sensorTriggerId,
                sensorTrigger.sensorId,
                sensorTrigger.sensorDatasheetId,
                sensorTrigger.sensorTypeId);            
        }

        var clearOnInsertCompleted = null;
        var clearOnDeleteCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnInsertCompleted();
            clearOnDeleteCompleted();
        });

        var onInsertCompleted = function (event, data) {
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'gatilho adicionado');
        };

        var onDeleteCompleted = function (event, data) {
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'gatilho excluído');
        };
    }]);

