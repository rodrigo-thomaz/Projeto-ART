'use strict';
app.factory('siMapper', [
    '$rootScope',
    'siContext',
    'numericalScaleFinder',
    'numericalScalePrefixFinder',
    'numericalScaleTypeFinder',
    'unitMeasurementScaleFinder',
    'unitMeasurementFinder',
    'unitMeasurementTypeFinder',
    'countryFinder',
    'numericalScalePrefixConstant',
    'numericalScaleConstant',
    'numericalScaleTypeCountryConstant',
    'numericalScaleTypeConstant',
    'unitMeasurementScaleConstant',
    'unitMeasurementConstant',
    'unitMeasurementTypeConstant',
    'sensorDatasheetUnitMeasurementDefaultFinder',
    'sensorDatasheetUnitMeasurementScaleFinder',
    function (
        $rootScope,
        siContext,
        numericalScaleFinder,
        numericalScalePrefixFinder,
        numericalScaleTypeFinder,
        unitMeasurementScaleFinder,
        unitMeasurementFinder,
        unitMeasurementTypeFinder,
        countryFinder,
        numericalScalePrefixConstant,
        numericalScaleConstant,
        numericalScaleTypeCountryConstant,
        numericalScaleTypeConstant,
        unitMeasurementScaleConstant,
        unitMeasurementConstant,
        unitMeasurementTypeConstant,
        sensorDatasheetUnitMeasurementDefaultFinder,
        sensorDatasheetUnitMeasurementScaleFinder) {

        var serviceFactory = {};

        siContext.$watchCollection('numericalScaleType', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var numericalScaleType = newValues[i];
                numericalScaleType.countries = function () { return countryFinder.getByNumericalScaleTypeKey(this.numericalScaleTypeId); }
                numericalScaleType.numericalScales = function () { return numericalScaleFinder.getByNumericalScaleTypeKey(this.numericalScaleTypeId); }
            }
        });

        siContext.$watchCollection('numericalScale', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var numericalScale = newValues[i];
                numericalScale.numericalScaleType = function () { return numericalScaleTypeFinder.getByKey(this.numericalScaleTypeId); }
                numericalScale.numericalScalePrefix = function () { return numericalScalePrefixFinder.getByKey(this.numericalScalePrefixId); }
                numericalScale.unitMeasurementScales = function () { return unitMeasurementScaleFinder.getByNumericalScaleKey(this.numericalScalePrefixId, this.numericalScaleTypeId); }
            }
        });

        siContext.$watchCollection('numericalScalePrefix', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var numericalScalePrefix = newValues[i];
                numericalScalePrefix.numericalScales = function () { return numericalScaleFinder.getByNumericalScalePrefixKey(this.numericalScalePrefixId); }
            }
        });

        siContext.$watchCollection('unitMeasurementScale', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var unitMeasurementScale = newValues[i];
                unitMeasurementScale.numericalScale = function () { return numericalScaleFinder.getByKey(this.numericalScalePrefixId, this.numericalScaleTypeId); }
                unitMeasurementScale.unitMeasurement = function () { return unitMeasurementFinder.getByKey(this.unitMeasurementId, this.unitMeasurementTypeId); }
                unitMeasurementScale.sensorDatasheetUnitMeasurementDefaults = function () { return sensorDatasheetUnitMeasurementDefaultFinder.getByUnitMeasurementScaleKey(this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
                unitMeasurementScale.sensorDatasheetUnitMeasurementScales = function () { return sensorDatasheetUnitMeasurementScaleFinder.getByUnitMeasurementScaleKey(this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
                unitMeasurementScale.sensorUnitMeasurementScales = function () { return sensorUnitMeasurementScaleFinder.getByUnitMeasurementScaleKey(this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
            }
        });

        siContext.$watchCollection('unitMeasurement', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var unitMeasurement = newValues[i];
                unitMeasurement.unitMeasurementType = function () { return unitMeasurementTypeFinder.getByKey(this.unitMeasurementTypeId); }
                unitMeasurement.unitMeasurementScales = function () { return unitMeasurementScaleFinder.getByUnitMeasurementKey(this.unitMeasurementId, this.unitMeasurementTypeId); }
            }
        });

        siContext.$watchCollection('unitMeasurementType', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var unitMeasurementType = newValues[i];
                unitMeasurementType.unitMeasurements = function () { return unitMeasurementFinder.getByUnitMeasurementTypeKey(this.unitMeasurementTypeId); }
            }
        });      


        // *** Events Subscriptions    

        var onNumericalScalePrefixGetAllCompleted = function (event, data) {
            numericalScalePrefixGetAllCompletedSubscription();
            siContext.numericalScalePrefixLoaded = true;
        }

        var onNumericalScaleGetAllCompleted = function (event, data) {
            numericalScaleGetAllCompletedSubscription();
            siContext.numericalScaleLoaded = true;
        }

        var onNumericalScaleTypeCountryGetAllCompleted = function (event, data) {
            numericalScaleTypeCountryGetAllCompletedSubscription();
            siContext.numericalScaleTypeCountryLoaded = true;
        }

        var onNumericalScaleTypeGetAllCompleted = function (event, data) {
            numericalScaleTypeGetAllCompletedSubscription();
            siContext.numericalScaleTypeLoaded = true;
        }

        var onUnitMeasurementScaleGetAllCompleted = function (event, data) {
            unitMeasurementScaleGetAllCompletedSubscription();
            siContext.unitMeasurementScaleLoaded = true;
        }

        var onUnitMeasurementGetAllCompleted = function (event, data) {
            unitMeasurementGetAllCompletedSubscription();
            siContext.unitMeasurementLoaded = true;
        }

        var onUnitMeasurementTypeGetAllCompleted = function (event, data) {
            unitMeasurementTypeGetAllCompletedSubscription();
            siContext.unitMeasurementTypeLoaded = true;
        }

        var numericalScalePrefixGetAllCompletedSubscription = $rootScope.$on(numericalScalePrefixConstant.getAllCompletedEventName, onNumericalScalePrefixGetAllCompleted);
        var numericalScaleGetAllCompletedSubscription = $rootScope.$on(numericalScaleConstant.getAllCompletedEventName, onNumericalScaleGetAllCompleted);
        var numericalScaleTypeCountryGetAllCompletedSubscription = $rootScope.$on(numericalScaleTypeCountryConstant.getAllCompletedEventName, onNumericalScaleTypeCountryGetAllCompleted);
        var numericalScaleTypeGetAllCompletedSubscription = $rootScope.$on(numericalScaleTypeConstant.getAllCompletedEventName, onNumericalScaleTypeGetAllCompleted);
        var unitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(unitMeasurementScaleConstant.getAllCompletedEventName, onUnitMeasurementScaleGetAllCompleted);
        var unitMeasurementGetAllCompletedSubscription = $rootScope.$on(unitMeasurementConstant.getAllCompletedEventName, onUnitMeasurementGetAllCompleted);
        var unitMeasurementTypeGetAllCompletedSubscription = $rootScope.$on(unitMeasurementTypeConstant.getAllCompletedEventName, onUnitMeasurementTypeGetAllCompleted);

        $rootScope.$on('$destroy', function () {
            numericalScalePrefixGetAllCompletedSubscription();
            numericalScaleGetAllCompletedSubscription();
            numericalScaleTypeCountryGetAllCompletedSubscription();
            numericalScaleTypeGetAllCompletedSubscription();
            unitMeasurementScaleGetAllCompletedSubscription();
            unitMeasurementGetAllCompletedSubscription();
            unitMeasurementTypeGetAllCompletedSubscription();
        });

        // *** Events Subscriptions        

        return serviceFactory;

    }]);