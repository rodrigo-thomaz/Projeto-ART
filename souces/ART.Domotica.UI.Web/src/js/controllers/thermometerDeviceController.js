'use strict';

app.controller('thermometerDeviceController', ['$scope', '$timeout', '$log', 'EventDispatcher', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $timeout, $log, EventDispatcher, temperatureScaleService, dsFamilyTempSensorService) {    

    $scope.resolutions = dsFamilyTempSensorService.resolutions;
    $scope.scales = temperatureScaleService.scales;

    EventDispatcher.on('dsFamilyTempSensorService_onReadReceived', onReadReceived);         

    function onReadReceived(payload) {        
        for (var i = 0; i < payload.dsFamilyTempSensors.length; i++) {
            $scope.sensors[i].addLog(payload.dsFamilyTempSensors[i]);
        }
        $scope.$apply();
    }
    
    $scope.range = {
        min: 30,
        max: 60
    };    

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
            forceY: [15, 35],
        }
    };   

    var chartLine = function (key) {
        this.key = key;
        this.values = [];
    }

    var dsFamilyTempSensor = function (dsFamilyTempSensorId, deviceAddress, family, resolution, scale) {

        var _this = this;

        this.dsFamilyTempSensorId = dsFamilyTempSensorId;
        this.deviceAddress = deviceAddress;
        this.family = family;

        this.isConnected = false;
        this.resolution = resolution;
        this.scale = scale;
        this.temperature = null;
        this.epochTime = null;

        this.highAlarm = null;
        this.lowAlarm = null;

        this.selectedResolution = {};
        this.selectedScale = {};
        
        this.setResolution = function () {
            dsFamilyTempSensorService.setResolution(_this.dsFamilyTempSensorId, _this.selectedResolution.id).then(function (results) {
                //alert('success');
            }, function (error) {
                if (error.status !== 401) {
                    alert(error.data.message);
                }
            });
        }

        this.setScale = function () {
            
        }

        this.forceY = {
            min: 15,//gambeta
            max: 35,//gambeta
        };

        this.changeForceYMin = function () {
            alert();
        }

        this.changeForceYMax = function () {
            alert();
        }

        this.alarm = {
            lowAlarm: 22,//gambeta
            highAlarm: 26,//gambeta
        };        

        this.changeHighAlarm = function () { 
            if (_this.highAlarm !== _this.alarm.highAlarm) {
                dsFamilyTempSensorService.setHighAlarm(_this.dsFamilyTempSensorId, _this.alarm.highAlarm).then(function (results) {
                    //alert('success');
                }, function (error) {
                    if (error.status !== 401) {
                        alert(error.data.message);
                    }
                });
            }
        }

        this.changeLowAlarm = function () {
            if (_this.lowAlarm !== _this.alarm.lowAlarm) {
                dsFamilyTempSensorService.setLowAlarm(_this.dsFamilyTempSensorId, _this.alarm.lowAlarm).then(function (results) {
                    //alert('success');
                }, function (error) {
                    if (error.status !== 401) {
                        alert(error.data.message);
                    }
                });
            }
        }

        this.chart = [];

        this.chart.push(new chartLine("Máximo"));
        this.chart.push(new chartLine("Temperatura"));
        this.chart.push(new chartLine("Mínimo"));        

        this.addLog = function (value) {

            this.isConnected = value.isConnected;
            this.resolution = value.resolution;
            //this.scale = value.scale;
            this.temperature = value.tempCelsius;
            this.chart[1].key = 'Temperatura ' + value.tempCelsius + ' °C';
            this.epochTime = value.epochTimeUtc;

            this.highAlarm = value.highAlarm;
            this.lowAlarm = value.lowAlarm;


            this.chart[0].values.push({
                epochTime: value.epochTimeUtc,
                temperature: value.highAlarm,
            });

            this.chart[1].values.push({
                epochTime: value.epochTimeUtc,
                temperature: value.tempCelsius,
            });

            this.chart[2].values.push({
                epochTime: value.epochTimeUtc,
                temperature: value.lowAlarm,
            });
            

            if (this.chart[0].values.length > 60)
                this.chart[0].values.shift();

            if (this.chart[1].values.length > 60)
                this.chart[1].values.shift();

            if (this.chart[2].values.length > 60)
                this.chart[2].values.shift();
        }
    }

    $scope.sensors = [];

    //gambi
    $scope.sensors.push(new dsFamilyTempSensor('7e415bda-e9c4-e711-9bf4-707781d470bc', '40:255:231:109:162:22:3:211', 'DS18B20', 9, 1));
    $scope.sensors.push(new dsFamilyTempSensor('7f415bda-e9c4-e711-9bf4-707781d470bc', '40:255:254:101:147:22:4:182', 'DS18B20', 11, 1));
    $scope.sensors.push(new dsFamilyTempSensor('80415bda-e9c4-e711-9bf4-707781d470bc', '40:255:192:95:147:22:4:195', 'DS18B20', 11, 1));
    $scope.sensors.push(new dsFamilyTempSensor('81415bda-e9c4-e711-9bf4-707781d470bc', '40:255:113:95:147:22:4:65', 'DS18B20', 11, 1));
}]);
