'use strict';

/**
 * Config for the router
 */
angular.module('app')
    .run(
    ['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ]
    )
    .config(
    ['$stateProvider', '$urlRouterProvider', 'JQ_CONFIG', 'MODULE_CONFIG',
        function ($stateProvider, $urlRouterProvider, JQ_CONFIG, MODULE_CONFIG) {

            var layout = "tpl/app.html";

            $urlRouterProvider
                .otherwise('/app/dashboard');

            $stateProvider
                .state('app', {
                    abstract: true,
                    url: '/app',
                    templateUrl: layout,
                    resolve: load([

                        'js/base64Helper.js',
                        'js/services/contextScope.js',

                        // Globalization

                        'js/services/globalization/globalizationContext.js',
                        'js/services/globalization/globalizationConstants.js',
                        'js/services/globalization/timeZoneFinder.js',
                        'js/services/globalization/globalizationMapper.js',

                        'js/services/globalization/timeZoneService.js',

                        // Locale

                        'js/services/locale/localeContext.js',
                        'js/services/locale/localeConstants.js',
                        'js/services/locale/localeMapper.js',

                        'js/services/locale/continentFinder.js',
                        'js/services/locale/continentService.js',
                        'js/services/locale/countryFinder.js',
                        'js/services/locale/countryService.js',

                        // SI

                        'js/services/si/siContext.js',
                        'js/services/si/siConstants.js',
                        'js/services/si/siMapper.js',

                        'js/services/si/numericalScaleFinder.js',
                        'js/services/si/numericalScalePrefixFinder.js',
                        'js/services/si/numericalScaleTypeFinder.js',
                        'js/services/si/numericalScaleTypeCountryFinder.js',

                        'js/services/si/unitMeasurementScaleFinder.js',
                        'js/services/si/unitMeasurementTypeFinder.js',
                        'js/services/si/unitMeasurementFinder.js',

                        'js/services/si/numericalScaleService.js',
                        'js/services/si/numericalScalePrefixService.js',
                        'js/services/si/numericalScaleTypeService.js',
                        'js/services/si/numericalScaleTypeCountryService.js',

                        'js/services/si/unitMeasurementScaleService.js',
                        'js/services/si/unitMeasurementTypeService.js',
                        'js/services/si/unitMeasurementService.js',
                        'js/services/si/unitMeasurementConverter.js',

                        //*** Hardware ***

                        // Sensor Datasheet

                        'js/services/sensorDatasheet/sensorDatasheetContext.js',
                        'js/services/sensorDatasheet/sensorDatasheetConstants.js',
                        'js/services/sensorDatasheet/sensorDatasheetMapper.js',

                        'js/services/sensorDatasheet/sensorTypeFinder.js',
                        'js/services/sensorDatasheet/sensorDatasheetFinder.js',
                        'js/services/sensorDatasheet/sensorDatasheetUnitMeasurementScaleFinder.js',
                        'js/services/sensorDatasheet/sensorDatasheetUnitMeasurementDefaultFinder.js',

                        'js/services/sensorDatasheet/sensorTypeService.js',
                        'js/services/sensorDatasheet/sensorDatasheetService.js',
                        'js/services/sensorDatasheet/sensorDatasheetUnitMeasurementScaleService.js',
                        'js/services/sensorDatasheet/sensorDatasheetUnitMeasurementDefaultService.js',

                        // Device Datasheet

                        'js/services/deviceDatasheet/deviceDatasheetContext.js',
                        'js/services/deviceDatasheet/deviceDatasheetConstants.js',
                        'js/services/deviceDatasheet/deviceDatasheetMapper.js',
                        'js/services/deviceDatasheet/deviceDatasheetFinder.js',                        
                        'js/services/deviceDatasheet/deviceDatasheetService.js',
                        
                        // Sensor                     

                        'js/services/sensor/sensorContext.js',
                        'js/services/sensor/sensorConstants.js',
                        'js/services/sensor/sensorMapper.js',

                        'js/services/sensor/sensorFinder.js',
                        'js/services/sensor/sensorUnitMeasurementScaleFinder.js',
                        'js/services/sensor/sensorTriggerFinder.js',
                        'js/services/sensor/sensorTempDSFamilyResolutionFinder.js',
                        'js/services/sensor/sensorTempDSFamilyFinder.js',

                        'js/services/sensor/sensorService.js',
                        'js/services/sensor/sensorUnitMeasurementScaleService.js',
                        'js/services/sensor/sensorTriggerService.js',
                        'js/services/sensor/sensorTempDSFamilyResolutionService.js',
                        'js/services/sensor/sensorTempDSFamilyService.js',

                        // Device

                        'js/services/device/deviceContext.js',
                        'js/services/device/deviceConstants.js',

                        'js/services/device/deviceWiFiFinder.js',
                        'js/services/device/deviceNTPFinder.js',
                        'js/services/device/deviceDebugFinder.js',
                        'js/services/device/deviceSensorsFinder.js',
                        'js/services/device/sensorInDeviceFinder.js',
                        'js/services/device/deviceFinder.js',

                        'js/services/device/deviceMapper.js',

                        'js/services/device/deviceWiFiService.js',
                        'js/services/device/deviceNTPService.js',
                        'js/services/device/deviceDebugService.js',
                        'js/services/device/deviceSensorsService.js',
                        'js/services/device/sensorInDeviceService.js',
                        'js/services/device/deviceService.js',
                    ])
                })
                .state('app.dashboard', {
                    url: '/dashboard',
                    templateUrl: 'views/dashboard.html',
                    resolve: load([
                        'js/services/dashboardService.js',
                        'js/controllers/dashboardController.js',
                    ])
                })

                .state('app.espDeviceList', {
                    url: '/espDeviceList',
                    templateUrl: 'views/espDeviceList.html',
                    resolve: load([
                        'vr.directives.slider',
                        'ui.select',
                        'toaster',

                        //*** Hardware ***                      

                        // Sensor                       

                        'js/controllers/sensor/sensorController.js',
                        'js/controllers/sensor/sensorUnitMeasurementScaleController.js',
                        'js/controllers/sensor/sensorTempDSFamilyController.js',
                        'js/controllers/sensor/sensorTriggersController.js',
                        'js/controllers/sensor/sensorTriggerController.js',

                        // Device

                        'js/controllers/device/deviceController.js',
                        'js/controllers/device/deviceWiFiController.js',
                        'js/controllers/device/deviceNTPController.js',
                        'js/controllers/device/deviceMQController.js',
                        'js/controllers/device/deviceDebugController.js',
                        'js/controllers/device/deviceSensorsController.js',
                        'js/controllers/device/sensorInDeviceController.js',

                        'js/controllers/espDeviceListController.js',

                    ])
                })
                .state('app.espDeviceManager', {
                    url: '/espDeviceManager',
                    templateUrl: 'views/espDeviceManager.html',
                    resolve: load([
                        'ui.grid',
                        'toaster',

                        'js/controllers/espDeviceJoinController.js',
                        'js/controllers/espDeviceManagerController.js',
                    ])
                })

                // admin
                .state('app.admin', {
                    url: '/admin',
                    template: '<div ui-view class="fade-in-down"></div>'
                })
                .state('app.admin.espDevice', {
                    url: '/admin/espDevice',
                    templateUrl: 'views/admin/espDeviceAdmin.html',
                    resolve: load([
                        'ui.grid',
                        'toaster',
                        'js/services/admin/espDeviceAdminService.js',
                        'js/controllers/admin/espDeviceAdminController.js',
                    ])
                })

                // pages
                .state('app.page', {
                    url: '/page',
                    template: '<div ui-view class="fade-in-down"></div>'
                })
                .state('app.page.profile', {
                    url: '/profile',
                    templateUrl: 'tpl/page_profile.html'
                })
                .state('app.docs', {
                    url: '/docs',
                    templateUrl: 'tpl/docs.html'
                })
                // others              
                .state('layout', {
                    abstract: true,
                    url: '/layout',
                    templateUrl: 'tpl/layout.html'
                })
                .state('layout.fullwidth', {
                    url: '/fullwidth',
                    views: {
                        '': {
                            templateUrl: 'tpl/layout_fullwidth.html'
                        },
                        'footer': {
                            templateUrl: 'tpl/layout_footer_fullwidth.html'
                        }
                    },
                    resolve: load(['js/controllers/vectormap.js'])
                })
                .state('layout.mobile', {
                    url: '/mobile',
                    views: {
                        '': {
                            templateUrl: 'tpl/layout_mobile.html'
                        },
                        'footer': {
                            templateUrl: 'tpl/layout_footer_mobile.html'
                        }
                    }
                })
                .state('layout.app', {
                    url: '/app',
                    views: {
                        '': {
                            templateUrl: 'tpl/layout_app.html'
                        },
                        'footer': {
                            templateUrl: 'tpl/layout_footer_fullwidth.html'
                        }
                    },
                    resolve: load(['js/controllers/tab.js'])
                })
                .state('apps', {
                    abstract: true,
                    url: '/apps',
                    templateUrl: 'tpl/layout.html'
                });

            function load(srcs, callback) {
                return {
                    deps: ['$ocLazyLoad', '$q',
                        function ($ocLazyLoad, $q) {
                            var deferred = $q.defer();
                            var promise = false;
                            srcs = angular.isArray(srcs) ? srcs : srcs.split(/\s+/);
                            if (!promise) {
                                promise = deferred.promise;
                            }
                            angular.forEach(srcs, function (src) {
                                promise = promise.then(function () {
                                    if (JQ_CONFIG[src]) {
                                        return $ocLazyLoad.load(JQ_CONFIG[src]);
                                    }
                                    angular.forEach(MODULE_CONFIG, function (module) {
                                        if (module.name === src) {
                                            name = module.name;
                                        } else {
                                            name = src;
                                        }
                                    });
                                    return $ocLazyLoad.load(name);
                                });
                            });
                            deferred.resolve();
                            return callback ? promise.then(function () { return callback(); }) : promise;
                        }]
                }
            }


        }
    ]
    );
