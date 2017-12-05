'use strict';
app.controller('espDeviceManagerController', ['$scope', '$timeout', '$log', '$modal', 'uiGridConstants', '$rootScope', 'toaster', 'contextScope', 'espDeviceService', function ($scope, $timeout, $log, $modal, uiGridConstants, $rootScope, toaster, contextScope, espDeviceService) {    
        
    var onDeleteFromApplicationClick = function (espDevice) {
        espDeviceService.deleteFromApplication(espDevice.hardwareInApplicationId);
    }

    var onDeleteFromApplicationCompleted = function () {
        toaster.pop('success', 'Sucesso', 'Device deletado');
    }

    $scope.$on('$destroy', function () {
        clearOnDeleteFromApplicationCompleted();
    });

    var clearOnDeleteFromApplicationCompleted = $rootScope.$on('espDeviceService_onDeleteFromApplicationCompleted', onDeleteFromApplicationCompleted);        
    
    $scope.gridOptions = {                                                 
        enableFiltering: true,
        enableSorting: true,
        showFooter: true,
        rowHeight: 36,
        data: [],
        columnDefs: [
            { name: 'DeviceId', field: 'deviceId', width: 270 },
            { name: 'ChipId', field: 'chipId', width: 100 },
            { name: 'FlashChipId', field: 'flashChipId', width: 120 },
            { name: 'MacAddress', field: 'macAddress', width: 140 },
            { name: 'Última atualização', field: 'epochTimeUtc', width: 160 },
            { name: 'WifiQuality', field: 'wifiQuality', width: 120 },
            { name: 'Data criação', field: 'createDate', width: 150 },
            { name: 'Ações', cellTemplate: '<div class="text-center"><a ng-click="grid.appScope.deleteFromApplicationClick(row.entity)" class="btn btn-danger" href="" aria-label="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a></div>', width: 85 },
        ],
    };

    $scope.deleteFromApplicationClick = onDeleteFromApplicationClick;

    $scope.gridOptions.data = contextScope.devices;

    // Join

    $scope.openDeviceJoin = function () {
        var modalInstance = $modal.open({
            templateUrl: 'espDeviceJoinHtml',
            //controller: 'espDeviceJoinController',
            size: 'lg',
        });

        modalInstance.result.then(function (selectedItem) {
            //$scope.selected = selectedItem;
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
    };

}]);
