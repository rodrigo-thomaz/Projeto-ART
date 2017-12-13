app.controller('sensorUnitMeasurementScaleController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'unitMeasurementConverter', 'sensorUnitMeasurementScaleService', 'sensorDatasheetContext', 'localeContext', 'sensorDatasheetUnitMeasurementScaleFinder', 'unitMeasurementScaleFinder',
    function ($scope, $rootScope, $timeout, $log, toaster, unitMeasurementConverter, sensorUnitMeasurementScaleService, sensorDatasheetContext, localeContext, sensorDatasheetUnitMeasurementScaleFinder, unitMeasurementScaleFinder) {

        $scope.sensorUnitMeasurementScale = null;

        $scope.$watch('sensorUnitMeasurementScale', function (newValue) {
            if (newValue) {
                $scope.unitMeasurementView.availables = sensorDatasheetUnitMeasurementScaleFinder.getUnitMeasurementsBySensorDatasheetKey(newValue.sensorDatasheetId, newValue.sensorTypeId);
            }
        });

        $scope.init = function (sensorUnitMeasurementScale) {

            $scope.sensorUnitMeasurementScale = sensorUnitMeasurementScale;

            // sensorDatasheetUnitMeasurementScale
            if (sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded)
                setSelectedSensorDatasheetUnitMeasurementScale();
            else {
                var sensorDatasheetUnitMeasurementScaleLoadedWatch = sensorDatasheetContext.$watch('sensorDatasheetUnitMeasurementScaleLoaded', function (newValue) {
                    if (newValue) {
                        sensorDatasheetUnitMeasurementScaleLoadedWatch();
                        setSelectedSensorDatasheetUnitMeasurementScale();
                    }
                })
            }

            //clearOnSetValueCompleted = $rootScope.$on('sensorUnitMeasurementScaleService_SetValueCompleted_Id_' + sensor.sensorUnitMeasurementScale.id, onSetValueCompleted);
        };               

        $scope.unitMeasurementView = {
            availables: [],
            selected: null,
        };    

        $scope.countryView = {
            availables: localeContext.country,
            selected: null,
        };

        $scope.numericalScaleTypeView = {
            availables: [],
            selected: null,
        };

        $scope.unitMeasurementScaleView = {
            availables: [],
            selected: null,
        };                 

        $scope.$watch('countryView.selected', function (newValue) {
            if (newValue) {
                $scope.numericalScaleTypeView.availables = newValue.numericalScaleTypeCountries();
            }
        });           

        $scope.$watch('numericalScaleTypeView.selected', function (newValue) {
            if (newValue) {
                var unitMeasurement = $scope.unitMeasurementView.selected;
                var unitMeasurementScales = unitMeasurementScaleFinder.getUnitMeasurementScalePrefixes(unitMeasurement.unitMeasurementId, unitMeasurement.unitMeasurementTypeId, newValue.numericalScaleTypeId);
                $scope.unitMeasurementScaleView.availables = unitMeasurementScales;
            }
        });    

        var setSelectedSensorDatasheetUnitMeasurementScale = function () {
            //$scope.sensorDatasheetUnitMeasurementScaleView.selected = $scope.sensorUnitMeasurementScale.sensorDatasheetUnitMeasurementScale();
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