'use strict';
app.controller('sensorTempDSFamilyController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorContext', 'sensorTempDSFamilyResolutionService', 'sensorTempDSFamilyService', 'sensorTempDSFamilyResolutionFinder', 'sensorTempDSFamilyConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorContext, sensorTempDSFamilyResolutionService, sensorTempDSFamilyService, sensorTempDSFamilyResolutionFinder, sensorTempDSFamilyConstant) {

        $scope.sensorTempDSFamily = null;

        $scope.$watch('sensorTempDSFamily', function (newValue) {
            if (newValue) {
                $scope.sensorTempDSFamilyResolutionView.selected = sensorTempDSFamilyResolutionFinder.getByKey(newValue.sensorTempDSFamilyResolutionId);                
                initializeSensorTempDSFamilyResolutionViewSelectedWatch();                
                clearOnSetResolutionCompleted = $rootScope.$on(sensorTempDSFamilyConstant.setResolutionCompletedEventName + newValue.sensorTempDSFamilyId, onSetResolutionCompleted);                
            }
        });      

        $scope.init = function (sensorTempDSFamily) {
            $scope.sensorTempDSFamily = sensorTempDSFamily;
        };

        $scope.sensorTempDSFamilyResolutionView = {
            availables: sensorContext.sensorTempDSFamilyResolution,
            selected: null,
        };

        var sensorTempDSFamilyResolutionViewSelectedWatch = null;

        var initializeSensorTempDSFamilyResolutionViewSelectedWatch = function () {
            sensorTempDSFamilyResolutionViewSelectedWatch = $scope.$watch('sensorTempDSFamilyResolutionView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorTempDSFamilyService.setResolution(
                    $scope.sensorTempDSFamily.sensorTempDSFamilyId,
                    $scope.sensorTempDSFamily.sensorDatasheetId,
                    $scope.sensorTempDSFamily.sensorTypeId,
                    $scope.sensorTempDSFamilyResolutionView.selected.sensorTempDSFamilyResolutionId);
            });
        };       

        var clearOnSetResolutionCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetResolutionCompleted();
        });

        var onSetResolutionCompleted = function (event, data) {
            sensorTempDSFamilyResolutionViewSelectedWatch();
            $scope.sensorTempDSFamilyResolutionView.selected = sensorTempDSFamilyResolutionFinder.getByKey(data.sensorTempDSFamilyResolutionId);                
            $scope.$apply();
            initializeSensorTempDSFamilyResolutionViewSelectedWatch();
            toaster.pop('success', 'Sucesso', 'resolução alterada');
        };

    }]);

