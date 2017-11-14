'use strict';
app.controller('espDeviceListController', ['$scope', '$timeout', '$log', 'EventDispatcher', 'espDeviceService', 'dsFamilyTempSensorService', 'temperatureScaleService', function ($scope, $timeout, $log, EventDispatcher, espDeviceService, dsFamilyTempSensorService, temperatureScaleService) {    
   
    $scope.devices = espDeviceService.devices;
    $scope.resolutions = dsFamilyTempSensorService.resolutions;
    $scope.scales = temperatureScaleService.scales;

}]);