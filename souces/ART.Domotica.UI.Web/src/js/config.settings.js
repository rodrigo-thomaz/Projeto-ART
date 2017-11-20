// settings

var segurancaPresentationWebPagesUri = 'http://dev02-pc/ART.Security.UI.Web/';
var segurancaDistributedServicesUri = 'http://dev02-pc/ART.Security.WebApi/';

var distributedServicesUri = 'http://dev02-pc/ART.Domotica.WebApi/';

var wsBrokerHostName = 'file-server';
var wsBrokerPort = 15674;

var app =
    angular.module('app')
        .constant('ngAuthSettings', {
            segurancaPresentationWebPagesUri: segurancaPresentationWebPagesUri,
            segurancaDistributedServicesUri: segurancaDistributedServicesUri,
            distributedServicesUri: distributedServicesUri,
            wsBrokerHostName: wsBrokerHostName,
            wsBrokerPort: wsBrokerPort,
            clientId: 'ngAuthApp'
});