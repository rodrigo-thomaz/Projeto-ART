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

              .state('app.devices', {
                  url: '/devices',
                  templateUrl: 'views/devices.html',
                  resolve: load([
                      'vr.directives.slider',
                      'ui.select',                      

                      'js/services/devicesService.js',

                      'js/controllers/dsFamilyTempSensorController.js',
                      'js/controllers/thermometerDeviceController.js',
                      'js/controllers/devicesController.js',
                  ])
              })
              .state('app.sensors', {
                  url: '/sensors',
                  templateUrl: 'views/sensors.html',
                  resolve: load([
                      'vr.directives.slider',
                      'ui.select',

                      'js/services/sensorsService.js',

                      'js/controllers/dsFamilyTempSensorController.js',
                      'js/controllers/thermometerDeviceController.js',
                      'js/controllers/sensorsController.js',
                  ])
              })
              // admin
              .state('app.admin', {
                  url: '/admin',
                  template: '<div ui-view class="fade-in-down"></div>'
              })  
              .state('app.admin.hardware', {
                  url: '/hardware',
                  templateUrl: 'views/hardware.html',
                  resolve: load([
                      'ui.grid',
                      'js/services/hardwareService.js',
                      'js/controllers/hardwareController.js',
                  ])
              })
              .state('app.hardwaresInApplication', {
                  url: '/hardwaresInApplication',
                  templateUrl: 'views/hardwaresInApplication.html',
                  resolve: load([
                      'js/services/hardwaresInApplicationService.js',
                      'js/controllers/hardwaresInApplicationController.js',
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
                          if( module.name == src){
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
