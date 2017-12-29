'use strict';
app.controller('deviceSensorsController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceSensorsConstant', 'deviceSensorsService',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceSensorsConstant, deviceSensorsService) {

    $scope.device = null;

    $scope.init = function (deviceSensors) {
        
        $scope.deviceSensors = deviceSensors; 

        $scope.publishIntervalInSecondsView = deviceSensors.publishIntervalInSeconds;

        clearOnSetPublishIntervalInSecondsCompleted = $rootScope.$on(deviceSensorsConstant.setPublishIntervalInSecondsCompletedEventName + $scope.deviceSensors.deviceSensorsId, onSetPublishIntervalInSecondsCompleted);        
    }

    var clearOnSetPublishIntervalInSecondsCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetPublishIntervalInSecondsCompleted();
    });

    var onSetPublishIntervalInSecondsCompleted = function (event, data) {
        $scope.publishIntervalInSecondsView = data.publishIntervalInSeconds;
        $scope.$apply();
        toaster.pop('success', 'Sucesso', 'PublishIntervalInSeconds alterado');
    };    

    $scope.changePublishIntervalInSeconds = function () {
        if (!$scope.deviceSensors || !$scope.publishIntervalInSecondsView) return;
        deviceSensorsService.setPublishIntervalInSeconds($scope.deviceSensors.deviceSensorsId, $scope.deviceSensors.deviceDatasheetId, $scope.publishIntervalInSecondsView);
    };

}]);