using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs.Enums;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IMovimentoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<MovimentoMasterModel>>> GetMasterList(jQueryDataTableParameter param, MesAnoModel periodo, long contaId, TipoConta tipoConta, TipoTransacao? tipoTransacao, ConciliacaoStatus? conciliacaoStatus, ConciliacaoOrigem? conciliacaoOrigem);
        Task<SelectListResponseModel<MovimentoSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest, TipoTransacao tipoTransacao);
        Task<MovimentoDetailViewModel> GetDetail(long movimentoId);
        Task<List<MesAnoModel>> GetPeriodos(long contaId, TipoConta tipoConta);
        Task<MovimentoDetailViewModel> Insert(MovimentoDetailInsertModel model);
        Task<MovimentoDetailViewModel> EditManual(MovimentoManualEditModel model);
        Task<MovimentoDetailViewModel> EditImportado(MovimentoImportadoEditModel model);
        Task<MovimentoDetailViewModel> EditConciliado(MovimentoConciliadoEditModel model);
        Task<bool> Remove(long movimentoId);
    }
}