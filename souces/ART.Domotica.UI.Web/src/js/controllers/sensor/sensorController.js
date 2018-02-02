'use strict';
app.controller('sensorController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorService', 'sensorConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorService, sensorConstant) {

        $scope.sensor = null;

        $scope.sensorLabel = "";                 

        $scope.init = function (sensor) {

            $scope.sensor = sensor;

            $scope.isConnectedView = sensor.isConnected;
            $scope.valueView = sensor.value;

            clearOnMessageIoTReceived = $rootScope.$on(sensorConstant.messageIoTEventName + $scope.sensor.sensorId, onMessageIoTReceived);
        }

        var clearOnMessageIoTReceived = null;
        var clearOnSetLabelCompleted = null;        

        $scope.$on('$destroy', function () {
            clearOnSetLabelCompleted();
            clearOnMessageIoTReceived();
        });

        var onMessageIoTReceived = function (event, data) {
            $scope.isConnectedView = $scope.sensor.isConnected;
            $scope.valueView = $scope.sensor.value;
            $scope.$apply();
        };

        var onSetSensorLabelCompleted = function (event, data) {
            $scope.sensorLabel = data.label;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'label do sensor alterado');
        };

        $scope.changeSensorLabel = function () {
            if (!$scope.sensor || !$scope.sensorLabel) return;
            sensorService.setLabel($scope.sensor.sensorId, $scope.sensor.sensorDatasheetId, $scope.sensor.sensorTypeId, $scope.sensorLabel);
        };  

        $scope.$watch('sensor', function (newValue) {
            if (newValue) {
                $scope.sensorLabel = newValue.label;    
                clearOnSetLabelCompleted = $rootScope.$on(sensorConstant.setLabelCompletedEventName + newValue.sensorId, onSetSensorLabelCompleted);
            }
        });        

    }]);