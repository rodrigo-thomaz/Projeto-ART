'use strict';
app.factory('sensorMapper', ['$rootScope', 'sensorContext', 'sensorConstant',
    function ($rootScope, sensorContext, sensorConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***



    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onSensorGetAllByApplicationIdCompleted = function (event, data) {
        sensorGetAllByApplicationIdCompletedSubscription();
        sensorContext.sensorLoaded = true;
    }   

    var sensorGetAllByApplicationIdCompletedSubscription = $rootScope.$on(sensorConstant.getAllByApplicationIdCompletedEventName, onSensorGetAllByApplicationIdCompleted);

    $rootScope.$on('$destroy', function () {
        sensorTypeGetAllByApplicationIdCompletedSubscription();        
    });

    // *** Events Subscriptions
    
    
    return serviceFactory;

}]);