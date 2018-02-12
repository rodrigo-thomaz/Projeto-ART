'use strict';
app.controller('deviceDebugController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceDebugService', 'deviceDebugConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceDebugService, deviceDebugConstant) {

        $scope.deviceDebug = null;

        $scope.remoteEnabledView = null;
        $scope.resetCmdEnabledView = null;
        $scope.serialEnabledView = null;
        $scope.showColorsView = null;
        $scope.showDebugLevelView = null;
        $scope.showProfilerView = null;
        $scope.showTimeView = null;

        $scope.init = function (deviceDebug) {

            $scope.deviceDebug = deviceDebug;

            $scope.remoteEnabledView = deviceDebug.remoteEnabled;
            $scope.resetCmdEnabledView = deviceDebug.resetCmdEnabled;
            $scope.serialEnabledView = deviceDebug.serialEnabled;
            $scope.showColorsView = deviceDebug.showColors;
            $scope.showDebugLevelView = deviceDebug.showDebugLevel;
            $scope.showProfilerView = deviceDebug.showProfiler;
            $scope.showTimeView = deviceDebug.showTime;

            initializeRemoteEnabledViewWatch();
            initializeResetCmdEnabledViewWatch();
            initializeSerialEnabledViewWatch();
            initializeShowColorsViewWatch();
            initializeShowDebugLevelViewWatch();
            initializeShowProfilerViewWatch();
            initializeShowTimeViewWatch();

            clearOnSetRemoteEnabledCompleted = $rootScope.$on(deviceDebugConstant.setRemoteEnabledCompletedEventName + $scope.deviceDebug.deviceId, onSetRemoteEnabledCompleted);
            clearOnSetResetCmdEnabledCompleted = $rootScope.$on(deviceDebugConstant.setResetCmdEnabledCompletedEventName + $scope.deviceDebug.deviceId, onSetResetCmdEnabledCompleted);
            clearOnSetSerialEnabledCompleted = $rootScope.$on(deviceDebugConstant.setSerialEnabledCompletedEventName + $scope.deviceDebug.deviceId, onSetSerialEnabledCompleted);
            clearOnSetShowColorsCompleted = $rootScope.$on(deviceDebugConstant.setShowColorsCompletedEventName + $scope.deviceDebug.deviceId, onSetShowColorsCompleted);
            clearOnSetShowDebugLevelCompleted = $rootScope.$on(deviceDebugConstant.setShowDebugLevelCompletedEventName + $scope.deviceDebug.deviceId, onSetShowDebugLevelCompleted);
            clearOnSetShowProfilerCompleted = $rootScope.$on(deviceDebugConstant.setShowProfilerCompletedEventName + $scope.deviceDebug.deviceId, onSetShowProfilerCompleted);
            clearOnSetShowTimeCompleted = $rootScope.$on(deviceDebugConstant.setShowTimeCompletedEventName + $scope.deviceDebug.deviceId, onSetShowTimeCompleted);
        }

        var remoteEnabledViewWatch = null;
        var resetCmdEnabledViewWatch = null;
        var serialEnabledViewWatch = null;
        var showColorsViewWatch = null;
        var showDebugLevelViewWatch = null;
        var showProfilerViewWatch = null;
        var showTimeViewWatch = null;

        var initializeRemoteEnabledViewWatch = function () {
            remoteEnabledViewWatch = $scope.$watch('remoteEnabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setRemoteEnabled(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var initializeResetCmdEnabledViewWatch = function () {
            resetCmdEnabledViewWatch = $scope.$watch('resetCmdEnabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setResetCmdEnabled(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var initializeSerialEnabledViewWatch = function () {
            serialEnabledViewWatch = $scope.$watch('serialEnabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setSerialEnabled(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var initializeShowColorsViewWatch = function () {
            showColorsViewWatch = $scope.$watch('showColorsView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowColors(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var initializeShowDebugLevelViewWatch = function () {
            showDebugLevelViewWatch = $scope.$watch('showDebugLevelView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowDebugLevel(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var initializeShowProfilerViewWatch = function () {
            showProfilerViewWatch = $scope.$watch('showProfilerView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowProfiler(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var initializeShowTimeViewWatch = function () {
            showTimeViewWatch = $scope.$watch('showTimeView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowTime(
                    $scope.deviceDebug.deviceTypeId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    $scope.deviceDebug.deviceId, 
                    newValue);
            });
        };

        var clearOnSetRemoteEnabledCompleted = null;
        var clearOnSetResetCmdEnabledCompleted = null;
        var clearOnSetSerialEnabledCompleted = null;
        var clearOnSetShowColorsCompleted = null;
        var clearOnSetShowDebugLevelCompleted = null;
        var clearOnSetShowProfilerCompleted = null;
        var clearOnSetShowTimeCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetRemoteEnabledCompleted();
            clearOnSetResetCmdEnabledCompleted();
            clearOnSetSerialEnabledCompleted();
            clearOnSetShowColorsCompleted();
            clearOnSetShowDebugLevelCompleted();
            clearOnSetShowProfilerCompleted();
            clearOnSetShowTimeCompleted();

            remoteEnabledViewWatch();
            resetCmdEnabledViewWatch();
            serialEnabledViewWatch();
            showColorsViewWatch();
            showDebugLevelViewWatch();
            showProfilerViewWatch();
            showTimeViewWatch();
        });

        var onSetRemoteEnabledCompleted = function (event, data) {
            $scope.remoteEnabledView = $scope.deviceDebug.remoteEnabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug RemoteEnabled alterado');
        };

        var onSetResetCmdEnabledCompleted = function (event, data) {
            $scope.resetCmdEnabledView = $scope.deviceDebug.resetCmdEnabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ResetCmdEnabled alterado');
        };

        var onSetSerialEnabledCompleted = function (event, data) {
            $scope.serialEnabledView = $scope.deviceDebug.serialEnabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug SerialEnabled alterado');
        };

        var onSetShowColorsCompleted = function (event, data) {
            $scope.showColorsView = $scope.deviceDebug.showColors;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowColors alterado');
        };

        var onSetShowDebugLevelCompleted = function (event, data) {
            $scope.showDebugLevelView = $scope.deviceDebug.showDebugLevel;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowDebugLevel alterado');
        };
        
        var onSetShowProfilerCompleted = function (event, data) {
            $scope.showProfilerView = $scope.deviceDebug.showProfiler;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowProfiler alterado');
        };

        var onSetShowTimeCompleted = function (event, data) {
            $scope.showTimeView = $scope.deviceDebug.showTime;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowTime alterado');
        };

    }]);