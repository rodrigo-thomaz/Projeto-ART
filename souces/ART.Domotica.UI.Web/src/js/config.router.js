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
                      'js/services/temperatureScaleService.js',
                      'js/services/temperatureScaleConverter.js',
                      'js/services/timeZoneService.js',
                      'js/services/dsFamilyTempSensorResolutionService.js',
                      'js/services/dsFamilyTempSensorService.js',
                      'js/services/espDeviceService.js',
                      'js/controllers/espDeviceListController.js',                      
                  ])
              })
              .state('app.espDeviceManager', {
                  url: '/espDeviceManager',
                  templateUrl: 'views/espDeviceManager.html',
                  resolve: load([
                      'ui.grid',
                      'toaster',
                      'js/services/espDeviceService.js',
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
