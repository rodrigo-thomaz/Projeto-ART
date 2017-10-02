'use strict';

app.controller('termometroController', ['$scope', '$timeout', 'termometroStompService', 'dsFamilyTempSensorResolutionService', 'dsFamilyTempSensorService', '$log', function ($scope, $timeout, termometroStompService, dsFamilyTempSensorResolutionService, dsFamilyTempSensorService, $log) {    

    termometroStompService.onReadReceived = onReadReceived;

    function onReadReceived(payload) {
        for (var i = 0; i < payload.length; i++) {
            $scope.sensors[i].addLog(payload[i]);
        }
        $scope.$apply();
    }
    
    $scope.range = {
        min: 30,
        max: 60
    };

    $scope.resolutions = dsFamilyTempSensorResolutionService.get();

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
                if (d == null) return null;                
                    return d.epochTime;
            },
            y: function (d) {
                if (d == null) return null;    
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
        
        this.setResolution = function () {
            dsFamilyTempSensorService.setResolution(_this.dsFamilyTempSensorId, _this.selectedResolution.dsFamilyTempSensorResolutionId).then(function (results) {
                //alert('success');
            }, function (error) {
                if (error.status !== 401) {
                    alert(error.data.message);
                }
            });
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
            if (_this.highAlarm != _this.alarm.highAlarm) {
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
            if (_this.lowAlarm != _this.alarm.lowAlarm) {
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
            this.epochTime = value.epochTime;

            this.highAlarm = value.highAlarm;
            this.lowAlarm = value.lowAlarm;


            this.chart[0].values.push({
                epochTime: value.epochTime,
                temperature: value.highAlarm,
            });

            this.chart[1].values.push({
                epochTime: value.epochTime,
                temperature: value.tempCelsius,
            });

            this.chart[2].values.push({
                epochTime: value.epochTime,
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
    $scope.sensors.push(new dsFamilyTempSensor('4fe0c742-b8a4-e711-9bee-707781d470bc', '28fffe6593164b6', 'DS18B20', 9, 1));
    $scope.sensors.push(new dsFamilyTempSensor('4ee0c742-b8a4-e711-9bee-707781d470bc', '28ffe76da2163d3', 'DS18B20', 11, 1));
}]);
