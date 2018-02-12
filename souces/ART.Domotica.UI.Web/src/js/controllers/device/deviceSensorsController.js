'use strict';
app.controller('deviceSensorsController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceSensorsConstant', 'deviceSensorsService',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceSensorsConstant, deviceSensorsService) {

    $scope.deviceSensors = null;

    $scope.init = function (deviceSensors) {
        
        $scope.deviceSensors = deviceSensors; 

        $scope.readIntervalInMilliSecondsView = deviceSensors.readIntervalInMilliSeconds;
        $scope.publishIntervalInMilliSecondsView = deviceSensors.publishIntervalInMilliSeconds;
        $scope.epochTimeUtcView = deviceSensors.epochTimeUtc;

        clearOnMessageIoTReceived = $rootScope.$on(deviceSensorsConstant.messageIoTEventName + $scope.deviceSensors.deviceId, onMessageIoTReceived);
        clearOnSetReadIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorsConstant.setReadIntervalInMilliSecondsCompletedEventName + $scope.deviceSensors.deviceId, onSetReadIntervalInMilliSecondsCompleted);        
        clearOnSetPublishIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedEventName + $scope.deviceSensors.deviceId, onSetPublishIntervalInMilliSecondsCompleted);        
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
        $scope.epochTimeUtcView = $scope.deviceSensors.epochTimeUtc;
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
        if (!$scope.deviceSensors || !$scope.readIntervalInMilliSecondsView) return;
        deviceSensorsService.setReadIntervalInMilliSeconds($scope.deviceSensors.deviceTypeId, $scope.deviceSensors.deviceDatasheetId, $scope.deviceSensors.deviceId, $scope.readIntervalInMilliSecondsView);
    };

    $scope.changePublishIntervalInMilliSeconds = function () {
        if (!$scope.deviceSensors || !$scope.publishIntervalInMilliSecondsView) return;
        deviceSensorsService.setPublishIntervalInMilliSeconds($scope.deviceSensors.deviceTypeId, $scope.deviceSensors.deviceDatasheetId, $scope.deviceSensors.deviceId, $scope.publishIntervalInMilliSecondsView);
    };

}]);