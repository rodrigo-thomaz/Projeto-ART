using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ILancamentoPagoRecebidoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<LancamentoMasterModel>>> GetMasterList(jQueryDataTableParameter param, MesAnoModel periodo, long contaId, TipoConta tipoConta);
        Task<LancamentoPagoRecebidoDetailViewModel> Edit(LancamentoPagoRecebidoDetailEditModel model);
        Task<LancamentoPagoRecebidoDetailViewModel> GetDetail(long lancamentoId);
        Task<List<MesAnoModel>> GetPeriodos(long contaId, TipoConta tipoConta);
        Task<bool> Remove(long lancamentoId, TipoTransacao tipoTransacao);
    }
}