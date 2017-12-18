'use strict';
app.controller('sensorTriggerController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

        $scope.sensorTriggers = null;

        $scope.init = function (sensorTriggers) {
            $scope.sensorTriggers = sensorTriggers;
        };        

        $scope.$on('$destroy', function () {
            
        });        

        $scope.$watchCollection('sensorTriggers', function (newValues, oldValues) {
            if (newValues) {
                for (var i = 0; i < newValues.length; i++) {
                    newValues[i].max = 60;
                    newValues[i].min = 30;
                }
            }
        });

    }]);

