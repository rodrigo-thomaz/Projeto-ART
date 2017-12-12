app.controller('sensorUnitMeasurementScaleController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'unitMeasurementConverter', 'sensorUnitMeasurementScaleService', 'siContext',
    function ($scope, $rootScope, $timeout, $log, toaster, unitMeasurementConverter, sensorUnitMeasurementScaleService, siContext) {

        $scope.sensorUnitMeasurementScale = null;

        $scope.init = function (sensorUnitMeasurementScale) {

            $scope.sensorUnitMeasurementScale = sensorUnitMeasurementScale;

            // Time Zone
            if (siContext.unitMeasurementScaleLoaded)
                setSelectedSensorUnitMeasurementScale();
            else {
                var unitMeasurementScaleLoadedWatch = siContext.$watch('unitMeasurementScaleLoaded', function (newValue) {
                    if (newValue) {
                        unitMeasurementScaleLoadedWatch();
                        setSelectedSensorUnitMeasurementScale();
                    }
                })
            }

            //clearOnSetValueCompleted = $rootScope.$on('sensorUnitMeasurementScaleService_SetValueCompleted_Id_' + sensor.sensorUnitMeasurementScale.id, onSetValueCompleted);
        };

        $scope.sensorUnitMeasurementScaleView = {
            availableSensorUnitMeasurementScale: siContext.unitMeasurementScale,
            selectedSensorUnitMeasurementScale: {},
        };

        var setSelectedSensorUnitMeasurementScale = function () {
            $scope.sensorUnitMeasurementScaleView.selectedSensorUnitMeasurementScale = $scope.sensorUnitMeasurementScale.unitMeasurementScale();
        };

        //var clearOnSetValueCompleted = null;

        $scope.$on('$destroy', function () {
            //clearOnSetValueCompleted();
        });

        //$scope.changeValue = function (position, value) {
        //    if (!initialized || value === undefined) return;
        //    var valueConverted = unitMeasurementConverter.convertToCelsius($scope.sensor.unitMeasurementId, value);
        //    sensorUnitMeasurementScaleService.setValue($scope.sensor.sensorUnitMeasurementScale.id, valueConverted, position);
        //};

        //$scope.$watch('sensor.sensorUnitMeasurementScale.maxConverted', function (newValue, oldValue) {
        //    $scope.maxView = $scope.sensor.sensorUnitMeasurementScale.maxConverted;
        //});

        //$scope.$watch('sensor.sensorUnitMeasurementScale.minConverted', function (newValue, oldValue) {
        //    $scope.minView = $scope.sensor.sensorUnitMeasurementScale.minConverted
        //});

        //var onSetValueCompleted = function (event, data) {
        //    if (data.position === 'Max') {
        //        toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
        //    }
        //    else if (data.position === 'Min') {
        //        toaster.pop('success', 'Sucesso', 'Limite baixo do gráfico alterado');
        //    }
        //};

    }]);