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

    $scope.lowAlarmView = {};
    $scope.highAlarmView = {};    

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
        if (!initialized || isNaN(alarmValue) || alarmValue === null) return;
        var alarmCelsius = temperatureScaleConverter.convertToCelsius($scope.sensor.temperatureScaleId, alarmValue);
        dsFamilyTempSensorService.setAlarmCelsius($scope.sensor.dsFamilyTempSensorId, alarmCelsius, position);        
    };

    $scope.changeAlarmBuzzerOn = function (position, alarmBuzzerOn) {
        if (!initialized) return;
        dsFamilyTempSensorService.setAlarmBuzzerOn($scope.sensor.dsFamilyTempSensorId, alarmBuzzerOn, position);        
    };

    $scope.changeChartLimiterValue = function (position, chartLimiterValue) {
        if (!initialized) return;
        var chartLimiterCelsius = temperatureScaleConverter.convertToCelsius($scope.sensor.temperatureScaleId, chartLimiterValue);
        dsFamilyTempSensorService.setChartLimiterCelsius($scope.sensor.dsFamilyTempSensorId, chartLimiterCelsius, position);
    };

    var initialized = false;

    $scope.init = function (sensor) {

        $scope.sensor = sensor;

        // Scale
        if (temperatureScaleService.initialized())
            setSelectedScale();
        else
            clearOnTemperatureScaleServiceInitialized = $rootScope.$on('TemperatureScaleService_Initialized', setSelectedScale);        

        // Resolution
        if (dsFamilyTempSensorResolutionService.initialized())
            setSelectedResolution();
        else
            clearOnDSFamilyTempSensorResolutionServiceInitialized = $rootScope.$on('DSFamilyTempSensorResolutionService_Initialized', setSelectedResolution);        
        
        // Alarm
        $scope.lowAlarmView = {
            alarmOn: sensor.lowAlarm.alarmOn,
            alarmValue: temperatureScaleConverter.convertFromCelsius(sensor.temperatureScaleId, sensor.lowAlarm.alarmCelsius),
            alarmBuzzerOn: sensor.lowAlarm.alarmBuzzerOn,
        };

        $scope.highAlarmView = {
            alarmOn: sensor.highAlarm.alarmOn,
            alarmValue: temperatureScaleConverter.convertFromCelsius(sensor.temperatureScaleId, sensor.highAlarm.alarmCelsius),
            alarmBuzzerOn: sensor.highAlarm.alarmBuzzerOn,
        };        

        // Temp Sensor Range
        $scope.tempSensorRangeView = {
            min: temperatureScaleConverter.convertFromCelsius(sensor.temperatureScaleId, sensor.tempSensorRange.min),
            max: temperatureScaleConverter.convertFromCelsius(sensor.temperatureScaleId, sensor.tempSensorRange.max),
        };  

        // Chart Limiter
        $scope.lowChartLimiterView = temperatureScaleConverter.convertFromCelsius(sensor.temperatureScaleId, sensor.lowChartLimiterCelsius);
        $scope.highChartLimiterView = temperatureScaleConverter.convertFromCelsius(sensor.temperatureScaleId, sensor.highChartLimiterCelsius);

        clearOnSetScaleCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetScaleCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetScaleCompleted);
        clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetResolutionCompleted);
        clearOnSetAlarmOnCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmOnCompleted);
        clearOnSetAlarmCelsiusCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmCelsiusCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmCelsiusCompleted);
        clearOnSetAlarmBuzzerOnCompleted = $rootScope.$on('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmBuzzerOnCompleted);        
        clearOnSetChartLimiterCelsiusCompleted = $rootScope.$on('dsFamilyTempSensorService_SetChartLimiterCelsiusCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetChartLimiterCelsiusCompleted);        

        initialized = true;
    };    
        
    var clearOnTemperatureScaleServiceInitialized = null;
    var clearOnDSFamilyTempSensorResolutionServiceInitialized = null;
    var clearOnSetScaleCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetAlarmOnCompleted = null;
    var clearOnSetAlarmCelsiusCompleted = null;
    var clearOnSetAlarmBuzzerOnCompleted = null;
    var clearOnSetChartLimiterCelsiusCompleted = null;
        
    $scope.$on('$destroy', function () {
        if (clearOnTemperatureScaleServiceInitialized != null) clearOnTemperatureScaleServiceInitialized();
        clearOnSetScaleCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetAlarmOnCompleted();
        clearOnSetAlarmCelsiusCompleted();
        clearOnSetAlarmBuzzerOnCompleted(); 
        clearOnSetChartLimiterCelsiusCompleted();
    });

    var setSelectedScale = function () {  
        $scope.scale.selectedScale = temperatureScaleService.getScaleById($scope.sensor.temperatureScaleId);
    };

    var setSelectedResolution = function () {
        $scope.resolution.selectedResolution = dsFamilyTempSensorResolutionService.getResolutionById($scope.sensor.dsFamilyTempSensorResolutionId);
    };

    var onSetScaleCompleted = function (event, data) {

        $scope.highAlarmView.alarmValue = temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, $scope.sensor.highAlarm.alarmCelsius);
        $scope.lowAlarmView.alarmValue = temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, $scope.sensor.lowAlarm.alarmCelsius);

        $scope.tempSensorRangeView.min = temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, $scope.sensor.tempSensorRange.min);
        $scope.tempSensorRangeView.max = temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, $scope.sensor.tempSensorRange.max);

        $scope.lowChartLimiterView = temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, $scope.sensor.lowChartLimiterCelsius);
        $scope.highChartLimiterView = temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, $scope.sensor.highChartLimiterCelsius);

        toaster.pop('success', 'Sucesso', 'escala alterada');
    };

    var onSetResolutionCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'resolução alterada');
    };

    var onSetAlarmOnCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme ligado/desligado');
    };

    var onSetAlarmCelsiusCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme alterado');
    };

    var onSetAlarmBuzzerOnCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'alarme buzzer ligado/desligado');
    };

    var onSetChartLimiterCelsiusCompleted = function (event, data) {
        toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
    };

    $scope.convertTemperature = function (temperature) {
        return temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, temperature);
    }  

}]);

