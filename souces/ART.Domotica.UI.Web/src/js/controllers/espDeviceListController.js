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

app.controller('dsFamilyTempSensorItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'espDeviceService', 'dsFamilyTempSensorService', 'temperatureScaleService', function ($scope, $rootScope, $timeout, $log, toaster, espDeviceService, dsFamilyTempSensorService, temperatureScaleService) {

    $scope.sensor = {};           

    $scope.selectedHasAlarm = false;
    $scope.selectedLowAlarm = 0;
    $scope.selectedHighAlarm = 0;

    $scope.scale = {
        availableScales: temperatureScaleService.scales,
        selectedScale: {},
    };

    $scope.resolution = {
        availableResolutions: dsFamilyTempSensorService.resolutions,
        selectedResolution: {},
    };

    $scope.changeScale = function () {
        dsFamilyTempSensorService.setScale($scope.sensor.dsFamilyTempSensorId, $scope.scale.selectedScale.id);
    };

    $scope.changeResolution = function () {
        dsFamilyTempSensorService.setResolution($scope.sensor.dsFamilyTempSensorId, $scope.resolution.selectedResolution.id);
    };    

    $scope.changeHasAlarm = function () {
        
    };

    $scope.changeLowAlarm = function () {
        dsFamilyTempSensorService.setLowAlarm($scope.sensor.dsFamilyTempSensorId, $scope.selectedLowAlarm);
    };

    $scope.changeHighAlarm = function () {
        dsFamilyTempSensorService.setHighAlarm($scope.sensor.dsFamilyTempSensorId, $scope.selectedHighAlarm);
    };

    $scope.init = function (sensor) {

        $scope.sensor = sensor;
        
        $scope.scale.selectedScale = temperatureScaleService.getScaleById(sensor.temperatureScaleId);
        $scope.resolution.selectedResolution = dsFamilyTempSensorService.getResolutionById(sensor.dsFamilyTempSensorResolutionId);

        $scope.selectedHasLowAlarm = sensor.hasLowAlarm;
        $scope.selectedHasHighAlarm = sensor.hasHighAlarm;

        $scope.selectedLowAlarm = sensor.lowAlarm;
        $scope.selectedHighAlarm = sensor.highAlarm;

        clearOnSetScaleCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetScaleCompleted);
        clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetResolutionCompleted);
        clearOnSetLowAlarmCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetLowAlarmCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetLowAlarmCompleted);
        clearOnSetHighAlarmCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetHighAlarmCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetHighAlarmCompleted);
    };    

    var clearOnSetScaleCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetLowAlarmCompleted = null;
    var clearOnSetHighAlarmCompleted = null;
    
    $scope.$on('$destroy', function () {
        clearOnSetScaleCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetLowAlarmCompleted();
        clearOnSetHighAlarmCompleted();
    });

    var onSetScaleCompleted = function (event, data) {
        $scope.sensor.temperatureScaleId = data.temperatureScaleId;
        toaster.pop('success', 'Sucesso', 'escala alterada');
    };

    var onSetResolutionCompleted = function (event, data) {
        $scope.sensor.dsFamilyTempSensorResolutionId = data.dsFamilyTempSensorResolutionId;
        toaster.pop('success', 'Sucesso', 'resolução alterada');
    };

    var onSetLowAlarmCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme baixo alterado');
    };

    var onSetHighAlarmCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme alto alterado');
    }; 

}]);