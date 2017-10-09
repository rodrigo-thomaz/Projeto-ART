'use strict';
app.controller('hardwareController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'hardwareService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, hardwareService) {    

    var onGetListCompleted = function (data) {
        for (var i = 0; i < data.length; i++) {
            data[i].createDateFormatted = new Date(data[i].createDate * 1000).toLocaleString();
        }
        $scope.gridOptions.data = data;
    }

    $scope.gridOptions = {
        enableFiltering: true,
        enableSorting: true,
        showFooter: true,
        rowHeight: 36,
        data: [],
        columnDefs: [
            { name: 'Id', field: 'id', width: 270 },
            { name: 'Pin', field: 'pin', width: 70 },
            { name: 'Em aplicação', field: 'inApplication', width: 70 },
            { name: 'Data criação', field: 'createDateFormatted', width: 150 }
        ],
    };
    
    hardwareService.getList();

    EventDispatcher.on('hardwareService_onGetListCompleted', onGetListCompleted);

}]);
