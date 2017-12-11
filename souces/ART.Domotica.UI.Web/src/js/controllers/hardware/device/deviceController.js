'use strict';
app.controller('deviceController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceConstant', 'deviceService',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceConstant, deviceService) {

    $scope.device = null;

    $scope.init = function (device) {

        $scope.device = device; 

        $scope.labelView = device.label;

        clearOnSetLabelCompleted = $rootScope.$on(deviceConstant.setLabelCompletedEventName + $scope.device.deviceId, onSetLabelCompleted);        
    }

    var clearOnSetLabelCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetLabelCompleted();
    });

    var onSetLabelCompleted = function (event, data) {
        $scope.labelView = data.label;
        $scope.$apply();
        toaster.pop('success', 'Sucesso', 'Label alterado');
    };    

    $scope.changeLabel = function () {
        if (!$scope.device || !$scope.labelView) return;
        deviceService.setLabel($scope.device.deviceId, $scope.labelView);
    };

}]);