'use strict';
app.controller('espDeviceListController', [
    '$scope',
    '$timeout',
    '$log',
    'espDeviceService',
    'continentService',
    'countryService',
    'numericalScaleService',
    'numericalScalePrefixService',
    'numericalScaleTypeService',
    'numericalScaleTypeCountryService',
    'unitMeasurementScaleService',
    'unitMeasurementConverter',
    'unitMeasurementTypeService',
    'unitMeasurementService',
    'sensorTypeService',
    'sensorDatasheetService',
    'sensorDatasheetUnitMeasurementScaleService',
    'sensorUnitMeasurementDefaultService',
    'deviceSensorsService',
    'sensorsInDeviceService',
    'sensorService',
    function ($scope, $timeout, $log, espDeviceService) {    
   
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

app.controller('sensorTempDSFamilyItemController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'espDeviceService', 'sensorTempDSFamilyResolutionService', 'unitMeasurementConverter', 'unitMeasurementTypeService', 'unitMeasurementService', 'sensorUnitMeasurementScaleService', 'sensorTriggerService', 'sensorTempDSFamilyService', function ($scope, $rootScope, $timeout, $log, toaster, espDeviceService, sensorTempDSFamilyResolutionService, unitMeasurementConverter, unitMeasurementTypeService, unitMeasurementService, sensorUnitMeasurementScaleService, sensorTriggerService, sensorTempDSFamilyService) {

    $scope.sensor = {};           

    $scope.lowAlarmView = {};
    $scope.highAlarmView = {};    

    $scope.labelView = "";  

    $scope.unitMeasurement = {
        availableUnitMeasurements: unitMeasurementService.unitMeasurements,
        selectedUnitMeasurement: {},
    };

    $scope.resolution = {
        availableResolutions: sensorTempDSFamilyResolutionService.resolutions,
        selectedResolution: {},
    };

    $scope.changeUnitMeasurement = function () {
        if (!initialized) return;
        sensorTempDSFamilyService.setUnitMeasurement($scope.sensor.sensorTempDSFamilyId, $scope.unitMeasurement.selectedUnitMeasurement.id);
    };

    $scope.changeResolution = function () {
        if (!initialized) return;
        sensorTempDSFamilyService.setResolution($scope.sensor.sensorTempDSFamilyId, $scope.resolution.selectedResolution.id);
    }; 

    $scope.changeLabel = function () {
        if (!initialized || !$scope.labelView) return;
        sensorTempDSFamilyService.setLabel($scope.sensor.sensorTempDSFamilyId, $scope.labelView);
    };

    $scope.changeAlarmOn = function (position, alarmOn) {
        if (!initialized) return;
        sensorTriggerService.setAlarmOn($scope.sensor.sensorTempDSFamilyId, alarmOn, position);        
    };

    $scope.changeAlarmValue = function (position, alarmValue) {
        if (!initialized || isNaN(alarmValue) || alarmValue === null) return;
        var alarmCelsius = unitMeasurementConverter.convertToCelsius($scope.sensor.unitMeasurementId, alarmValue);
        sensorTriggerService.setAlarmCelsius($scope.sensor.sensorTempDSFamilyId, alarmCelsius, position);        
    };

    $scope.changeAlarmBuzzerOn = function (position, alarmBuzzerOn) {
        if (!initialized) return;
        sensorTriggerService.setAlarmBuzzerOn($scope.sensor.sensorTempDSFamilyId, alarmBuzzerOn, position);        
    }; 

    var initialized = false;

    $scope.init = function (sensor) {

        $scope.sensor = sensor;

        // UnitMeasurement
        if (unitMeasurementService.initialized())
            setSelectedUnitMeasurement();
        else
            clearOnUnitMeasurementServiceInitialized = $rootScope.$on('UnitMeasurementService_Initialized', setSelectedUnitMeasurement);        

        // Resolution
        if (sensorTempDSFamilyResolutionService.initialized())
            setSelectedResolution();
        else
            clearOnSensorTempDSFamilyResolutionServiceInitialized = $rootScope.$on('SensorTempDSFamilyResolutionService_Initialized', setSelectedResolution);        

        // Label
        $scope.labelView = sensor.label;

        // Alarm
        $scope.lowAlarmView = {
            alarmOn: sensor.lowAlarm.alarmOn,
            alarmValue: unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.lowAlarm.alarmCelsius),
            alarmBuzzerOn: sensor.lowAlarm.alarmBuzzerOn,
        };

        $scope.highAlarmView = {
            alarmOn: sensor.highAlarm.alarmOn,
            alarmValue: unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.highAlarm.alarmCelsius),
            alarmBuzzerOn: sensor.highAlarm.alarmBuzzerOn,
        };        
                     
        clearOnSetUnitMeasurementCompleted = $rootScope.$on('sensorTempDSFamilyService_onSetUnitMeasurementCompleted_Id_' + $scope.sensor.sensorTempDSFamilyId, onSetUnitMeasurementCompleted);
        clearOnSetResolutionCompleted = $rootScope.$on('sensorTempDSFamilyService_onSetResolutionCompleted_Id_' + $scope.sensor.sensorTempDSFamilyId, onSetResolutionCompleted);
        clearOnSetLabelCompleted = $rootScope.$on('sensorTempDSFamilyService_onSetLabelCompleted_Id_' + $scope.sensor.sensorTempDSFamilyId, onSetLabelCompleted);
        clearOnSetAlarmOnCompleted = $rootScope.$on('sensorTriggerService_onSetAlarmOnCompleted_Id_' + $scope.sensor.sensorTempDSFamilyId, onSetAlarmOnCompleted);
        clearOnSetAlarmCelsiusCompleted = $rootScope.$on('sensorTriggerService_onSetAlarmCelsiusCompleted_Id_' + $scope.sensor.sensorTempDSFamilyId, onSetAlarmCelsiusCompleted);
        clearOnSetAlarmBuzzerOnCompleted = $rootScope.$on('sensorTriggerService_SetAlarmBuzzerOnCompleted_Id_' + $scope.sensor.sensorTempDSFamilyId, onSetAlarmBuzzerOnCompleted);        
        clearOnReadReceived = $rootScope.$on('ESPDeviceService_onReadReceived', onReadReceived);        

        initialized = true;
    };    
        
    var clearOnUnitMeasurementServiceInitialized = null;
    var clearOnSensorTempDSFamilyResolutionServiceInitialized = null;
    var clearOnSetUnitMeasurementCompleted = null;
    var clearOnSetResolutionCompleted = null;
    var clearOnSetLabelCompleted = null;
    var clearOnSetAlarmOnCompleted = null;
    var clearOnSetAlarmCelsiusCompleted = null;
    var clearOnSetAlarmBuzzerOnCompleted = null;
    var clearOnReadReceived = null;

    $scope.$on('$destroy', function () {
        if (clearOnUnitMeasurementServiceInitialized !== null) clearOnUnitMeasurementServiceInitialized();
        clearOnSetUnitMeasurementCompleted();
        clearOnSetResolutionCompleted();
        clearOnSetLabelCompleted();
        clearOnSetAlarmOnCompleted();
        clearOnSetAlarmCelsiusCompleted();
        clearOnSetAlarmBuzzerOnCompleted(); 
        clearOnReadReceived();
    });

    var setSelectedUnitMeasurement = function () {  
        $scope.unitMeasurement.selectedUnitMeasurement = unitMeasurementService.getByKey($scope.sensor.unitMeasurementId);
    };

    var setSelectedResolution = function () {
        $scope.resolution.selectedResolution = sensorTempDSFamilyResolutionService.getResolutionById($scope.sensor.sensorTempDSFamilyResolutionId);
    };

    var onSetUnitMeasurementCompleted = function (event, data) {

        setSelectedUnitMeasurement();

        $scope.highAlarmView.alarmValue = unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, $scope.sensor.highAlarm.alarmCelsius);
        $scope.lowAlarmView.alarmValue = unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, $scope.sensor.lowAlarm.alarmCelsius);

        $scope.lowChartLimiterView = unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, $scope.sensor.lowChartLimiterCelsius);
        $scope.highChartLimiterView = unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, $scope.sensor.highChartLimiterCelsius);

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
            $scope.highAlarmView.alarmValue = unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, data.alarmCelsius);
            toaster.pop('success', 'Sucesso', 'Alarme alto alterado');
        }
        else if (data.position === 'Min') {
            $scope.lowAlarmView.alarmValue = unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, data.alarmCelsius);
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

    var onReadReceived = function (event, data) {
        $scope.$apply();
    };

    $scope.convertTemperature = function (temperature) {
        return unitMeasurementConverter.convertFromCelsius($scope.sensor.unitMeasurementId, temperature);
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

