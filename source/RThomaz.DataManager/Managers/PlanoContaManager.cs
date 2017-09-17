using RThomaz.Domain.Financeiro.Services;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Diagnostics;

namespace RThomaz.DataManager.Managers
{
    public class PlanoContaManager
    {
        public PlanoContaManager()
        {
            System.Console.WriteLine("Cadastrando planos de conta...");

            CarregaContaReceber();
            CarregaContaPagar();
        }

        private void CarregaContaReceber()
        {
            try
            {
                var service = new PlanoContaService();
                service.AplicacaoId = Guid.NewGuid();
                
                var comercialNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, null, "Comercial", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, comercialNode.PlanoContaId, "Serviços", true, "")).Wait();
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, comercialNode.PlanoContaId, "Vendas", true, "")).Wait();

                var financeiroNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, null, "Financeiro", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, financeiroNode.PlanoContaId, "Depósito", true, "")).Wait();
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, financeiroNode.PlanoContaId, "Reembolso", true, "")).Wait();

                var investimentosNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, null, "Investimentos", true, "")).Result;
                var ganhosCapitalNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, investimentosNode.PlanoContaId, "Ganhos de capital", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, ganhosCapitalNode.PlanoContaId, "Dividendos", true, "")).Wait();
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Credito, investimentosNode.PlanoContaId, "Juros", true, "")).Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Erro no Plano de Conta: {0}", ex.Message));
            }            
        }

        private void CarregaContaPagar()
        {
            try
            {
                var service = new PlanoContaService();
                service.AplicacaoId = Guid.NewGuid();
                
                //Contas a Pagar
                var escritorioNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Escritório", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, escritorioNode.PlanoContaId, "Aluguel", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, escritorioNode.PlanoContaId, "Equipamentos", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, escritorioNode.PlanoContaId, "Jornais/Revistas", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, escritorioNode.PlanoContaId, "Móveis", true, ""));

                var financeiroNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Financeiro", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, financeiroNode.PlanoContaId, "Cobrança", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, financeiroNode.PlanoContaId, "Contabilidade", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, financeiroNode.PlanoContaId, "Juros", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, financeiroNode.PlanoContaId, "Saque", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, financeiroNode.PlanoContaId, "Tarifa Bancária", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, financeiroNode.PlanoContaId, "Transferência", true, ""));

                var impostosNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Impostos", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "COFINS", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "CPMF", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "CSLL", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "ICMS", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "INSS", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "IOF", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "IPTU", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "IRPJ", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "ISS", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "Importação", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, impostosNode.PlanoContaId, "PIS", true, ""));
                
                var manutencaoNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Manutenção", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, manutencaoNode.PlanoContaId, "Jardinagem", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, manutencaoNode.PlanoContaId, "Limpesa", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, manutencaoNode.PlanoContaId, "Pintura", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, manutencaoNode.PlanoContaId, "Reparos", true, ""));
                
                var outrasDespesasNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Outras Despesas", true, "")).Result;
                
                var pessoalNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Pessoal", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "13º Salário", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "Cesta Básica", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "FGTS", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "Férias", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "Horas Extras", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "Plano de Saúde", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "Salário", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, pessoalNode.PlanoContaId, "Vale Transporte", true, ""));

                var servicosPublicosNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Serviços Públicos", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Eletricidade", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Gás", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Internet", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Segurança", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Telefone Celular", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Telefone FIxo", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, servicosPublicosNode.PlanoContaId, "Água e Esgoto", true, ""));

                var suprimentosNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Suprimentos", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, suprimentosNode.PlanoContaId, "Despensa", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, suprimentosNode.PlanoContaId, "Impressora/Fax", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, suprimentosNode.PlanoContaId, "Limpesa/Higiene", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, suprimentosNode.PlanoContaId, "Papelaria", true, ""));

                var transporteNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Transporte", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, transporteNode.PlanoContaId, "Combustível", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, transporteNode.PlanoContaId, "Estacionamento", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, transporteNode.PlanoContaId, "Manutenção", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, transporteNode.PlanoContaId, "Multa", true, ""));

                var vendasNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Vendas", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, vendasNode.PlanoContaId, "Comissão", true, ""));

                var viagemNode = service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, null, "Viagem", true, "")).Result;
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, viagemNode.PlanoContaId, "Aluguel de Carro", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, viagemNode.PlanoContaId, "Combusível", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, viagemNode.PlanoContaId, "Hospedagem", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, viagemNode.PlanoContaId, "Ligações Telefônicas", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, viagemNode.PlanoContaId, "Passagens", true, ""));
                service.Insert(new PlanoContaDetailInsertDTO(TipoTransacao.Debito, viagemNode.PlanoContaId, "Restaurantes", true, ""));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Erro no Plano de Conta: {0}", ex.Message));
            }  
        }
    }
}
