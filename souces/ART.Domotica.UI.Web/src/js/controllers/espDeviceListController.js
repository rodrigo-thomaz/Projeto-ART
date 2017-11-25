'use strict';
app.controller('espDeviceListController', ['$scope', '$timeout', '$log', 'espDeviceService', function ($scope, $timeout, $log, espDeviceService) {    
   
    $scope.devices = espDeviceService.devices;    

}]);

app.controller('espDeviceItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'timeZoneService', 'espDeviceService', function ($scope, $rootScope, $timeout, $log, toaster, timeZoneService, espDeviceService) {

    $scope.device = {};

    var initialized = false;

    $scope.init = function (device) {

        $scope.device = device; 

        $scope.updateIntervalInMilliSecondView = device.deviceNTP.updateIntervalInMilliSecond;

        // Time Zone
        if (timeZoneService.initialized())
            setSelectedTimeZone();
        else
            clearOnTimeZoneServiceInitialized = $rootScope.$on('timeZoneService_Initialized', setSelectedTimeZone);        


        clearOnSetTimeZoneCompleted = $rootScope.$on('espDeviceService_onSetTimeZoneCompleted_Id_' + $scope.device.deviceId, onSetTimeZoneCompleted);        
        clearOnSetUpdateIntervalInMilliSecondCompleted = $rootScope.$on('espDeviceService_onSetUpdateIntervalInMilliSecondCompleted_Id_' + $scope.device.deviceId, onSetUpdateIntervalInMilliSecondCompleted);        

        initialized = true;
    }

    var clearOnTimeZoneServiceInitialized = null;
    var clearOnSetTimeZoneCompleted = null;
    var clearOnSetUpdateIntervalInMilliSecondCompleted = null;

    $scope.$on('$destroy', function () {
        if (clearOnTimeZoneServiceInitialized !== null) clearOnTimeZoneServiceInitialized();
        clearOnSetTimeZoneCompleted();
        clearOnSetUpdateIntervalInMilliSecondCompleted();
    });

    var setSelectedTimeZone = function () {
        $scope.timeZone.selectedTimeZone = timeZoneService.getTimeZoneById($scope.device.deviceNTP.timeZoneId);
    };

    var onSetTimeZoneCompleted = function (event, data) {
        setSelectedTimeZone();
        toaster.pop('success', 'Sucesso', 'Fuso horário alterado');
    };

    var onSetUpdateIntervalInMilliSecondCompleted = function (event, data) {
        $scope.updateIntervalInMilliSecondView = data.updateIntervalInMilliSecond;
        toaster.pop('success', 'Sucesso', 'UpdateIntervalInMilliSecond alterado');
    };

    $scope.timeZone = {
        availableTimeZones: timeZoneService.timeZones,
        selectedTimeZone: {},
    };

    $scope.changeTimeZone = function () {
        if (!initialized) return;
        espDeviceService.setTimeZone($scope.device.deviceId, $scope.timeZone.selectedTimeZone.id);
    }; 

    $scope.changeUpdateIntervalInMilliSecond = function () {
        if (!initialized) return;
        espDeviceService.setUpdateIntervalInMilliSecond($scope.device.deviceId, $scope.updateIntervalInMilliSecondView);
    };

}]);

