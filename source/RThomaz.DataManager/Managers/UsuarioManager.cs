using RThomaz.Domain.Financeiro.Services;
using RThomaz.DataManager.Helpers;
using System;
using System.IO;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.DataManager.Managers
{
    public class UsuarioManager
    {
        public UsuarioManager()
        {
            System.Console.WriteLine("Cadastrando usuários...");

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
                            var nomeCompleto = string.Format("{0} {1}", nome, sobrenome);
                            var email = string.Format("{0}{1}@rthomaz.com.br"
                                , PrimitiveHelper.GetStringNoAccents(nome).ToLower()
                                , PrimitiveHelper.GetStringNoAccents(sobrenome).ToLower()).Replace(' ', '_');
                            var senha = "12345";

                            try
                            {
                                var client = new UsuarioService();
                                client.AplicacaoId = Guid.NewGuid();
                                client.StorageBucketName = "rthomaz-client-49d2d654-ee86-47df-8db4-910c3cf89708";

                                client.Insert(new UsuarioDetailInsertDTO(nomeCompleto, email, senha, true, ""));
                            }
                            catch (Exception ex)
                            {
                                var message = ex.Message;
                            }

                            //Debug.WriteLine(string.Format("{0} ({1})", nomeCompleto, email));
                        }
                    }
                }
            }
        }
    }
}
