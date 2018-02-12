'use strict';
app.controller('deviceSensorController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceSensorConstant', 'deviceSensorService',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceSensorConstant, deviceSensorService) {

    $scope.deviceSensor = null;

    $scope.init = function (deviceSensor) {
        
        $scope.deviceSensor = deviceSensor; 

        $scope.readIntervalInMilliSecondsView = deviceSensor.readIntervalInMilliSeconds;
        $scope.publishIntervalInMilliSecondsView = deviceSensor.publishIntervalInMilliSeconds;
        $scope.epochTimeUtcView = deviceSensor.epochTimeUtc;

        clearOnMessageIoTReceived = $rootScope.$on(deviceSensorConstant.messageIoTEventName + $scope.deviceSensor.deviceId, onMessageIoTReceived);
        clearOnSetReadIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorConstant.setReadIntervalInMilliSecondsCompletedEventName + $scope.deviceSensor.deviceId, onSetReadIntervalInMilliSecondsCompleted);        
        clearOnSetPublishIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorConstant.setPublishIntervalInMilliSecondsCompletedEventName + $scope.deviceSensor.deviceId, onSetPublishIntervalInMilliSecondsCompleted);        
    }

    var clearOnMessageIoTReceived = null;
    var clearOnSetReadIntervalInMilliSecondsCompleted = null;
    var clearOnSetPublishIntervalInMilliSecondsCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnMessageIoTReceived();
        clearOnSetReadIntervalInMilliSecondsCompleted();
        clearOnSetPublishIntervalInMilliSecondsCompleted();
    });

    var onMessageIoTReceived = function (event, data) {
        $scope.epochTimeUtcView = $scope.deviceSensor.epochTimeUtc;
        $scope.$apply();
    };

    var onSetReadIntervalInMilliSecondsCompleted = function (event, data) {
        $scope.readIntervalInMilliSecondsView = data.readIntervalInMilliSeconds;
        $scope.$apply();
        toaster.pop('success', 'Sucesso', 'ReadIntervalInMilliSeconds alterado');
    };    

    var onSetPublishIntervalInMilliSecondsCompleted = function (event, data) {
        $scope.publishIntervalInMilliSecondsView = data.publishIntervalInMilliSeconds;
        $scope.$apply();
        toaster.pop('success', 'Sucesso', 'Sensors PublishIntervalInMilliSeconds alterado');
    };    

    $scope.changeReadIntervalInMilliSeconds = function () {
        if (!$scope.deviceSensor || !$scope.readIntervalInMilliSecondsView) return;
        deviceSensorService.setReadIntervalInMilliSeconds($scope.deviceSensor.deviceTypeId, $scope.deviceSensor.deviceDatasheetId, $scope.deviceSensor.deviceId, $scope.readIntervalInMilliSecondsView);
    };

    $scope.changePublishIntervalInMilliSeconds = function () {
        if (!$scope.deviceSensor || !$scope.publishIntervalInMilliSecondsView) return;
        deviceSensorService.setPublishIntervalInMilliSeconds($scope.deviceSensor.deviceTypeId, $scope.deviceSensor.deviceDatasheetId, $scope.deviceSensor.deviceId, $scope.publishIntervalInMilliSecondsView);
    };

}]);