app.controller('dsFamilyTempSensorItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'espDeviceService', 'dsFamilyTempSensorResolutionService', 'temperatureScaleConverter', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $rootScope, $timeout, $log, toaster, espDeviceService, dsFamilyTempSensorResolutionService, temperatureScaleConverter, temperatureScaleService, dsFamilyTempSensorService) {

    $scope.sensor = {};           

    $scope.lowAlarmView = {};
    $scope.highAlarmView = {};    

    $scope.labelView = "";  

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

    $scope.changeLabel = function () {
        if (!initialized) return;
        dsFamilyTempSensorService.setLabel($scope.sensor.dsFamilyTempSensorId, $scope.labelView);
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

        // Label
        $scope.labelView = sensor.label;

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
        clearOnSetLabelCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetLabelCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetLabelCompleted);
        clearOnSetAlarmOnCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmOnCompleted);
        clearOnSetAlarmCelsiusCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetAlarmCelsiusCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmCelsiusCompleted);
        clearOnSetAlarmBuzzerOnCompleted = $rootScope.$on('dsFamilyTempSensorService_SetAlarmBuzzerOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmBuzzerOnCompleted);        
        clearOnSetChartLimiterCelsiusCompleted = $rootScope.$on('dsFamilyTempSensorService_SetChartLimiterCelsiusCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetChartLimiterCelsiusCompleted);        
        clearOnReadReceived = $rootScope.$on('ESPDeviceService_onReadReceived', onReadReceived);        

        initialized = true;
    };    
        
    var clearOnTemperatureScaleServiceInitialized = null;
    var clearOnDSFamilyTempSensorResolutionServiceInitialized = null;
    var clearOnSetScaleCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetLabelCompleted = null;
    var clearOnSetAlarmOnCompleted = null;
    var clearOnSetAlarmCelsiusCompleted = null;
    var clearOnSetAlarmBuzzerOnCompleted = null;
    var clearOnSetChartLimiterCelsiusCompleted = null;
    var clearOnReadReceived = null;
        
    $scope.$on('$destroy', function () {
        if (clearOnTemperatureScaleServiceInitialized !== null) clearOnTemperatureScaleServiceInitialized();
        clearOnSetScaleCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetLabelCompleted();
        clearOnSetAlarmOnCompleted();
        clearOnSetAlarmCelsiusCompleted();
        clearOnSetAlarmBuzzerOnCompleted(); 
        clearOnSetChartLimiterCelsiusCompleted();
        clearOnReadReceived();
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

    var onSetLabelCompleted = function (event, data) {
        $scope.labelView = data.label;
        toaster.pop('success', 'Sucesso', 'label alterado');
    };

    var onSetAlarmOnCompleted = function (event, data) {
        if (data.position === 'High')
            toaster.pop('success', 'Sucesso', 'Alarme alto ligado/desligado');
        else if (data.position === 'Low')
            toaster.pop('success', 'Sucesso', 'Alarme baixo ligado/desligado');
    };

    var onSetAlarmCelsiusCompleted = function (event, data) {
        if (data.position === 'High')
            toaster.pop('success', 'Sucesso', 'Alarme alto alterado');
        else if (data.position === 'Low')
            toaster.pop('success', 'Sucesso', 'Alarme baixo alterado');
    };

    var onSetAlarmBuzzerOnCompleted = function (event, data) {
        if (data.position === 'High')
            toaster.pop('success', 'Sucesso', 'Alarme buzzer alto ligado/desligado');
        else if (data.position === 'Low')
            toaster.pop('success', 'Sucesso', 'Alarme buzzer baixo ligado/desligado');
    };

    var onSetChartLimiterCelsiusCompleted = function (event, data) {
        if(data.position === 'High')
            toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
        else if (data.position === 'Low')
            toaster.pop('success', 'Sucesso', 'Limite baixo do gráfico alterado');
    };

    var onReadReceived = function (event, data) {
        $scope.$apply();
    };

    $scope.convertTemperature = function (temperature) {
        return temperatureScaleConverter.convertFromCelsius($scope.sensor.temperatureScaleId, temperature);
    }  

    $scope.options = {
        chart: {
            type: 'lineChart',
            height: 240,
            margin: {
                top: 20,
                right: 30,
                bottom: 35,
                left: 40
            },
            x: function (d) {
                if (d === null) return null;
                return d.epochTime;
            },
            y: function (d) {
                if (d === null) return null;
                return d.temperature;
            },
            useInteractiveGuideline: true,
            duration: 0,
            xAxis: {
                //axisLabel: 'Tempo',
                tickFormat: function (d) {
                    return new Date(d * 1000).toLocaleTimeString();
                },
                tickPadding: 18,
                axisLabelDistance: 0,
            },
            yAxis: {
                //axisLabel: 'Temperatura',
                tickFormat: function (d) {
                    return d3.format('.02f')(d);
                },
                tickPadding: 5,
                axisLabelDistance: 0,
            },
            forceY: [
                $scope.lowChartLimiterView,
                $scope.highChartLimiterView
            ],
        }
    };   

}]);

