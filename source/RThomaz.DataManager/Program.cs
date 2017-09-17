using RThomaz.DataManager.Helpers;
using RThomaz.DataManager.Managers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace RThomaz.DataManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            Console.WriteLine("Deseja realmente reload de dados iniciais neste banco de dados? (precione 'S' para sim e 'N' para não):");
            Console.WriteLine();
            do
            {
                Console.Write("   ");
                line = Console.ReadLine();
                if (line.ToLower().Trim() == "s")
                {
                    var usuarioAdministratorId = DoWorkInitialSQLQuery();
                    DoWorkManagers();
                    return;
                }
            } while (line != null && line.ToLower().Trim() != "n");
        }

        private static string GetInitialSQLQuery()
        {
            var fullFileName = Path.Combine(DirectoryHelper.DefaultSampleDirectory, @"Scripts\", "InitialSQLQuery.sql");
            var result = File.ReadAllText(fullFileName);
            return result;
        }

        private static long DoWorkInitialSQLQuery()
        {
            System.Console.WriteLine("Removendo os dados existentes e inserindo no administrador...");

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var connection = new SqlConnection(connectionString);
            var command = connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = GetInitialSQLQuery();

            connection.Open();

            var usuarioAdministratorId = (long)command.ExecuteScalar();

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return usuarioAdministratorId;
        }

        private static void DoWorkManagers()
        {
            var bancoManager = new BancoManager();
            new CentroCustoManager();
            new PlanoContaManager();
            //var paisManager = new PaisManager();
            //new EstadoManager(paisManager.BrasilDetailContract);
            new ContaManager(bancoManager.DetailContractSample.BancoId);
            new PessoaManager();
            new UsuarioManager();
        }
    }
}
