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

        clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetResolutionCompleted);
        clearOnSetLowAlarmCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetLowAlarmCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetLowAlarmCompleted);
        clearOnSetHighAlarmCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetHighAlarmCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetHighAlarmCompleted);
    };    

    var clearOnSetResolutionCompleted = null;
    var clearOnSetLowAlarmCompleted = null;
    var clearOnSetHighAlarmCompleted = null;
    
    $scope.$on('$destroy', function () {
        clearOnSetResolutionCompleted();
        clearOnSetLowAlarmCompleted();
        clearOnSetHighAlarmCompleted();
    });

    var onSetResolutionCompleted = function (event, data) {
        $scope.sensor.dsFamilyTempSensorResolutionId = data.dsFamilyTempSensorResolutionId;
        alert("onSetResolutionCompleted");
    };

    var onSetLowAlarmCompleted = function (event, data) {
        alert("onSetLowAlarmCompleted");
    };

    var onSetHighAlarmCompleted = function (event, data) {
        alert("onSetHighAlarmCompleted");
    }; 

}]);