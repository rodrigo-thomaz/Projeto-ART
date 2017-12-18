'use strict';
app.controller('sensorTriggerController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

        var _sensor = null;

        $scope.sensorTriggers = null;

        $scope.init = function (sensor) {
            _sensor = sensor;
            $scope.sensorTriggers = sensor.sensorTriggers;
        };

        var triggerOnDefault = false;
        var buzzerOnDefault = false;
        var maxDefault = 125;
        var minDefault = -55;

        $scope.insert = function () {
            
            sensorTriggerService.insertTrigger(
                _sensor.sensorId,
                _sensor.sensorDatasheetId,
                _sensor.sensorTypeId,
                triggerOnDefault,
                buzzerOnDefault,
                maxDefault,
                minDefault);

            //$scope.sensorTriggers.push({
            //    sensorId: _sensor.sensorId,
            //    sensorDatasheetId: _sensor.sensorDatasheetId,
            //    sensorTypeId: _sensor.sensorTypeId,
            //    triggerOn: triggerOnDefault,
            //    buzzerOn: buzzerOnDefault,
            //    max: maxDefault,
            //    min: minDefault,
            //});

        }

        $scope.remove = function (sensorTrigger) {
            for (var i = 0; i < $scope.sensorTriggers.length; i++) {
                if (sensorTrigger === $scope.sensorTriggers[i]) {
                    sensorTriggerService.deleteTrigger(
                        sensorTrigger.sensorTriggerId,
                        sensorTrigger.sensorId,
                        sensorTrigger.sensorDatasheetId,
                        sensorTrigger.sensorTypeId);
                    //$scope.sensorTriggers.splice(i, 1);
                    break;
                }
            }
        }

        $scope.$on('$destroy', function () {

        });

    }]);

