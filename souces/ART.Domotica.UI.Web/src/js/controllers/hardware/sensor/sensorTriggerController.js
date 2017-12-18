'use strict';
app.controller('sensorTriggerController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

        var _sensor = null;

        $scope.sensorTriggers = null;

        $scope.init = function (sensor) {
            _sensor = sensor;
            $scope.sensorTriggers = sensor.sensorTriggers;
        };

        $scope.add = function () {
            var sensorTrigger = {
                sensorId: _sensor.sensorId,
                sensorDatasheetId: _sensor.sensorDatasheetId,
                sensorTypeId: _sensor.sensorTypeId,
                triggerOn: false,
                buzzerOn: false,
                max: 125,
                min: -55,
            };
            $scope.sensorTriggers.push(sensorTrigger);
        }

        $scope.remove = function (sensorTrigger) {
            for (var i = 0; i < $scope.sensorTriggers.length; i++) {
                if (sensorTrigger === $scope.sensorTriggers[i]) {
                    $scope.sensorTriggers.splice(i, 1);
                    break;
                }
            }
        }

        $scope.$on('$destroy', function () {

        });

    }]);

