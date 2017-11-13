'use strict';
app.controller('espDeviceListController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'espDeviceService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, espDeviceService) {    
   
    $scope.devices = espDeviceService.devices;

}]);
