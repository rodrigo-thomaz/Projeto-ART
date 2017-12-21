'use strict';
app.controller('sensorController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorService', 'sensorConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorService, sensorConstant) {

        $scope.sensor = null;

        $scope.$watch('sensor', function (newValue) {
            if (newValue) {
                $scope.sensorLabel = newValue.label;    
                clearOnSetLabelCompleted = $rootScope.$on(sensorConstant.setLabelCompletedEventName + newValue.sensorId, onSetSensorLabelCompleted);
            }
        });

        $scope.sensorLabel = "";  

        $scope.init = function (sensor) {
            $scope.sensor = sensor;
        }

        $scope.changeSensorLabel = function () {
            if (!$scope.sensor || !$scope.sensorLabel) return;
            sensorService.setLabel($scope.sensor.sensorId, $scope.sensor.sensorDatasheetId, $scope.sensor.sensorTypeId, $scope.sensorLabel);
        };

        var clearOnSetLabelCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetLabelCompleted();
        });

        var onSetSensorLabelCompleted = function (event, data) {
            $scope.sensorLabel = data.label;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'label do sensor alterado');
        };

    }]);