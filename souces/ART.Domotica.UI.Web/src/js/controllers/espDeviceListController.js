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

    $scope.selectedLowAlarm = {
        alarmOn: false,
        alarmValue: -55,
        alarmBuzzerOn: false,
    };

    $scope.selectedHighAlarm = {
        alarmOn: false,
        alarmValue: 125,
        alarmBuzzerOn: false,
    };

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

    $scope.changeAlarmOn = function (position, alarmOn) {
        dsFamilyTempSensorService.setAlarmOn($scope.sensor.dsFamilyTempSensorId, alarmOn, position);        
    };

    $scope.changeAlarmValue = function (position, alarmValue) {
        dsFamilyTempSensorService.setAlarmValue($scope.sensor.dsFamilyTempSensorId, alarmValue, position);        
    };

    $scope.changeAlarmBuzzerOn = function (position, alarmBuzzerOn) {
        dsFamilyTempSensorService.setAlarmBuzzerOn($scope.sensor.dsFamilyTempSensorId, alarmBuzzerOn, position);        
    };

    $scope.init = function (sensor) {

        $scope.sensor = sensor;
        
        $scope.scale.selectedScale = temperatureScaleService.getScaleById(sensor.temperatureScaleId);
        $scope.resolution.selectedResolution = dsFamilyTempSensorService.getResolutionById(sensor.dsFamilyTempSensorResolutionId);

        $scope.selectedLowAlarm = sensor.lowAlarm;
        $scope.selectedHighAlarm = sensor.highAlarm;

        clearOnSetScaleCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetScaleCompleted);
        clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetResolutionCompleted);
        clearOnSetAlarmOnCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmOnCompleted);
        clearOnSetAlarmValueCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmValueCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmValueCompleted);
        clearOnSetAlarmBuzzerOnCompleted = $rootScope.$on('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmBuzzerOnCompleted);
    };    

    var clearOnSetScaleCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetAlarmOnCompleted = null;
    var clearOnSetAlarmValueCompleted = null;
    var clearOnSetAlarmBuzzerOnCompleted = null;
    
    $scope.$on('$destroy', function () {
        clearOnSetScaleCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetAlarmOnCompleted();
        clearOnSetAlarmValueCompleted();
        clearOnSetAlarmBuzzerOnCompleted();
    });

    var onSetScaleCompleted = function (event, data) {
        $scope.sensor.temperatureScaleId = data.temperatureScaleId;
        toaster.pop('success', 'Sucesso', 'escala alterada');
    };

    var onSetResolutionCompleted = function (event, data) {
        $scope.sensor.dsFamilyTempSensorResolutionId = data.dsFamilyTempSensorResolutionId;
        toaster.pop('success', 'Sucesso', 'resolução alterada');
    };

    var onSetAlarmOnCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme ligado/desligado');
    };

    var onSetAlarmValueCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme alterado');
    };

    var onSetAlarmBuzzerOnCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme buzzer ligado/desligado');
    };

}]);