'use strict';
app.controller('deviceSensorsController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceSensorsConstant', 'deviceSensorsService',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceSensorsConstant, deviceSensorsService) {

    $scope.deviceSensors = null;

    $scope.init = function (deviceSensors) {
        
        $scope.deviceSensors = deviceSensors; 

        $scope.readIntervalInMilliSecondsView = deviceSensors.readIntervalInMilliSeconds;
        $scope.publishIntervalInMilliSecondsView = deviceSensors.publishIntervalInMilliSeconds;

        clearOnSetReadIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorsConstant.setReadIntervalInMilliSecondsCompletedEventName + $scope.deviceSensors.deviceSensorsId, onSetReadIntervalInMilliSecondsCompleted);        
        clearOnSetPublishIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedEventName + $scope.deviceSensors.deviceSensorsId, onSetPublishIntervalInMilliSecondsCompleted);        
    }

    var clearOnSetReadIntervalInMilliSecondsCompleted = null;
    var clearOnSetPublishIntervalInMilliSecondsCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetReadIntervalInMilliSecondsCompleted();
        clearOnSetPublishIntervalInMilliSecondsCompleted();
    });

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
        deviceSensorsService.setReadIntervalInMilliSeconds($scope.deviceSensors.deviceSensorsId, $scope.deviceSensors.deviceDatasheetId, $scope.readIntervalInMilliSecondsView);
    };

    $scope.changePublishIntervalInMilliSeconds = function () {
        if (!$scope.deviceSensors || !$scope.publishIntervalInMilliSecondsView) return;
        deviceSensorsService.setPublishIntervalInMilliSeconds($scope.deviceSensors.deviceSensorsId, $scope.deviceSensors.deviceDatasheetId, $scope.publishIntervalInMilliSecondsView);
    };

}]);