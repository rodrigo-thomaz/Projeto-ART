using RThomaz.Domain.Financeiro.Services;
using RThomaz.Domain.Financeiro.Services.DTOs;
using System;
using System.Diagnostics;

namespace RThomaz.DataManager.Managers
{
    public class ContaManager
    {
        public ContaManager(long bancoId)
        {
            System.Console.WriteLine("Cadastrando contas...");

            for (int i = 0; i < 100; i++)
            {   
                var numeroAgencia = "0356";
                var numeroConta = "01458-5";
                var saldoInicialData = DateTime.Now;
                decimal saldoInicialValor = decimal.Parse("15645.45");

                try
                {
                    var client = new ContaService();

                    client.AplicacaoId = Guid.NewGuid();

                    client.InsertContaCorrente(new ContaCorrenteDetailInsertDTO(bancoId, new DadoBancarioDTO(numeroAgencia, numeroConta), null, true, new SaldoInicialDTO(saldoInicialData, saldoInicialValor)));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Erro no bancoId: {0} | Mensagem: {1}", bancoId, ex.Message));
                }    
            }
        }
    }
}
