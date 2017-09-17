using RThomaz.Domain.Financeiro.Services;
using RThomaz.DataManager.Helpers;
using RThomaz.Infra.CrossCutting.Storage;
using System.IO;
using RThomaz.Domain.Financeiro.Services.DTOs;
using System;

namespace RThomaz.DataManager.Managers
{
    public class BancoManager
    {
        private readonly BancoDetailViewDTO _detailContractSample;

        public BancoManager()
        {
            System.Console.WriteLine("Cadastrando bancos...");

            var service = new BancoService();

            service.AplicacaoId = Guid.NewGuid();
            service.StorageBucketName = "rthomaz-client-49d2d654-ee86-47df-8db4-910c3cf89708";

            service.Insert(new BancoDetailInsertDTO("Banco A.J.Renner S.A.", "Banco A.J.Renner S.A.", "654", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco ABC Brasil S.A.", "Banco ABC Brasil S.A.", "246", null, null, null, "www.abcbrasil.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Alfa S.A.", "Banco Alfa S.A.", "25", null, null, null, "www.bancoalfa.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Alvorada S.A.", "Banco Alvorada S.A.", "641", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Arbi S.A.", "Banco Arbi S.A.", "213", null, null, null, "www.arbi.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Azteca do Brasil S.A.", "Banco Azteca do Brasil S.A.", "19", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Banerj S.A.", "Banco Banerj S.A.", "29", null, null, null, "www.banerj.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bankpar S.A.", "Banco Bankpar S.A.", "0", null, null, null, "www.aexp.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Barclays S.A.", "Banco Barclays S.A.", "740", null, null, null, "www.barclays.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BBM S.A.", "Banco BBM S.A.", "107", null, null, null, "www.bbmbank.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Beg S.A.", "Banco Beg S.A.", "31", null, null, null, "www.itau.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BGN S.A.", "Banco BGN S.A.", "739", null, null, null, "www.bgn.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BM&F de Serviços de Liquidação e Custódia S.A", "Banco BM&F de Serviços de Liquidação e Custódia S.A", "96", null, null, null, "www.bmf.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BMG S.A.", "Banco BMG S.A.", "318", null, null, null, "www.bancobmg.com.br", null, string.Empty, true, GetBancoImage("BMG.svg")));
            service.Insert(new BancoDetailInsertDTO("Banco BNP Paribas Brasil S.A.", "Banco BNP Paribas Brasil S.A.", "752", null, null, null, "www.bnpparibas.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Boavista Interatlântico S.A.", "Banco Boavista Interatlântico S.A.", "248", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bonsucesso S.A.", "Banco Bonsucesso S.A.", "218", null, null, null, "www.bancobonsucesso.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bracce S.A.", "Banco Bracce S.A.", "65", null, null, null, "www.lemon.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bradesco BBI S.A.", "Banco Bradesco BBI S.A.", "36", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bradesco Cartões S.A.", "Banco Bradesco Cartões S.A.", "204", null, null, null, "www.iamex.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bradesco Financiamentos S.A.", "Banco Bradesco Financiamentos S.A.", "394", null, null, null, "www.bmc.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Bradesco S.A.", "Banco Bradesco S.A.", "237", null, null, null, "www.bradesco.com.br", null, string.Empty, true, GetBancoImage("Bradesco.svg")));
            service.Insert(new BancoDetailInsertDTO("Banco Brascan S.A.", "Banco Brascan S.A.", "225", null, null, null, "www.bancobrascan.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BRJ S.A.", "Banco BRJ S.A.", "M15", null, null, null, "www.brjbank.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BTG Pactual S.A.", "Banco BTG Pactual S.A.", "208", null, null, null, "www.pactual.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco BVA S.A.", "Banco BVA S.A.", "44", null, null, null, "www.bancobva.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Cacique S.A.", "Banco Cacique S.A.", "263", null, null, null, "www.bancocacique.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Caixa Geral - Brasil S.A.", "Banco Caixa Geral - Brasil S.A.", "473", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Capital S.A.", "Banco Capital S.A.", "412", null, null, null, "www.bancocapital.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Cargill S.A.", "Banco Cargill S.A.", "40", null, null, null, "www.bancocargill.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Citibank S.A.", "Banco Citibank S.A.", "745", null, null, null, "www.citibank.com.br", null, string.Empty, true, GetBancoImage("Citibank.svg")));
            service.Insert(new BancoDetailInsertDTO("Banco Citicard S.A.", "Banco Citicard S.A.", "M08", null, null, null, "www.credicardbanco.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Clássico S.A.", "Banco Clássico S.A.", "241", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco CNH Capital S.A.", "Banco CNH Capital S.A.", "M19", null, null, null, "www.bancocnh.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Comercial e de Investimento Sudameris S.A.", "Banco Comercial e de Investimento Sudameris S.A.", "215", null, null, null, "www.sudameris.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Cooperativo do Brasil S.A. - BANCOOB", "Banco Cooperativo do Brasil S.A. - BANCOOB", "756", null, null, null, "www.bancoob.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Cooperativo Sicredi S.A.", "Banco Cooperativo Sicredi S.A.", "748", null, null, null, "www.sicredi.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco CR2 S.A.", "Banco CR2 S.A.", "75", null, null, null, "www.bancocr2.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Credibel S.A.", "Banco Credibel S.A.", "721", null, null, null, "www.credibel.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Credit Agricole Brasil S.A.", "Banco Credit Agricole Brasil S.A.", "222", null, null, null, "www.calyon.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Credit Suisse (Brasil) S.A.", "Banco Credit Suisse (Brasil) S.A.", "505", null, null, null, "www.csfb.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Cruzeiro do Sul S.A.", "Banco Cruzeiro do Sul S.A.", "229", null, null, null, "www.bcsul.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Cédula S.A.", "Banco Cédula S.A.", "266", null, null, null, "www.bancocedula.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco da Amazônia S.A.", "Banco da Amazônia S.A.", "3", null, null, null, "www.bancoamazonia.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco da China Brasil S.A.", "Banco da China Brasil S.A.", "083-3", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Daimlerchrysler S.A.", "Banco Daimlerchrysler S.A.", "M21", null, null, null, "www.bancodaimlerchrysler.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Daycoval S.A.", "Banco Daycoval S.A.", "707", null, null, null, "www.daycoval.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco de La Nacion Argentina", "Banco de La Nacion Argentina", "300", null, null, null, "www.bna.com.ar", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco de La Provincia de Buenos Aires", "Banco de La Provincia de Buenos Aires", "495", null, null, null, "www.bapro.com.ar", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco de La Republica Oriental del Uruguay", "Banco de La Republica Oriental del Uruguay", "494", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco de Lage Landen Brasil S.A.", "Banco de Lage Landen Brasil S.A.", "M06", null, null, null, "www.delagelanden.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco de Pernambuco S.A. - BANDEPE", "Banco de Pernambuco S.A. - BANDEPE", "24", null, null, null, "www.bandepe.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco de Tokyo-Mitsubishi UFJ Brasil S.A.", "Banco de Tokyo-Mitsubishi UFJ Brasil S.A.", "456", null, null, null, "www.btm.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Dibens S.A.", "Banco Dibens S.A.", "214", null, null, null, "www.bancodibens.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco do Brasil S.A.", "Banco do Brasil S.A.", "1", null, null, null, "www.bb.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco do Estado de Sergipe S.A.", "Banco do Estado de Sergipe S.A.", "47", null, null, null, "www.banese.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco do Estado do Pará S.A.", "Banco do Estado do Pará S.A.", "37", null, null, null, "www.banparanet.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco do Estado do Piauí S.A. - BEP", "Banco do Estado do Piauí S.A. - BEP", "39", null, null, null, "www.bep.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco do Estado do Rio Grande do Sul S.A.", "Banco do Estado do Rio Grande do Sul S.A.", "41", null, null, null, "www.banrisul.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco do Nordeste do Brasil S.A.", "Banco do Nordeste do Brasil S.A.", "4", null, null, null, "www.banconordeste.gov.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Fator S.A.", "Banco Fator S.A.", "265", null, null, null, "www.bancofator.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Fiat S.A.", "Banco Fiat S.A.", "M03", null, null, null, "www.bancofiat.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Fibra S.A.", "Banco Fibra S.A.", "224", null, null, null, "www.bancofibra.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Ficsa S.A.", "Banco Ficsa S.A.", "626", null, null, null, "www.ficsa.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Ford S.A.", "Banco Ford S.A.", "M18", null, null, null, "www.bancoford.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco GE Capital S.A.", "Banco GE Capital S.A.", "233", null, null, null, "www.ge.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Gerdau S.A.", "Banco Gerdau S.A.", "734", null, null, null, "www.bancogerdau.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco GMAC S.A.", "Banco GMAC S.A.", "M07", null, null, null, "www.bancogm.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Guanabara S.A.", "Banco Guanabara S.A.", "612", null, null, null, "www.bcoguan.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Honda S.A.", "Banco Honda S.A.", "M22", null, null, null, "www.bancohonda.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Ibi S.A. Banco Múltiplo", "Banco Ibi S.A. Banco Múltiplo", "63", null, null, null, "www.ibi.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco IBM S.A.", "Banco IBM S.A.", "M11", null, null, null, "www.ibm.com/br/financing/", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Industrial do Brasil S.A.", "Banco Industrial do Brasil S.A.", "604", null, null, null, "www.bancoindustrial.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Industrial e Comercial S.A.", "Banco Industrial e Comercial S.A.", "320", null, null, null, "www.bicbanco.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Indusval S.A.", "Banco Indusval S.A.", "653", null, null, null, "www.indusval.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Intercap S.A.", "Banco Intercap S.A.", "630", null, null, null, "www.intercap.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Intermedium S.A.", "Banco Intermedium S.A.", "077-9", null, null, null, "www.intermedium.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Investcred Unibanco S.A.", "Banco Investcred Unibanco S.A.", "249", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Itaucred Financiamentos S.A.", "Banco Itaucred Financiamentos S.A.", "M09", null, null, null, "www.itaucred.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Itaú BBA S.A.", "Banco Itaú BBA S.A.", "184", null, null, null, "www.itaubba.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco ItaúBank S.A", "Banco ItaúBank S.A", "479", null, null, null, "www.itaubank.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco J. P. Morgan S.A.", "Banco J. P. Morgan S.A.", "376", null, null, null, "www.jpmorgan.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco J. Safra S.A.", "Banco J. Safra S.A.", "74", null, null, null, "www.jsafra.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco John Deere S.A.", "Banco John Deere S.A.", "217", null, null, null, "www.johndeere.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco KDB S.A.", "Banco KDB S.A.", "76", null, null, null, "www.bancokdb.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco KEB do Brasil S.A.", "Banco KEB do Brasil S.A.", "757", null, null, null, "www.bancokeb.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Luso Brasileiro S.A.", "Banco Luso Brasileiro S.A.", "600", null, null, null, "www.lusobrasileiro.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Matone S.A.", "Banco Matone S.A.", "212", null, null, null, "www.bancomatone.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Maxinvest S.A.", "Banco Maxinvest S.A.", "M12", null, null, null, "www.bancomaxinvest.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Mercantil do Brasil S.A.", "Banco Mercantil do Brasil S.A.", "389", null, null, null, "www.mercantil.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Modal S.A.", "Banco Modal S.A.", "746", null, null, null, "www.bancomodal.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Moneo S.A.", "Banco Moneo S.A.", "M10", null, null, null, "www.bancomoneo.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Morada S.A.", "Banco Morada S.A.", "738", null, null, null, "www.bancomorada.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Morgan Stanley S.A.", "Banco Morgan Stanley S.A.", "66", null, null, null, "www.morganstanley.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Máxima S.A.", "Banco Máxima S.A.", "243", null, null, null, "www.bancomaxima.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Opportunity S.A.", "Banco Opportunity S.A.", "45", null, null, null, "www.opportunity.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Ourinvest S.A.", "Banco Ourinvest S.A.", "M17", null, null, null, "www.ourinvest.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Panamericano S.A.", "Banco Panamericano S.A.", "623", null, null, null, "www.panamericano.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Paulista S.A.", "Banco Paulista S.A.", "611", null, null, null, "www.bancopaulista.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Pecúnia S.A.", "Banco Pecúnia S.A.", "613", null, null, null, "www.bancopecunia.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Petra S.A.", "Banco Petra S.A.", "094-2", null, null, null, "www.personaltrader.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Pine S.A.", "Banco Pine S.A.", "643", null, null, null, "www.bancopine.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Porto Seguro S.A.", "Banco Porto Seguro S.A.", "724", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Pottencial S.A.", "Banco Pottencial S.A.", "735", null, null, null, "www.pottencial.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Prosper S.A.", "Banco Prosper S.A.", "638", null, null, null, "www.bancoprosper.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco PSA Finance Brasil S.A.", "Banco PSA Finance Brasil S.A.", "M24", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Rabobank International Brasil S.A.", "Banco Rabobank International Brasil S.A.", "747", null, null, null, "www.rabobank.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Randon S.A.", "Banco Randon S.A.", "088-4", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Real S.A.", "Banco Real S.A.", "356", null, null, null, "www.bancoreal.com.br", null, string.Empty, true, GetBancoImage("Real.svg")));
            service.Insert(new BancoDetailInsertDTO("Banco Rendimento S.A.", "Banco Rendimento S.A.", "633", null, null, null, "www.rendimento.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Ribeirão Preto S.A.", "Banco Ribeirão Preto S.A.", "741", null, null, null, "www.brp.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Rodobens S.A.", "Banco Rodobens S.A.", "M16", null, null, null, "www.rodobens.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Rural Mais S.A.", "Banco Rural Mais S.A.", "72", null, null, null, "www.rural.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Rural S.A.", "Banco Rural S.A.", "453", null, null, null, "www.rural.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Safra S.A.", "Banco Safra S.A.", "422", null, null, null, "www.safra.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Santander (Brasil) S.A.", "Banco Santander (Brasil) S.A.", "33", null, null, null, "www.santander.com.br", null, string.Empty, true, GetBancoImage("Santander.svg")));
            service.Insert(new BancoDetailInsertDTO("Banco Schahin S.A.", "Banco Schahin S.A.", "250", null, null, null, "www.bancoschahin.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Semear S.A.", "Banco Semear S.A.", "743", null, null, null, "www.bancosemear.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Simples S.A.", "Banco Simples S.A.", "749", null, null, null, "www.bancosimples.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Société Générale Brasil S.A.", "Banco Société Générale Brasil S.A.", "366", null, null, null, "www.sgbrasil.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Sofisa S.A.", "Banco Sofisa S.A.", "637", null, null, null, "www.sofisa.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Standard de Investimentos S.A.", "Banco Standard de Investimentos S.A.", "12", null, null, null, "www.standardbank.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Sumitomo Mitsui Brasileiro S.A.", "Banco Sumitomo Mitsui Brasileiro S.A.", "464", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Topázio S.A.", "Banco Topázio S.A.", "082-5", null, null, null, "www.bancotopazio.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Toyota do Brasil S.A.", "Banco Toyota do Brasil S.A.", "M20", null, null, null, "www.bancotoyota.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Tricury S.A.", "Banco Tricury S.A.", "M13", null, null, null, "www.tricury.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Triângulo S.A.", "Banco Triângulo S.A.", "634", null, null, null, "www.tribanco.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Volkswagen S.A.", "Banco Volkswagen S.A.", "M14", null, null, null, "www.bancovw.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Volvo (Brasil) S.A.", "Banco Volvo (Brasil) S.A.", "M23", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco Votorantim S.A.", "Banco Votorantim S.A.", "655", null, null, null, "www.bancovotorantim.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco VR S.A.", "Banco VR S.A.", "610", null, null, null, "www.vr.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banco WestLB do Brasil S.A.", "Banco WestLB do Brasil S.A.", "370", null, null, null, "www.westlb.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("BANESTES S.A. Banco do Estado do Espírito Santo", "BANESTES S.A. Banco do Estado do Espírito Santo", "21", null, null, null, "www.banestes.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Banif-Banco Internacional do Funchal (Brasil)S.A.", "Banif-Banco Internacional do Funchal (Brasil)S.A.", "719", null, null, null, "www.banif.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Bank of America Merrill Lynch Banco Múltiplo S.A.", "Bank of America Merrill Lynch Banco Múltiplo S.A.", "755", null, null, null, "www.ml.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("BankBoston N.A.", "BankBoston N.A.", "744", null, null, null, "www.bankboston.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("BB Banco Popular do Brasil S.A.", "BB Banco Popular do Brasil S.A.", "73", null, null, null, "www.bancopopulardobrasil.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("BES Investimento do Brasil S.A.-Banco de Investimento", "BES Investimento do Brasil S.A.-Banco de Investimento", "78", null, null, null, "www.besinvestimento.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("BPN Brasil Banco Múltiplo S.A.", "BPN Brasil Banco Múltiplo S.A.", "69", null, null, null, "www.bpnbrasil.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("BRB - Banco de Brasília S.A.", "BRB - Banco de Brasília S.A.", "70", null, null, null, "www.brb.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Brickell S.A. Crédito, financiamento e Investimento", "Brickell S.A. Crédito, financiamento e Investimento", "092-2", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Caixa Econômica Federal", "Caixa Econômica Federal", "104", null, null, null, "www.caixa.gov.br", null, string.Empty, true, GetBancoImage("Caixa_Economica_Federal.svg")));
            service.Insert(new BancoDetailInsertDTO("Citibank N.A.", "Citibank N.A.", "477", null, null, null, "www.citibank.com/brasil", null, string.Empty, true, GetBancoImage("Citibank.svg")));
            service.Insert(new BancoDetailInsertDTO("Concórdia Banco S.A.", "Concórdia Banco S.A.", "081-7", null, null, null, "www.concordiabanco.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Cooperativa Central de Crédito Noroeste Brasileiro Ltda.", "Cooperativa Central de Crédito Noroeste Brasileiro Ltda.", "097-3", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Cooperativa Central de Crédito Urbano-CECRED", "Cooperativa Central de Crédito Urbano-CECRED", "085-x", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Cooperativa Central de Economia e Credito Mutuo das Unicreds (x)", "Cooperativa Central de Economia e Credito Mutuo das Unicreds (x)", "099-x", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Cooperativa Central de Economia e Crédito Mutuo das Unicreds (2)", "Cooperativa Central de Economia e Crédito Mutuo das Unicreds (2)", "090-2", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Cooperativa de Crédito Rural da Região de Mogiana", "Cooperativa de Crédito Rural da Região de Mogiana", "089-2", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Cooperativa Unicred Central Santa Catarina", "Cooperativa Unicred Central Santa Catarina", "087-6", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Credicorol Cooperativa de Crédito Rural", "Credicorol Cooperativa de Crédito Rural", "098-1", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Deutsche Bank S.A. - Banco Alemão", "Deutsche Bank S.A. - Banco Alemão", "487", null, null, null, "www.deutsche-bank.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Dresdner Bank Brasil S.A. - Banco Múltiplo", "Dresdner Bank Brasil S.A. - Banco Múltiplo", "751", null, null, null, "www.dkib.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Goldman Sachs do Brasil Banco Múltiplo S.A.", "Goldman Sachs do Brasil Banco Múltiplo S.A.", "64", null, null, null, "www.goldmansachs.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Hipercard Banco Múltiplo S.A.", "Hipercard Banco Múltiplo S.A.", "62", null, null, null, "www.hipercard.com.br", null, string.Empty, true, GetBancoImage("Hipercard.svg")));
            service.Insert(new BancoDetailInsertDTO("HSBC Bank Brasil S.A. - Banco Múltiplo", "HSBC Bank Brasil S.A. - Banco Múltiplo", "399", null, null, null, "www.hsbc.com.br", null, string.Empty, true, GetBancoImage("HSBC.svg")));
            service.Insert(new BancoDetailInsertDTO("HSBC Finance (Brasil) S.A. - Banco Múltiplo", "HSBC Finance (Brasil) S.A. - Banco Múltiplo", "168", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("ING Bank N.V.", "ING Bank N.V.", "492", null, null, null, "www.ing.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Itaú Unibanco Holding S.A.", "Itaú Unibanco Holding S.A.", "652", null, null, null, "www.itau.com.br", null, string.Empty, true, null));
            var task = service.Insert(new BancoDetailInsertDTO("Itaú Unibanco S.A.", "Itaú Unibanco S.A.", "341", null, null, null, "www.itau.com.br", null, string.Empty, true, GetBancoImage("Itau.svg")));
            task.Wait();
            //_detailContractSample = task.Result;
            service.Insert(new BancoDetailInsertDTO("JBS Banco S.A.", "JBS Banco S.A.", "79", null, null, null, "www.bancojbs.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("JPMorgan Chase Bank", "JPMorgan Chase Bank", "488", null, null, null, "www.jpmorganchase.com", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Natixis Brasil S.A. Banco Múltiplo", "Natixis Brasil S.A. Banco Múltiplo", "14", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("NBC Bank Brasil S.A. - Banco Múltiplo", "NBC Bank Brasil S.A. - Banco Múltiplo", "753", null, null, null, "www.nbcbank.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("OBOE Crédito Financiamento e Investimento S.A.", "OBOE Crédito Financiamento e Investimento S.A.", "086-8", null, null, null, "", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Paraná Banco S.A.", "Paraná Banco S.A.", "254", null, null, null, "www.paranabanco.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("UNIBANCO - União de Bancos Brasileiros S.A.", "UNIBANCO - União de Bancos Brasileiros S.A.", "409", null, null, null, "www.unibanco.com.br", null, string.Empty, true, GetBancoImage("Unibanco.svg")));
            service.Insert(new BancoDetailInsertDTO("Unicard Banco Múltiplo S.A.", "Unicard Banco Múltiplo S.A.", "230", null, null, null, "www.unicard.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Unicred Central do Rio Grande do Sul", "Unicred Central do Rio Grande do Sul", "091-4", null, null, null, "www.unicred-rs.com.br", null, string.Empty, true, null));
            service.Insert(new BancoDetailInsertDTO("Unicred Norte do Paraná", "Unicred Norte do Paraná", "84", null, null, null, "", null, string.Empty, true, null));
        }

        private StorageUploadDTO GetBancoImage(string fileName)
        {
            var fullFileName = Path.Combine(DirectoryHelper.DefaultSampleDirectory, @"Images\Bancos\", fileName);
            var data = File.ReadAllBytes(fullFileName);
            return new StorageUploadDTO(fileName, data);
        }

        public BancoDetailViewDTO DetailContractSample
        {
            get
            {
                return _detailContractSample;
            }
        }
    }
}
