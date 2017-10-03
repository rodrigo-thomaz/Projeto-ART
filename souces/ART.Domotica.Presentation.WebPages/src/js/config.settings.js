// settings

var segurancaPresentationWebPagesUri = 'http://localhost/ART.Seguranca.Presentation.WebPages/index.html';
var segurancaDistributedServicesUri = 'http://localhost/ART.Seguranca.DistributedServices/';
var distributedServicesUri = 'http://localhost/ART.DistributedServices/';
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