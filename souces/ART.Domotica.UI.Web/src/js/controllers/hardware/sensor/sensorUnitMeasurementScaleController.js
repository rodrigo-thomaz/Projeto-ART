app.controller('sensorUnitMeasurementScaleController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'debounce', 'unitMeasurementConverter', 'sensorUnitMeasurementScaleService', 'sensorDatasheetContext', 'localeContext', 'sensorDatasheetUnitMeasurementScaleFinder', 'unitMeasurementFinder', 'unitMeasurementScaleFinder', 'countryFinder', 'numericalScaleTypeFinder', 'sensorUnitMeasurementScaleConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, debounce, unitMeasurementConverter, sensorUnitMeasurementScaleService, sensorDatasheetContext, localeContext, sensorDatasheetUnitMeasurementScaleFinder, unitMeasurementFinder, unitMeasurementScaleFinder, countryFinder, numericalScaleTypeFinder, sensorUnitMeasurementScaleConstant) {

        $scope.sensorUnitMeasurementScale = null;

        $scope.$watch('sensorUnitMeasurementScale', function (newValue) {
            if (newValue) {                

                applySelectsWithContext();

                initializeSelectedWatches();

                $scope.rangeMaxView = newValue.rangeMax;
                $scope.rangeMinView = newValue.rangeMin;

                $scope.chartLimiterMaxView = newValue.chartLimiterMax;
                $scope.chartLimiterMinView = newValue.chartLimiterMin;

                initializeRangeMaxViewWatch();
                initializeRangeMinViewWatch();
                initializeChartLimiterMaxViewWatch();
                initializeChartLimiterMinViewWatch();

                clearOnSetUnitMeasurementNumericalScaleTypeCountryCompleted = $rootScope.$on(sensorUnitMeasurementScaleConstant.setUnitMeasurementNumericalScaleTypeCountryCompletedEventName + newValue.sensorUnitMeasurementScaleId, onSetUnitMeasurementNumericalScaleTypeCountryCompleted);
                clearOnSetRangeCompleted = $rootScope.$on(sensorUnitMeasurementScaleConstant.setRangeCompletedEventName + newValue.sensorUnitMeasurementScaleId, onSetRangeCompleted);
                clearOnSetChartLimiterCompleted = $rootScope.$on(sensorUnitMeasurementScaleConstant.setChartLimiterCompletedEventName + newValue.sensorUnitMeasurementScaleId, onSetChartLimiterCompleted);
            }
        });        

        $scope.init = function (sensorUnitMeasurementScale) {
            $scope.sensorUnitMeasurementScale = sensorUnitMeasurementScale;
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

        $scope.rangeMaxView = null;
        $scope.rangeMinView = null;

        $scope.chartLimiterMaxView = null;
        $scope.chartLimiterMinView = null;

        var unitMeasurementViewSelectedWatch = null;
        var countryViewSelectedWatch = null;
        var numericalScaleTypeViewSelectedWatch = null;
        var unitMeasurementScaleViewSelectedWatch = null;

        var rangeMaxViewWatch = null;
        var rangeMinViewWatch = null;

        var chartLimiterMaxViewWatch = null;
        var chartLimiterMinViewWatch = null;

        var initializeSelectedWatches = function () {            

            unitMeasurementViewSelectedWatch = $scope.$watch('unitMeasurementView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                applyUnitMeasurementScaleView();
                changeUnitMeasurementScale();
            });

            countryViewSelectedWatch = $scope.$watch('countryView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                applyNumericalScaleTypeView();
                changeUnitMeasurementScale();
            });

            numericalScaleTypeViewSelectedWatch = $scope.$watch('numericalScaleTypeView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                applyUnitMeasurementScaleView();
                changeUnitMeasurementScale();
            });

            unitMeasurementScaleViewSelectedWatch = $scope.$watch('unitMeasurementScaleView.selected', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                changeUnitMeasurementScale();
            });

        };

        var finalizeSelectedWatches = function () {
            if (unitMeasurementViewSelectedWatch) unitMeasurementViewSelectedWatch();
            if (countryViewSelectedWatch) countryViewSelectedWatch();
            if (numericalScaleTypeViewSelectedWatch) numericalScaleTypeViewSelectedWatch();
            if (unitMeasurementScaleViewSelectedWatch) unitMeasurementScaleViewSelectedWatch();
        };        

        var initializeRangeMaxViewWatch = function () {
            rangeMaxViewWatch = $scope.$watch('rangeMaxView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorUnitMeasurementScaleService.setRange(
                    $scope.sensorUnitMeasurementScale.sensorUnitMeasurementScaleId,
                    $scope.sensorUnitMeasurementScale.sensorDatasheetId,
                    $scope.sensorUnitMeasurementScale.sensorTypeId,
                    'Max',
                    newValue
                );
            });
        };        

        var initializeRangeMinViewWatch = function () {
            rangeMinViewWatch = $scope.$watch('rangeMinView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorUnitMeasurementScaleService.setRange(
                    $scope.sensorUnitMeasurementScale.sensorUnitMeasurementScaleId,
                    $scope.sensorUnitMeasurementScale.sensorDatasheetId,
                    $scope.sensorUnitMeasurementScale.sensorTypeId,
                    'Min',
                    newValue
                );
            });
        };

        var initializeChartLimiterMaxViewWatch = function () {
            chartLimiterMaxViewWatch = $scope.$watch('chartLimiterMaxView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorUnitMeasurementScaleService.setChartLimiter(
                    $scope.sensorUnitMeasurementScale.sensorUnitMeasurementScaleId,
                    $scope.sensorUnitMeasurementScale.sensorDatasheetId,
                    $scope.sensorUnitMeasurementScale.sensorTypeId,
                    'Max',
                    newValue
                );
            });
        };

        var initializeChartLimiterMinViewWatch = function () {
            chartLimiterMinViewWatch = $scope.$watch('chartLimiterMinView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                sensorUnitMeasurementScaleService.setChartLimiter(
                    $scope.sensorUnitMeasurementScale.sensorUnitMeasurementScaleId,
                    $scope.sensorUnitMeasurementScale.sensorDatasheetId,
                    $scope.sensorUnitMeasurementScale.sensorTypeId,
                    'Min',
                    newValue
                );
            });
        };

        var applySelectsWithContext = function () {

            var sensorUnitMeasurementScale = $scope.sensorUnitMeasurementScale;

            //unitMeasurement
            $scope.unitMeasurementView.availables = sensorDatasheetUnitMeasurementScaleFinder.getUnitMeasurementsBySensorDatasheetKey(sensorUnitMeasurementScale.sensorDatasheetId, sensorUnitMeasurementScale.sensorTypeId);
            $scope.unitMeasurementView.selected = unitMeasurementFinder.getByKey(sensorUnitMeasurementScale.unitMeasurementId, sensorUnitMeasurementScale.unitMeasurementTypeId);

            //country
            var country = countryFinder.getByKey(sensorUnitMeasurementScale.countryId);
            $scope.countryView.availables = localeContext.country;
            $scope.countryView.selected = country;

            //numericalScaleType
            $scope.numericalScaleTypeView.availables = sensorDatasheetUnitMeasurementScaleFinder.getNumericalScaleTypesBySensorDatasheetCountryKey(sensorUnitMeasurementScale.sensorDatasheetId, sensorUnitMeasurementScale.sensorTypeId, sensorUnitMeasurementScale.countryId);
            $scope.numericalScaleTypeView.selected = numericalScaleTypeFinder.getByKey(sensorUnitMeasurementScale.numericalScaleTypeId);

            //unitMeasurementScale
            var unitMeasurementScale = unitMeasurementScaleFinder.getByKey(sensorUnitMeasurementScale.unitMeasurementId, sensorUnitMeasurementScale.unitMeasurementTypeId, sensorUnitMeasurementScale.numericalScalePrefixId, sensorUnitMeasurementScale.numericalScaleTypeId);
            $scope.unitMeasurementScaleView.availables = unitMeasurementScaleFinder.getUnitMeasurementScalePrefixes(sensorUnitMeasurementScale.unitMeasurementId, sensorUnitMeasurementScale.unitMeasurementTypeId, sensorUnitMeasurementScale.numericalScaleTypeId);
            $scope.unitMeasurementScaleView.selected = unitMeasurementScale;
        }

        var applyNumericalScaleTypeView = function () {
                        
            var selectedNumericalScaleType = null;

            if ($scope.countryView.selected) {
                var country = $scope.countryView.selected;

                $scope.numericalScaleTypeView.availables = sensorDatasheetUnitMeasurementScaleFinder.getNumericalScaleTypesBySensorDatasheetCountryKey($scope.sensorUnitMeasurementScale.sensorDatasheetId, $scope.sensorUnitMeasurementScale.sensorTypeId, country.countryId);
                                                
                if ($scope.numericalScaleTypeView.availables.length == 1) {
                    selectedNumericalScaleType = $scope.numericalScaleTypeView.availables[0];
                }
            }

            $scope.numericalScaleTypeView.selected = selectedNumericalScaleType;

            var selectNumericalScaleType = $scope.form.selectNumericalScaleType;

            selectNumericalScaleType.$setViewValue(selectedNumericalScaleType);
            selectNumericalScaleType.$commitViewValue();
            selectNumericalScaleType.$render();

        };

        var applyUnitMeasurementScaleView = function () {

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
                $scope.unitMeasurementScaleView.availables = [];
            }

            $scope.unitMeasurementScaleView.selected = selectedUnitMeasurementScale;

            var selectUnitMeasurementScale = $scope.form.selectUnitMeasurementScale;

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

                sensorUnitMeasurementScaleService.setUnitMeasurementNumericalScaleTypeCountry(
                    $scope.sensorUnitMeasurementScale.sensorUnitMeasurementScaleId,
                    $scope.sensorUnitMeasurementScale.sensorDatasheetId,
                    $scope.sensorUnitMeasurementScale.sensorTypeId,
                    unitMeasurementScale.unitMeasurementId,
                    unitMeasurementScale.unitMeasurementTypeId,
                    unitMeasurementScale.numericalScalePrefixId,
                    unitMeasurementScale.numericalScaleTypeId,
                    country.countryId
                );
            }

        });

        var clearOnSetUnitMeasurementNumericalScaleTypeCountryCompleted = null;
        var clearOnSetRangeCompleted = null;
        var clearOnSetChartLimiterCompleted = null;

        $scope.$on('$destroy', function () {
            finalizeSelectedWatches();
            clearOnSetUnitMeasurementNumericalScaleTypeCountryCompleted();
            clearOnSetRangeCompleted();
            clearOnSetChartLimiterCompleted();
        });

        var onSetUnitMeasurementNumericalScaleTypeCountryCompleted = function (event, data) {
            finalizeSelectedWatches();
            applySelectsWithContext();
            initializeSelectedWatches();
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'UnitMeasurementNumericalScaleTypeCountry alterado');
        };

        var onSetRangeCompleted = function (event, data) {
            if (data.position === 'Max') {
                rangeMaxViewWatch();
                $scope.rangeMaxView = data.value;
                $scope.$apply();
                initializeRangeMaxViewWatch();
                toaster.pop('success', 'Sucesso', 'Range alto alterado');
            }
            else if (data.position === 'Min') {
                rangeMinViewWatch();
                $scope.rangeMinView = data.value;
                $scope.$apply();
                initializeRangeMinViewWatch();
                toaster.pop('success', 'Sucesso', 'Range baixo alterado');
            }
        };

        var onSetChartLimiterCompleted = function (event, data) {
            if (data.position === 'Max') {
                chartLimiterMaxViewWatch();
                $scope.chartLimiterMaxView = data.value;
                $scope.$apply();
                initializeChartLimiterMaxViewWatch();
                toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
            }
            else if (data.position === 'Min') {
                chartLimiterMinViewWatch();
                $scope.chartLimiterMinView = data.value;
                $scope.$apply();
                initializeChartLimiterMinViewWatch();
                toaster.pop('success', 'Sucesso', 'Limite baixo do gráfico alterado');
            }
        };

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

        

    }]);