using RThomaz.Domain.Financeiro.Services;
using RThomaz.Domain.Financeiro.Services.DTOs;
using System;
using System.Diagnostics;

namespace RThomaz.DataManager.Managers
{
    public class CentroCustoManager
    {
        public CentroCustoManager()
        {
            System.Console.WriteLine("Cadastrando centros de custo...");

            long? responsavelId = null;

            try
            {
                var service = new CentroCustoService();

                service.AplicacaoId = Guid.NewGuid();
                
                var administracaoNode = service.Insert(new CentroCustoDetailInsertDTO(null, responsavelId, "Administração", true, "")).Result;
                service.Insert(new CentroCustoDetailInsertDTO(administracaoNode.CentroCustoId, responsavelId, "Departamento Financeiro", true, "")).Wait();
                service.Insert(new CentroCustoDetailInsertDTO(administracaoNode.CentroCustoId, responsavelId, "Recursos Humanos", true, "")).Wait();

                var comercialNode = service.Insert(new CentroCustoDetailInsertDTO(null, responsavelId, "Departamento Comercial", true, "")).Result;
                service.Insert(new CentroCustoDetailInsertDTO(comercialNode.CentroCustoId, responsavelId, "Marketing", true, "")).Wait();
                service.Insert(new CentroCustoDetailInsertDTO(comercialNode.CentroCustoId, responsavelId, "Vendas", true, "")).Wait();

                var tecnicoNode = service.Insert(new CentroCustoDetailInsertDTO(null, responsavelId, "Departamento Técnico", true, "")).Result;
                service.Insert(new CentroCustoDetailInsertDTO(tecnicoNode.CentroCustoId, responsavelId, "Desenvolvimento", true, "")).Wait();
                service.Insert(new CentroCustoDetailInsertDTO(tecnicoNode.CentroCustoId, responsavelId, "Suporte Técnico", true, "")).Wait(); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Erro no CentroCusto: {0}", ex.Message));
            }        
        }
    }
}
