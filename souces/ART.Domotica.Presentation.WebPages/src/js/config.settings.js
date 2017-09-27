// settings

var segurancaPresentationWebPagesUri = 'http://localhost/ART.Seguranca.Presentation.WebPages/index.html';
var segurancaDistributedServicesUri = 'http://localhost/ART.Seguranca.DistributedServices/';
var domoticaDistributedServicesUri = 'http://localhost/ART.Domotica.DistributedServices/';
var wsbrokerUri = 'file-server';
var wsport = 15675;

var app =
    angular.module('app')
        .constant('ngAuthSettings', {
            segurancaPresentationWebPagesUri: segurancaPresentationWebPagesUri,
            segurancaDistributedServicesUri: segurancaDistributedServicesUri,
            domoticaDistributedServicesUri: domoticaDistributedServicesUri,
            wsbrokerUri: wsbrokerUri,
            wsport: wsport,
            clientId: 'ngAuthApp'
});