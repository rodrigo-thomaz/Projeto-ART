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

app.controller('dsFamilyTempSensorItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'espDeviceService', 'dsFamilyTempSensorResolutionService', 'temperatureScaleConverter', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $rootScope, $timeout, $log, toaster, espDeviceService, dsFamilyTempSensorResolutionService, temperatureScaleConverter, temperatureScaleService, dsFamilyTempSensorService) {

    $scope.sensor = {};           

    $scope.selectedLowAlarm = {};
    $scope.selectedHighAlarm = {};

    $scope.scale = {
        availableScales: temperatureScaleService.scales,
        selectedScale: {},
    };

    $scope.resolution = {
        availableResolutions: dsFamilyTempSensorResolutionService.resolutions,
        selectedResolution: {},
    };

    $scope.changeScale = function () {
        if (!initialized) return;
        dsFamilyTempSensorService.setScale($scope.sensor.dsFamilyTempSensorId, $scope.scale.selectedScale.id);
    };

    $scope.changeResolution = function () {
        if (!initialized) return;
        dsFamilyTempSensorService.setResolution($scope.sensor.dsFamilyTempSensorId, $scope.resolution.selectedResolution.id);
    };   

    $scope.changeAlarmOn = function (position, alarmOn) {
        if (!initialized) return;
        dsFamilyTempSensorService.setAlarmOn($scope.sensor.dsFamilyTempSensorId, alarmOn, position);        
    };

    $scope.changeAlarmValue = function (position, alarmValue) {
        if (!initialized) return;
        var alarmValueRaw = temperatureScaleConverter.convertToRaw($scope.sensor.temperatureScaleId, alarmValue);
        dsFamilyTempSensorService.setAlarmValue($scope.sensor.dsFamilyTempSensorId, alarmValueRaw, position);        
    };

    $scope.changeAlarmBuzzerOn = function (position, alarmBuzzerOn) {
        if (!initialized) return;
        dsFamilyTempSensorService.setAlarmBuzzerOn($scope.sensor.dsFamilyTempSensorId, alarmBuzzerOn, position);        
    };

    var initialized = false;

    $scope.init = function (sensor) {

        $scope.sensor = sensor;

        if (temperatureScaleService.initialized())
            setSelectedScale();
        else
            clearOnTemperatureScaleServiceInitialized = $rootScope.$on('TemperatureScaleService_Initialized', setSelectedScale);        

        if (dsFamilyTempSensorResolutionService.initialized())
            setSelectedResolution();
        else
            clearOnDSFamilyTempSensorResolutionServiceInitialized = $rootScope.$on('DSFamilyTempSensorResolutionService_Initialized', setSelectedResolution);        
                

        $scope.selectedLowAlarm = sensor.lowAlarm;
        $scope.selectedHighAlarm = sensor.highAlarm;

        var lowAlarmValueConverted = temperatureScaleConverter.convertFromRaw(sensor.temperatureScaleId, sensor.lowAlarm.alarmValue);
        var highAlarmValueConverted = temperatureScaleConverter.convertFromRaw(sensor.temperatureScaleId, sensor.highAlarm.alarmValue);

        $scope.sensor.lowAlarm.alarmValue = lowAlarmValueConverted;
        $scope.sensor.highAlarm.alarmValue = highAlarmValueConverted;


        clearOnSetScaleCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetScaleCompleted);
        clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetResolutionCompleted);
        clearOnSetAlarmOnCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmOnCompleted);
        clearOnSetAlarmValueCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmValueCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmValueCompleted);
        clearOnSetAlarmBuzzerOnCompleted = $rootScope.$on('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmBuzzerOnCompleted);        

        initialized = true;
    };    
        
    var clearOnTemperatureScaleServiceInitialized = null;
    var clearOnDSFamilyTempSensorResolutionServiceInitialized = null;
    var clearOnSetScaleCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetAlarmOnCompleted = null;
    var clearOnSetAlarmValueCompleted = null;
    var clearOnSetAlarmBuzzerOnCompleted = null;
        
    $scope.$on('$destroy', function () {
        if (clearOnTemperatureScaleServiceInitialized != null) clearOnTemperatureScaleServiceInitialized();
        clearOnSetScaleCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetAlarmOnCompleted();
        clearOnSetAlarmValueCompleted();
        clearOnSetAlarmBuzzerOnCompleted();        
    });

    var setSelectedScale = function () {  
        $scope.scale.selectedScale = temperatureScaleService.getScaleById($scope.sensor.temperatureScaleId);
    };

    var setSelectedResolution = function () {
        $scope.resolution.selectedResolution = dsFamilyTempSensorResolutionService.getResolutionById($scope.sensor.dsFamilyTempSensorResolutionId);
    };

    var onSetScaleCompleted = function (event, data) {
        $scope.sensor.rawTemperature = temperatureScaleConverter.convertFromRaw($scope.sensor.temperatureScaleId, $scope.sensor.rawTemperature);
        $scope.sensor.highAlarm.alarmValue = temperatureScaleConverter.convertFromRaw($scope.sensor.temperatureScaleId, $scope.sensor.highAlarm.alarmValue);
        $scope.sensor.lowAlarm.alarmValue = temperatureScaleConverter.convertFromRaw($scope.sensor.temperatureScaleId, $scope.sensor.lowAlarm.alarmValue);
        toaster.pop('success', 'Sucesso', 'escala alterada');
    };

    var onSetResolutionCompleted = function (event, data) {
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


    $scope.convertTemperature = function (raw) {
        return temperatureScaleConverter.convertFromRaw($scope.sensor.temperatureScaleId, raw);
    }  

}]);

