'use strict';

app.factory('applicationBroker', [function () {

    var serviceFactory = {};    
    
    var init = function (applicationBroker) {
        serviceFactory.brokerApplicationTopic = applicationBroker.brokerApplicationTopic;
        serviceFactory.initialized = true;
    }
    
    //Functions
    serviceFactory.init = init;

    //Properties
    serviceFactory.initialized = false;
    serviceFactory.brokerApplicationTopic = null;

    return serviceFactory;

}]);

app.factory('applicationBrokerSettingService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'applicationBroker', function ($http, $log, ngAuthSettings, $rootScope, stompService, applicationBroker) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    serviceFactory.applicationBrokerSetting = {};

    var initialized = false;
    var initializing = false;

    var onConnected = function () {
        if (!initialized && initializing) {
            return;
        }
        else if (!initialized && !initializing) {
            stompService.subscribe('ApplicationBrokerSetting.GetViewCompleted', onGetApplicationBrokerSettingCompleted);
            getApplicationBrokerSetting();
            initializing = true;            
        }    
    }   

    var getApplicationBrokerSetting = function () {
        return $http.post(serviceBase + 'api/applicationBrokerSetting/get').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetApplicationBrokerSettingCompleted = function (payload) {        
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        applicationBroker.init(JSON.parse(dataUTF8));
        clearOnConnected();
        initialized = true;
        initializing = false;
        $rootScope.$emit('applicationBrokerSettingServiceInitialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.client.connected)
        onConnected();

    return serviceFactory;   

}]);