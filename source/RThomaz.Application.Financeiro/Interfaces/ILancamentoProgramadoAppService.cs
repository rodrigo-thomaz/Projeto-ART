using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ILancamentoProgramadoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<LancamentoProgramadoMasterModel>>> GetMasterList(jQueryDataTableParameter param, long? contaId, TipoConta? tipoConta);
        Task<LancamentoProgramadoDetailViewModel> GetDetail(long programacaoId);
        Task<LancamentoProgramadoDetailViewModel> Insert(LancamentoProgramadoDetailUpdateModel model);
        Task<LancamentoProgramadoDetailViewModel> Edit(LancamentoProgramadoDetailUpdateModel model);
        Task<bool> Remove(long id);
    }
}