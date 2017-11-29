app.controller('sensorChartLimiterController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'unitOfMeasurementConverter', 'sensorChartLimiterService', function ($scope, $rootScope, $timeout, $log, toaster, unitOfMeasurementConverter, sensorChartLimiterService) {

    $scope.sensor = {};    

    var initialized = false;

    $scope.init = function (sensor) {

        $scope.sensor = sensor;
        
        clearOnSetValueCompleted = $rootScope.$on('sensorChartLimiterService_SetValueCompleted_Id_' + sensor.sensorChartLimiter.id, onSetValueCompleted);

        initialized = true;
    };

    var clearOnSetValueCompleted = null;

    $scope.$on('$destroy', function () {
        clearOnSetValueCompleted();
    });

    $scope.changeValue = function (position, value) {
        if (!initialized || value === undefined) return;
        var valueConverted = unitOfMeasurementConverter.convertToCelsius($scope.sensor.unitOfMeasurementId, value);
        sensorChartLimiterService.setValue($scope.sensor.sensorChartLimiter.id, valueConverted, position);
    };

    $scope.$watch('sensor.sensorChartLimiter.maxConverted', function (newValue, oldValue) {
        $scope.maxView = $scope.sensor.sensorChartLimiter.maxConverted;
    });

    $scope.$watch('sensor.sensorChartLimiter.minConverted', function (newValue, oldValue) {
        $scope.minView = $scope.sensor.sensorChartLimiter.minConverted
    });

    var onSetValueCompleted = function (event, data) {
        if (data.position === 'Max') {
            toaster.pop('success', 'Sucesso', 'Limite alto do gráfico alterado');
        }
        else if (data.position === 'Min') {
            toaster.pop('success', 'Sucesso', 'Limite baixo do gráfico alterado');
        }
    };

}]);