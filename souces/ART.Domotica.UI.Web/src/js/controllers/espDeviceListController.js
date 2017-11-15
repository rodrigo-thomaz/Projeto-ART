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

app.controller('dsFamilyTempSensorItemController', ['$scope', '$rootScope', '$timeout', '$log', 'EventDispatcher', 'espDeviceService', 'dsFamilyTempSensorService', 'temperatureScaleService', function ($scope, $rootScope, $timeout, $log, EventDispatcher, espDeviceService, dsFamilyTempSensorService, temperatureScaleService) {

    $scope.sensor = {};           

    $scope.scale = {
        availableScales: temperatureScaleService.scales,
        selectedScale: {},
    };

    $scope.resolution = {
        availableResolutions: dsFamilyTempSensorService.resolutions,
        selectedResolution: {},
    };

    $scope.changeScale = function () {
        //temperatureScaleService.setScale();
        alert($scope.scale.selectedScale.name);
    };

    $scope.changeResolution = function () {
        dsFamilyTempSensorService.setResolution($scope.sensor.dsFamilyTempSensorId, $scope.resolution.selectedResolution.id);
    };

    $scope.init = function (sensor) {

        $scope.sensor = sensor;
        
        $scope.scale.selectedScale = temperatureScaleService.getScaleById(sensor.temperatureScaleId);

        $scope.resolution.selectedResolution = dsFamilyTempSensorService.getResolutionById(sensor.dsFamilyTempSensorResolutionId);
        
    };

    var clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted', function (event, data) {
        if ($scope.sensor.dsFamilyTempSensorId == data.dsFamilyTempSensorId) {
            alert($scope.sensor.dsFamilyTempSensorId);
        }
        else {
            alert("Passei aqui");
        }        
    });

    $scope.$on('$destroy', function () {
        clearOnSetResolutionCompleted();
    });

    var onSetLowAlarmCompleted = function () {
        alert("onSetLowAlarmCompleted");
    };

    var onSetHighAlarmCompleted = function () {
        alert("onSetHighAlarmCompleted");
    };

    EventDispatcher.on('dsFamilyTempSensorService_onSetLowAlarmCompleted', onSetLowAlarmCompleted);
    EventDispatcher.on('dsFamilyTempSensorService_onSetHighAlarmCompleted', onSetHighAlarmCompleted);  

}]);