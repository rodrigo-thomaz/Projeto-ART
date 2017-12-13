app.controller('sensorUnitMeasurementScaleController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'debounce', 'unitMeasurementConverter', 'sensorUnitMeasurementScaleService', 'sensorDatasheetContext', 'localeContext', 'sensorDatasheetUnitMeasurementScaleFinder', 'unitMeasurementFinder', 'unitMeasurementScaleFinder', 'countryFinder', 'numericalScaleTypeCountryFinder',
    function ($scope, $rootScope, $timeout, $log, toaster, debounce, unitMeasurementConverter, sensorUnitMeasurementScaleService, sensorDatasheetContext, localeContext, sensorDatasheetUnitMeasurementScaleFinder, unitMeasurementFinder, unitMeasurementScaleFinder, countryFinder, numericalScaleTypeCountryFinder) {

        $scope.sensorUnitMeasurementScale = null;

        $scope.$watch('sensorUnitMeasurementScale', function (newValue) {
            if (newValue) {
                //unitMeasurement
                var unitMeasurement = unitMeasurementFinder.getByKey(newValue.unitMeasurementId, newValue.unitMeasurementTypeId);
                $scope.unitMeasurementView.availables = sensorDatasheetUnitMeasurementScaleFinder.getUnitMeasurementsBySensorDatasheetKey(newValue.sensorDatasheetId, newValue.sensorTypeId);
                $scope.unitMeasurementView.selected = unitMeasurement;
                //country
                var country = countryFinder.getByKey(newValue.countryId);
                $scope.countryView.availables = localeContext.country;
                $scope.countryView.selected = country;
                //numericalScaleType
                var numericalScaleType = numericalScaleTypeCountryFinder.getByKey(newValue.numericalScaleTypeId, newValue.countryId);
                $scope.numericalScaleTypeView.availables = country.numericalScaleTypeCountries();
                $scope.numericalScaleTypeView.selected = numericalScaleType;

                //watches
                initializeWatches();
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
            availables: [],
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

        var initializeWatches = function () {

            $scope.$watch('unitMeasurementView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                applyUnitMeasurementScaleView();
                changeUnitMeasurementScale();
            });

            $scope.$watch('countryView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                applyNumericalScaleTypeView();
                changeUnitMeasurementScale();
            });

            $scope.$watch('numericalScaleTypeView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                applyUnitMeasurementScaleView();
                changeUnitMeasurementScale();
            });

            $scope.$watch('unitMeasurementScaleView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                changeUnitMeasurementScale();
            });

        };

        var applyNumericalScaleTypeView = function () {

            var selectNumericalScaleType = $scope.form.selectNumericalScaleType;
            var selectedNumericalScaleType = null;

            if ($scope.countryView.selected) {
                var country = $scope.countryView.selected;
                $scope.numericalScaleTypeView.availables = country.numericalScaleTypeCountries();
                if ($scope.numericalScaleTypeView.availables.length == 1) {
                    selectedNumericalScaleType = $scope.numericalScaleTypeView.availables[0];
                }
            }

            $scope.numericalScaleTypeView.selected = selectedNumericalScaleType;

            selectNumericalScaleType.$setViewValue(selectedNumericalScaleType);
            selectNumericalScaleType.$commitViewValue();
            selectNumericalScaleType.$render();

        };

        var applyUnitMeasurementScaleView = function () {

            var selectUnitMeasurementScale = $scope.form.selectUnitMeasurementScale;
            var selectedUnitMeasurementScale = null;

            if ($scope.numericalScaleTypeView.selected && $scope.unitMeasurementView.selected) {
                var numericalScaleType = $scope.numericalScaleTypeView.selected;
                var unitMeasurement = $scope.unitMeasurementView.selected;
                var unitMeasurementScales = unitMeasurementScaleFinder.getUnitMeasurementScalePrefixes(unitMeasurement.unitMeasurementId, unitMeasurement.unitMeasurementTypeId, numericalScaleType.numericalScaleTypeId);
                $scope.unitMeasurementScaleView.availables = unitMeasurementScales;
                if ($scope.unitMeasurementScaleView.availables.length == 1) {
                    selectedUnitMeasurementScale = $scope.unitMeasurementScaleView.availables[0];
                }
            }
            else {
                $scope.unitMeasurementScaleView.availables = null;
            }

            $scope.unitMeasurementScaleView.selected = selectedUnitMeasurementScale;

            selectUnitMeasurementScale.$setViewValue(selectedUnitMeasurementScale);
            selectUnitMeasurementScale.$commitViewValue();
            selectUnitMeasurementScale.$render();

        };

        var changeUnitMeasurementScale = debounce(500, function () {

            var unitMeasurement = $scope.unitMeasurementView.selected;
            var country = $scope.countryView.selected;
            var numericalScaleType = $scope.numericalScaleTypeView.selected;
            var unitMeasurementScale = $scope.unitMeasurementScaleView.selected;

            if (unitMeasurement && country && numericalScaleType && unitMeasurementScale) {
                alert('change!');
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