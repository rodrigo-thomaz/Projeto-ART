'use strict';
app.controller('deviceNTPController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'globalizationContext', 'deviceNTPService',
    function ($scope, $rootScope, $timeout, $log, toaster, globalizationContext, deviceNTPService) {

        $scope.deviceNTP = {};

        var initialized = false;

        $scope.init = function (deviceNTP) {

            $scope.deviceNTP = deviceNTP;

            $scope.updateIntervalInMilliSecondView = deviceNTP.updateIntervalInMilliSecond;

            // Time Zone
            if (globalizationContext.timeZoneLoaded)
                setSelectedTimeZone();
            else
                clearOnTimeZoneServiceInitialized = $rootScope.$on('timeZoneService_Initialized', setSelectedTimeZone);

            clearOnSetTimeZoneCompleted = $rootScope.$on('deviceService_onSetTimeZoneCompleted_Id_' + $scope.deviceNTP.device.deviceId, onSetTimeZoneCompleted);
            clearOnSetUpdateIntervalInMilliSecondCompleted = $rootScope.$on('deviceService_onSetUpdateIntervalInMilliSecondCompleted_Id_' + $scope.deviceNTP.device.deviceId, onSetUpdateIntervalInMilliSecondCompleted);

            initialized = true;
        }

        var clearOnTimeZoneServiceInitialized = null;
        var clearOnSetTimeZoneCompleted = null;
        var clearOnSetUpdateIntervalInMilliSecondCompleted = null;

        $scope.$on('$destroy', function () {
            if (clearOnTimeZoneServiceInitialized !== null) clearOnTimeZoneServiceInitialized();
            clearOnSetTimeZoneCompleted();
            clearOnSetUpdateIntervalInMilliSecondCompleted();
        });

        var setSelectedTimeZone = function () {
            $scope.timeZone.selectedTimeZone = $scope.deviceNTP.timeZone();
        };

        var onSetTimeZoneCompleted = function (event, data) {
            setSelectedTimeZone();
            toaster.pop('success', 'Sucesso', 'Fuso horário alterado');
        };

        var onSetUpdateIntervalInMilliSecondCompleted = function (event, data) {
            $scope.updateIntervalInMilliSecondView = data.updateIntervalInMilliSecond;
            toaster.pop('success', 'Sucesso', 'UpdateIntervalInMilliSecond alterado');
        };

        $scope.timeZone = {
            availableTimeZones: globalizationContext.timeZone,
            selectedTimeZone: {},
        };

        $scope.changeTimeZone = function () {
            if (!initialized) return;
            deviceNTPService.setTimeZone($scope.deviceNTP.device.deviceId, $scope.timeZone.selectedTimeZone.timeZoneId);
        };

        $scope.changeUpdateIntervalInMilliSecond = function () {
            if (!initialized || !$scope.updateIntervalInMilliSecondView) return;
            deviceNTPService.setUpdateIntervalInMilliSecond($scope.deviceNTP.device.deviceId, $scope.updateIntervalInMilliSecondView);
        };

    }]);