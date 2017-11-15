'use strict';
app.controller('espDeviceListController', ['$scope', '$timeout', '$log', 'EventDispatcher', 'espDeviceService', function ($scope, $timeout, $log, EventDispatcher, espDeviceService) {    
   
    $scope.devices = espDeviceService.devices;    

}]);

app.controller('espDeviceItemController', ['$scope', '$timeout', '$log', 'EventDispatcher', 'espDeviceService', function ($scope, $timeout, $log, EventDispatcher, espDeviceService) {

    $scope.device = {};

    $scope.init = function (device) {
        $scope.device = device;        
    }

}]);

app.controller('dsFamilyTempSensorItemController', ['$scope', '$timeout', '$log', 'EventDispatcher', 'espDeviceService', 'dsFamilyTempSensorService', 'temperatureScaleService', function ($scope, $timeout, $log, EventDispatcher, espDeviceService, dsFamilyTempSensorService, temperatureScaleService) {

    $scope.sensor = {};

    $scope.scale = {
        availableScales: temperatureScaleService.scales,
        selectedScale: {},
    };

    $scope.resolution = {
        availableResolutions: dsFamilyTempSensorService.resolutions,
        selectedResolution: {},
    };

    $scope.init = function (sensor) {

        $scope.sensor = sensor;

        for (var i = 0; i < $scope.scale.availableScales.length; i++) {
            if ($scope.scale.availableScales[i].id == sensor.temperatureScaleId) {
                $scope.scale.selectedScale = $scope.scale.availableScales[i];
                break;
            }
        }

        for (var i = 0; i < $scope.resolution.availableResolutions.length; i++) {
            if ($scope.resolution.availableResolutions[i].id == sensor.dsFamilyTempSensorResolutionId) {
                $scope.resolution.selectedResolution = $scope.resolution.availableResolutions[i];
                break;
            }
        }
    }

    $scope.selectedScale = {};

}]);