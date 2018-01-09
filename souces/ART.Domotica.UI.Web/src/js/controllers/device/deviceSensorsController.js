'use strict';
app.controller('deviceSensorsController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceSensorsConstant', 'deviceSensorsService',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceSensorsConstant, deviceSensorsService) {

    $scope.device = null;

    $scope.init = function (deviceSensors) {
        
        $scope.deviceSensors = deviceSensors; 

        $scope.publishIntervalInMilliSecondsView = deviceSensors.publishIntervalInMilliSeconds;

        clearOnSetPublishIntervalInMilliSecondsCompleted = $rootScope.$on(deviceSensorsConstant.setPublishIntervalInMilliSecondsCompletedEventName + $scope.deviceSensors.deviceSensorsId, onSetPublishIntervalInMilliSecondsCompleted);        
    }

    var clearOnSetPublishIntervalInMilliSecondsCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetPublishIntervalInMilliSecondsCompleted();
    });

    var onSetPublishIntervalInMilliSecondsCompleted = function (event, data) {
        $scope.publishIntervalInMilliSecondsView = data.publishIntervalInMilliSeconds;
        $scope.$apply();
        toaster.pop('success', 'Sucesso', 'PublishIntervalInMilliSeconds alterado');
    };    

    $scope.changePublishIntervalInMilliSeconds = function () {
        if (!$scope.deviceSensors || !$scope.publishIntervalInMilliSecondsView) return;
        deviceSensorsService.setPublishIntervalInMilliSeconds($scope.deviceSensors.deviceSensorsId, $scope.deviceSensors.deviceDatasheetId, $scope.publishIntervalInMilliSecondsView);
    };

}]);