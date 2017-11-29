'use strict';
app.controller('espDeviceListController', ['$scope', '$timeout', '$log', 'espDeviceService', function ($scope, $timeout, $log, espDeviceService) {    
   
    $scope.devices = espDeviceService.devices;    

}]);

app.controller('espDeviceItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'timeZoneService', 'espDeviceService', 'deviceNTPService', function ($scope, $rootScope, $timeout, $log, toaster, timeZoneService, espDeviceService, deviceNTPService) {

    $scope.device = {};

    var initialized = false;

    $scope.init = function (device) {

        $scope.device = device; 

        $scope.labelView = device.label;

        clearOnSetLabelCompleted = $rootScope.$on('espDeviceService_onSetLabelCompleted_Id_' + $scope.device.deviceId, onSetLabelCompleted);        

        initialized = true;
    }

    var clearOnSetLabelCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetLabelCompleted();
    });

    var onSetLabelCompleted = function (event, data) {
        $scope.labelView = data.label;
        toaster.pop('success', 'Sucesso', 'Label alterado');
    };    

    $scope.changeLabel = function () {
        if (!initialized || !$scope.labelView) return;
        espDeviceService.setLabel($scope.device.deviceId, $scope.labelView);
    };

}]);

app.controller('deviceNTPController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'timeZoneService', 'deviceNTPService', function ($scope, $rootScope, $timeout, $log, toaster, timeZoneService, deviceNTPService) {

    $scope.deviceId = null;
    $scope.deviceNTP = {};

    var initialized = false;

    $scope.init = function (deviceId, deviceNTP) {

        $scope.deviceId = deviceId;
        $scope.deviceNTP = deviceNTP;

        $scope.updateIntervalInMilliSecondView = deviceNTP.updateIntervalInMilliSecond;

        // Time Zone
        if (timeZoneService.initialized())
            setSelectedTimeZone();
        else
            clearOnTimeZoneServiceInitialized = $rootScope.$on('timeZoneService_Initialized', setSelectedTimeZone);

        clearOnSetTimeZoneCompleted = $rootScope.$on('espDeviceService_onSetTimeZoneCompleted_Id_' + $scope.deviceId, onSetTimeZoneCompleted);
        clearOnSetUpdateIntervalInMilliSecondCompleted = $rootScope.$on('espDeviceService_onSetUpdateIntervalInMilliSecondCompleted_Id_' + $scope.deviceId, onSetUpdateIntervalInMilliSecondCompleted);

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
        $scope.timeZone.selectedTimeZone = timeZoneService.getTimeZoneById($scope.deviceNTP.timeZoneId);
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
        deviceNTPService.setTimeZone($scope.deviceId, $scope.timeZone.selectedTimeZone.id);
    };

    $scope.changeUpdateIntervalInMilliSecond = function () {
        if (!initialized || !$scope.updateIntervalInMilliSecondView) return;
        deviceNTPService.setUpdateIntervalInMilliSecond($scope.deviceId, $scope.updateIntervalInMilliSecondView);
    };

}]);

