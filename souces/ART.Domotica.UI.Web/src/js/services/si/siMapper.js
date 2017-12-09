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
        unitMeasurementTypeConstant) {

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
            }
        });

        // *** Navigation Properties Mappers ***                      

        var mapper_UnitMeasurementScale_UnitMeasurement_Init = false;
        var mapper_UnitMeasurementScale_UnitMeasurement = function () {
            if (!mapper_UnitMeasurementScale_UnitMeasurement_Init && siContext.unitMeasurementScaleLoaded && siContext.unitMeasurementLoaded) {
                mapper_UnitMeasurementScale_UnitMeasurement_Init = true;
                for (var i = 0; i < siContext.unitMeasurementScale.length; i++) {
                    var unitMeasurementScale = siContext.unitMeasurementScale[i];
                    var unitMeasurement = unitMeasurementFinder.getByKey(unitMeasurementScale.unitMeasurementId, unitMeasurementScale.unitMeasurementTypeId);
                    //unitMeasurementScale.unitMeasurement = unitMeasurement;
                    if (unitMeasurement.unitMeasurementScales === undefined) {
                        unitMeasurement.unitMeasurementScales = [];
                    }
                    unitMeasurement.unitMeasurementScales.push(unitMeasurementScale);
                }
            }
        };       

        var mapper_UnitMeasurement_UnitMeasurementType_Init = false;
        var mapper_UnitMeasurement_UnitMeasurementType = function () {
            if (!mapper_UnitMeasurement_UnitMeasurementType_Init && siContext.unitMeasurementTypeLoaded && siContext.unitMeasurementLoaded) {
                mapper_UnitMeasurement_UnitMeasurementType_Init = true;
                for (var i = 0; i < siContext.unitMeasurement.length; i++) {
                    var unitMeasurement = siContext.unitMeasurement[i];
                    var unitMeasurementType = unitMeasurementTypeFinder.getByKey(unitMeasurement.unitMeasurementTypeId);
                    unitMeasurement.unitMeasurementType = unitMeasurementType;
                    if (unitMeasurementType.unitMeasurements === undefined) {
                        unitMeasurementType.unitMeasurements = [];
                    }
                    unitMeasurementType.unitMeasurements.push(unitMeasurement);
                }
            }
        };
        
        // *** Navigation Properties Mappers ***


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
            mapper_UnitMeasurementScale_UnitMeasurement();
        }

        var onUnitMeasurementGetAllCompleted = function (event, data) {
            unitMeasurementGetAllCompletedSubscription();
            siContext.unitMeasurementLoaded = true;
            mapper_UnitMeasurement_UnitMeasurementType();
            mapper_UnitMeasurementScale_UnitMeasurement();
        }

        var onUnitMeasurementTypeGetAllCompleted = function (event, data) {
            unitMeasurementTypeGetAllCompletedSubscription();
            siContext.unitMeasurementTypeLoaded = true;
            mapper_UnitMeasurement_UnitMeasurementType();
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