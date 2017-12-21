'use strict';
app.factory('deviceDatasheetMapper', [
    '$rootScope',
    'deviceDatasheetContext',
    'deviceDatasheetFinder',
    'deviceDatasheetConstant',
    'deviceFinder',
    function (
        $rootScope,
        deviceDatasheetContext,
        deviceDatasheetFinder,
        deviceDatasheetConstant,
        deviceFinder) {

        var serviceFactory = {};        

        deviceDatasheetContext.$watchCollection('deviceDatasheet', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceDatasheet = newValues[i];
                deviceDatasheet.devices = function () { return deviceFinder.getByDeviceDatasheetKey(this.deviceDatasheetId); }
            }
        });       

        // *** Events Subscriptions        

        var onDeviceDatasheetGetAllCompleted = function (event, data) {
            deviceDatasheetGetAllCompletedSubscription();
            deviceDatasheetContext.deviceDatasheetLoaded = true;
        }       

        var deviceDatasheetGetAllCompletedSubscription = $rootScope.$on(deviceDatasheetConstant.getAllCompletedEventName, onDeviceDatasheetGetAllCompleted);

        $rootScope.$on('$destroy', function () {
            deviceDatasheetGetAllCompletedSubscription();
        });

        // *** Events Subscriptions


        return serviceFactory;

    }]);