app.controller('dsFamilyTempSensorItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'espDeviceService', 'dsFamilyTempSensorResolutionService', 'unitOfMeasurementConverter', 'unitOfMeasurementTypeService', 'unitOfMeasurementService', 'sensorRangeService', 'sensorChartLimiterService', 'sensorTriggerService', 'dsFamilyTempSensorService', function ($scope, $rootScope, $timeout, $log, toaster, espDeviceService, dsFamilyTempSensorResolutionService, unitOfMeasurementConverter, unitOfMeasurementTypeService, unitOfMeasurementService, sensorRangeService, sensorChartLimiterService, sensorTriggerService, dsFamilyTempSensorService) {

    $scope.sensor = {};           

    $scope.lowAlarmView = {};
    $scope.highAlarmView = {};    

    $scope.labelView = "";  

    $scope.unitOfMeasurement = {
        availableUnitOfMeasurements: unitOfMeasurementService.unitOfMeasurements,
        selectedUnitOfMeasurement: {},
    };

    $scope.resolution = {
        availableResolutions: dsFamilyTempSensorResolutionService.resolutions,
        selectedResolution: {},
    };

    $scope.changeUnitOfMeasurement = function () {
        if (!initialized) return;
        dsFamilyTempSensorService.setUnitOfMeasurement($scope.sensor.dsFamilyTempSensorId, $scope.unitOfMeasurement.selectedUnitOfMeasurement.id);
    };

    $scope.changeResolution = function () {
        if (!initialized) return;
        dsFamilyTempSensorService.setResolution($scope.sensor.dsFamilyTempSensorId, $scope.resolution.selectedResolution.id);
    }; 

    $scope.changeLabel = function () {
        if (!initialized || !$scope.labelView) return;
        dsFamilyTempSensorService.setLabel($scope.sensor.dsFamilyTempSensorId, $scope.labelView);
    };

    $scope.changeAlarmOn = function (position, alarmOn) {
        if (!initialized) return;
        sensorTriggerService.setAlarmOn($scope.sensor.dsFamilyTempSensorId, alarmOn, position);        
    };

    $scope.changeAlarmValue = function (position, alarmValue) {
        if (!initialized || isNaN(alarmValue) || alarmValue === null) return;
        var alarmCelsius = unitOfMeasurementConverter.convertToCelsius($scope.sensor.unitOfMeasurementId, alarmValue);
        sensorTriggerService.setAlarmCelsius($scope.sensor.dsFamilyTempSensorId, alarmCelsius, position);        
    };

    $scope.changeAlarmBuzzerOn = function (position, alarmBuzzerOn) {
        if (!initialized) return;
        sensorTriggerService.setAlarmBuzzerOn($scope.sensor.dsFamilyTempSensorId, alarmBuzzerOn, position);        
    };

    $scope.changeChartLimiterValue = function (position, chartLimiterValue) {
        if (!initialized || chartLimiterValue === undefined) return;
        var value = unitOfMeasurementConverter.convertToCelsius($scope.sensor.unitOfMeasurementId, chartLimiterValue);
        sensorChartLimiterService.setValue($scope.sensor.dsFamilyTempSensorId, value, position);
    };

    var initialized = false;

    $scope.init = function (sensor) {

        $scope.sensor = sensor;

        $scope.sensorRangeView = {};

        // UnitOfMeasurement
        if (unitOfMeasurementService.initialized())
            setSelectedUnitOfMeasurement();
        else
            clearOnUnitOfMeasurementServiceInitialized = $rootScope.$on('UnitOfMeasurementService_Initialized', setSelectedUnitOfMeasurement);        

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
            alarmValue: unitOfMeasurementConverter.convertFromCelsius(sensor.unitOfMeasurementId, sensor.lowAlarm.alarmCelsius),
            alarmBuzzerOn: sensor.lowAlarm.alarmBuzzerOn,
        };

        $scope.highAlarmView = {
            alarmOn: sensor.highAlarm.alarmOn,
            alarmValue: unitOfMeasurementConverter.convertFromCelsius(sensor.unitOfMeasurementId, sensor.highAlarm.alarmCelsius),
            alarmBuzzerOn: sensor.highAlarm.alarmBuzzerOn,
        };        

        // Temp Sensor Range
        if (sensorRangeService.initialized()) {
            setSensorRange();
        }
        else {
            clearOnSensorRangeServiceInitialized = $rootScope.$on('sensorRangeService_Initialized', setSensorRange);        
        }

        // Chart Limiter
        $scope.lowChartLimiterView = unitOfMeasurementConverter.convertFromCelsius(sensor.unitOfMeasurementId, sensor.lowChartLimiterCelsius);
        $scope.highChartLimiterView = unitOfMeasurementConverter.convertFromCelsius(sensor.unitOfMeasurementId, sensor.highChartLimiterCelsius);

        clearOnSetUnitOfMeasurementCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetUnitOfMeasurementCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetUnitOfMeasurementCompleted);
        clearOnSetResolutionCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetResolutionCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetResolutionCompleted);
        clearOnSetLabelCompleted = $rootScope.$on('dsFamilyTempSensorService_onSetLabelCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetLabelCompleted);
        clearOnSetAlarmOnCompleted = $rootScope.$on('sensorTriggerService_onSetAlarmOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmOnCompleted);
        clearOnSetAlarmCelsiusCompleted = $rootScope.$on('sensorTriggerService_onSetAlarmCelsiusCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmCelsiusCompleted);
        clearOnSetAlarmBuzzerOnCompleted = $rootScope.$on('sensorTriggerService_SetAlarmBuzzerOnCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetAlarmBuzzerOnCompleted);        
        clearOnSetChartLimiterCelsiusCompleted = $rootScope.$on('sensorChartLimiterService_SetValueCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetChartLimiterCelsiusCompleted);        
        clearOnReadReceived = $rootScope.$on('ESPDeviceService_onReadReceived', onReadReceived);        

        initialized = true;
    };    
        
    var clearOnUnitOfMeasurementServiceInitialized = null;
    var clearOnDSFamilyTempSensorResolutionServiceInitialized = null;
    var clearOnSetUnitOfMeasurementCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetLabelCompleted = null;
    var clearOnSetAlarmOnCompleted = null;
    var clearOnSetAlarmCelsiusCompleted = null;
    var clearOnSetAlarmBuzzerOnCompleted = null;
    var clearOnSetChartLimiterCelsiusCompleted = null;
    var clearOnReadReceived = null;
    var clearOnSensorRangeServiceInitialized = null;

    $scope.$on('$destroy', function () {
        if (clearOnUnitOfMeasurementServiceInitialized !== null) clearOnUnitOfMeasurementServiceInitialized();
        if (clearOnSensorRangeServiceInitialized !== null) clearOnSensorRangeServiceInitialized();
        clearOnSetUnitOfMeasurementCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetLabelCompleted();
        clearOnSetAlarmOnCompleted();
        clearOnSetAlarmCelsiusCompleted();
        clearOnSetAlarmBuzzerOnCompleted(); 
        clearOnSetChartLimiterCelsiusCompleted();
        clearOnReadReceived();
    });

    var setSelectedUnitOfMeasurement = function () {  
        $scope.unitOfMeasurement.selectedUnitOfMeasurement = unitOfMeasurementService.getByKey($scope.sensor.unitOfMeasurementId);
    };

    var setSelectedResolution = function () {
        $scope.resolution.selectedResolution = dsFamilyTempSensorResolutionService.getResolutionById($scope.sensor.dsFamilyTempSensorResolutionId);
    };

    var setSensorRange = function () {
        var sensorRange = sensorRangeService.getById($scope.sensor.sensorRangeId);        
        $scope.sensorRangeView.min = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, sensorRange.min);
        $scope.sensorRangeView.max = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, sensorRange.max);        
    };

    var onSetUnitOfMeasurementCompleted = function (event, data) {

        setSelectedUnitOfMeasurement();

        $scope.highAlarmView.alarmValue = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, $scope.sensor.highAlarm.alarmCelsius);
        $scope.lowAlarmView.alarmValue = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, $scope.sensor.lowAlarm.alarmCelsius);

        setSensorRange();

        $scope.lowChartLimiterView = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, $scope.sensor.lowChartLimiterCelsius);
        $scope.highChartLimiterView = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, $scope.sensor.highChartLimiterCelsius);

        toaster.pop('success', 'Sucesso', 'escala alterada');
    };

    var onSetResolutionCompleted = function (event, data) {
        setSelectedResolution();
        toaster.pop('success', 'Sucesso', 'resolução alterada');
    };

    var onSetLabelCompleted = function (event, data) {
        $scope.labelView = data.label;
        toaster.pop('success', 'Sucesso', 'label alterado');
    };

    var onSetAlarmOnCompleted = function (event, data) {
        if (data.position === 'Max') {
            $scope.highAlarmView.alarmOn = data.alarmOn;
            toaster.pop('success', 'Sucesso', 'Alarme alto ligado/desligado');
        }
        else if (data.position === 'Min') {
            $scope.lowAlarmView.alarmOn = data.alarmOn;
            toaster.pop('success', 'Sucesso', 'Alarme baixo ligado/desligado');
        }
    };

    var onSetAlarmCelsiusCompleted = function (event, data) {
        if (data.position === 'Max') {
            $scope.highAlarmView.alarmValue = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, data.alarmCelsius);
            toaster.pop('success', 'Sucesso', 'Alarme alto alterado');
        }
        else if (data.position === 'Min') {
            $scope.lowAlarmView.alarmValue = unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, data.alarmCelsius);
            toaster.pop('success', 'Sucesso', 'Alarme baixo alterado');
        }
    };

    var onSetAlarmBuzzerOnCompleted = function (event, data) {
        if (data.position === 'Max') {
            $scope.highAlarmView.alarmBuzzerOn = data.alarmBuzzerOn;
            toaster.pop('success', 'Sucesso', 'Alarme buzzer alto ligado/desligado');
        }
        else if (data.position === 'Min') {
            $scope.lowAlarmView.alarmBuzzerOn = data.alarmBuzzerOn;
            toaster.pop('success', 'Sucesso', 'Alarme buzzer baixo ligado/desligado');
        }
    };

    var onSetChartLimiterCelsiusCompleted = function (event, data) {
        if (data.position === 'Max') {
            $scope.highChartLimiterView = data.value;
            toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
        }
        else if (data.position === 'Min') {
            $scope.lowChartLimiterView = data.value;
            toaster.pop('success', 'Sucesso', 'Limite baixo do gráfico alterado');
        }
    };

    var onReadReceived = function (event, data) {
        $scope.$apply();
    };

    $scope.convertTemperature = function (temperature) {
        return unitOfMeasurementConverter.convertFromCelsius($scope.sensor.unitOfMeasurementId, temperature);
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

