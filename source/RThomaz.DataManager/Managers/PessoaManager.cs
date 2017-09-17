using RThomaz.Domain.Financeiro.Services;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.DataManager.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.DataManager.Managers
{
    public class PessoaManager
    {
        public PessoaManager()
        {
            System.Console.WriteLine("Cadastrando pessoas...");

            var nomeFiles = Directory.GetFiles(DirectoryHelper.NomesSampleDirectory);
            var sobrenomeFiles = Directory.GetFiles(DirectoryHelper.SobrenomesSampleDirectory);

            foreach (var nomeFile in nomeFiles)
            {
                foreach (var sobrenomeFile in sobrenomeFiles)
                {
                    var nomeLines = File.ReadAllLines(nomeFile, DirectoryHelper.Encoding);
                    var sobrenomeLines = File.ReadAllLines(sobrenomeFile, DirectoryHelper.Encoding);

                    foreach (var nomeLine in nomeLines)
                    {
                        foreach (var sobrenomeLine in sobrenomeLines)
                        {
                            var nome = nomeLine.Trim();
                            var sobrenome = sobrenomeLine.Trim();

                            try
                            {
                                var client = new PessoaService();
                                client.AplicacaoId = Guid.NewGuid();                                
                                client.InsertPessoaFisica(new PessoaFisicaDetailInsertDTO(0, nome, null, sobrenome, Sexo.Masculino, null, "12345678910", null, null, null, null, null, null, null, true, "", null, null, null, null));
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(string.Format("Erro no nome: {0} {1} | Mensagem: {2}", nome, sobrenome, ex.Message));
                            }
                        }
                    }
                }
            }
        }
    }
}
