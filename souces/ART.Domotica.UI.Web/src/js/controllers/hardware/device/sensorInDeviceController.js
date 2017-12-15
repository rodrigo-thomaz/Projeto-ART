'use strict';
app.controller('sensorInDeviceController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorInDeviceService',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorInDeviceService) {

        $scope.sensorInDevice = [];

        $scope.init = function (sensorInDevice) {

            $scope.sensorInDevice = sensorInDevice;

        }

        $scope.$watchCollection('sensorInDevice', function (newValues, oldValues) {

        });

        $scope.$on('$destroy', function () {

        });

        $scope.dragControlListeners = {
            accept: function (sourceItemHandleScope, destSortableScope) { //override to determine drag is allowed or not. default is true.
                return true;
            },
            itemMoved: function (event) {
                //Do what you want
            },
            orderChanged: function (event) {
                //Do what you want
            },
            //containment: '#board', //optional param.
            clone: false,//optional param for clone feature.
            allowDuplicates: false, //optional param allows duplicates to be dropped.
        };


    }]);