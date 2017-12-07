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
    [          '$stateProvider', '$urlRouterProvider', 'JQ_CONFIG', 'MODULE_CONFIG', 
      function ($stateProvider,   $urlRouterProvider, JQ_CONFIG, MODULE_CONFIG) {

          var layout = "tpl/app.html";

          $urlRouterProvider
              .otherwise('/app/dashboard');
          
          $stateProvider
              .state('app', {
                  abstract: true,
                  url: '/app',
                  templateUrl: layout
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
                      
                      'js/services/contextScope.js',

                      // Globalization

                      'js/services/globalization/globalizationContext.js',
                      'js/services/globalization/globalizationConstants.js',
                      'js/services/globalization/globalizationFinder.js',
                      'js/services/globalization/globalizationMapper.js',

                      'js/services/globalization/timeZoneService.js',

                      // Locale

                      'js/services/locale/localeContext.js',
                      'js/services/locale/localeConstants.js',
                      'js/services/locale/localeFinder.js',
                      'js/services/locale/localeMapper.js',
                      
                      'js/services/locale/continentService.js',
                      'js/services/locale/countryService.js',

                      // SI

                      'js/services/si/siContext.js',
                      'js/services/si/siConstants.js',
                      'js/services/si/siFinder.js',
                      'js/services/si/siMapper.js',

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

                      'js/services/hardware/sensorDatasheet/sensorDatasheetContext.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetConstants.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetFinder.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetMapper.js',

                      'js/services/hardware/sensorDatasheet/sensorTypeService.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetService.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetUnitMeasurementScaleService.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetUnitMeasurementDefaultService.js',

                      // Device

                      'js/services/hardware/device/deviceContext.js',
                      'js/services/hardware/device/deviceConstants.js',
                      'js/services/hardware/device/deviceFinder.js',
                      'js/services/hardware/device/deviceMapper.js',

                      'js/services/hardware/device/deviceNTPService.js',
                      'js/services/hardware/device/deviceSensorsService.js',
                      'js/services/hardware/device/sensorInDeviceService.js',
                      'js/services/hardware/device/deviceService.js',                     

                      // Sensor                     

                      'js/services/hardware/sensor/sensorContext.js',
                      'js/services/hardware/sensor/sensorConstants.js',
                      'js/services/hardware/sensor/sensorFinder.js',
                      'js/services/hardware/sensor/sensorMapper.js',

                      'js/services/hardware/sensor/sensorService.js',                                            
                      'js/services/hardware/sensor/sensorUnitMeasurementScaleService.js',
                      'js/services/hardware/sensor/sensorTriggerService.js',
                      'js/services/hardware/sensor/sensorTempDSFamilyResolutionService.js',
                      'js/services/hardware/sensor/sensorTempDSFamilyService.js',                      

                      'js/controllers/sensorUnitMeasurementScaleController.js',
                      'js/controllers/sensorTriggerController.js',                      

                      'js/controllers/espDeviceListController.js',                      
                  ])
              })
              .state('app.espDeviceManager', {
                  url: '/espDeviceManager',
                  templateUrl: 'views/espDeviceManager.html',
                  resolve: load([
                      'ui.grid',
                      'toaster',

                      'js/services/contextScope.js',

                      // Globalization

                      'js/services/globalization/globalizationContext.js',
                      'js/services/globalization/globalizationConstants.js',
                      'js/services/globalization/globalizationFinder.js',
                      'js/services/globalization/globalizationMapper.js',

                      'js/services/globalization/timeZoneService.js',                      

                      // Locale

                      'js/services/locale/localeContext.js',
                      'js/services/locale/localeConstants.js',
                      'js/services/locale/localeFinder.js',
                      'js/services/locale/localeMapper.js',

                      'js/services/locale/continentService.js',
                      'js/services/locale/countryService.js',

                      // SI

                      'js/services/si/siContext.js',
                      'js/services/si/siConstants.js',
                      'js/services/si/siFinder.js',
                      'js/services/si/siMapper.js',                      

                      //*** Hardware ***

                      // Sensor Datasheet

                      'js/services/hardware/sensorDatasheet/sensorDatasheetContext.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetConstants.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetFinder.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetMapper.js',

                      'js/services/hardware/sensorDatasheet/sensorTypeService.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetService.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetUnitMeasurementScaleService.js',
                      'js/services/hardware/sensorDatasheet/sensorDatasheetUnitMeasurementDefaultService.js',

                      // Device

                      'js/services/hardware/device/deviceContext.js',
                      'js/services/hardware/device/deviceConstants.js',
                      'js/services/hardware/device/deviceFinder.js',
                      'js/services/hardware/device/deviceMapper.js',

                      'js/services/hardware/device/deviceNTPService.js',
                      'js/services/hardware/device/deviceSensorsService.js',
                      'js/services/hardware/device/sensorInDeviceService.js',
                      'js/services/hardware/device/deviceService.js',            

                      'js/controllers/espDeviceJoinController.js',
                      'js/controllers/espDeviceManagerController.js',

                      // Sensor
                      'js/services/hardware/sensor/sensorContext.js',
                      'js/services/hardware/sensor/sensorConstants.js',
                      'js/services/hardware/sensor/sensorFinder.js',
                      'js/services/hardware/sensor/sensorMapper.js',

                      'js/services/hardware/sensor/sensorService.js',
                      'js/services/hardware/sensor/sensorUnitMeasurementScaleService.js',
                      'js/services/hardware/sensor/sensorTriggerService.js',
                      'js/services/hardware/sensor/sensorTempDSFamilyResolutionService.js',
                      'js/services/hardware/sensor/sensorTempDSFamilyService.js',               
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
                  resolve: load( ['js/controllers/vectormap.js'] )
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
                  resolve: load( ['js/controllers/tab.js'] )
              })
              .state('apps', {
                  abstract: true,
                  url: '/apps',
                  templateUrl: 'tpl/layout.html'
              });

          function load(srcs, callback) {
            return {
                deps: ['$ocLazyLoad', '$q',
                  function( $ocLazyLoad, $q ){
                    var deferred = $q.defer();
                    var promise  = false;
                    srcs = angular.isArray(srcs) ? srcs : srcs.split(/\s+/);
                    if(!promise){
                      promise = deferred.promise;
                    }
                    angular.forEach(srcs, function(src) {
                      promise = promise.then( function(){
                        if(JQ_CONFIG[src]){
                          return $ocLazyLoad.load(JQ_CONFIG[src]);
                        }
                        angular.forEach(MODULE_CONFIG, function(module) {
                          if( module.name === src){
                            name = module.name;
                          }else{
                            name = src;
                          }
                        });
                        return $ocLazyLoad.load(name);
                      } );
                    });
                    deferred.resolve();
                    return callback ? promise.then(function(){ return callback(); }) : promise;
                }]
            }
          }


      }
    ]
  );
