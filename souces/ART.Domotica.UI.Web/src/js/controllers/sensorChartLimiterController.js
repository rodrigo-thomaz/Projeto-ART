app.controller('sensorChartLimiterController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'unitOfMeasurementConverter', 'sensorChartLimiterService', function ($scope, $rootScope, $timeout, $log, toaster, unitOfMeasurementConverter, sensorChartLimiterService) {

    $scope.sensor = {};    

    var initialized = false;

    $scope.init = function (sensor) {

        $scope.sensor = sensor;

        $scope.maxView = unitOfMeasurementConverter.convertFromCelsius(sensor.unitOfMeasurementId, sensor.sensorChartLimiter.max);
        $scope.minView = unitOfMeasurementConverter.convertFromCelsius(sensor.unitOfMeasurementId, sensor.sensorChartLimiter.min);
        
        clearOnSetChartLimiterCelsiusCompleted = $rootScope.$on('sensorChartLimiterService_SetValueCompleted_Id_' + $scope.sensor.dsFamilyTempSensorId, onSetChartLimiterCelsiusCompleted);

        initialized = true;
    };

    var clearOnSetChartLimiterCelsiusCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetChartLimiterCelsiusCompleted();
    });

    $scope.changeChartLimiterValue = function (position, chartLimiterValue) {
        if (!initialized || chartLimiterValue === undefined) return;
        var value = unitOfMeasurementConverter.convertToCelsius($scope.sensor.unitOfMeasurementId, chartLimiterValue);
        sensorChartLimiterService.setValue($scope.sensor.dsFamilyTempSensorId, value, position);
    };

    var onSetChartLimiterCelsiusCompleted = function (event, data) {
        if (data.position === 'Max') {
            $scope.maxView = data.value;
            toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
        }
        else if (data.position === 'Min') {
            $scope.minView = data.value;
            toaster.pop('success', 'Sucesso', 'Limite baixo do gráfico alterado');
        }
    };

}]);