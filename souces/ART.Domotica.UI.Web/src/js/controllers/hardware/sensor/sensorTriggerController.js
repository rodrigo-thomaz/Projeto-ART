'use strict';
app.controller('sensorTriggerController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTriggerService', 'sensorTriggerFinder', 'sensorTriggerConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTriggerService, sensorTriggerFinder, sensorTriggerConstant) {

        $scope.sensorTriggers = null;

        $scope.init = function (sensorTriggers) {
            $scope.sensorTriggers = sensorTriggers;
        };        

        $scope.$on('$destroy', function () {
            
        }); 

    }]);

