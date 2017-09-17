using System.Threading.Tasks;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ITipoEmailAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<TipoEmailMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo);
        Task<TipoEmailDetailViewModel> Edit(TipoEmailDetailEditModel model);
        Task<TipoEmailDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<List<TipoEmailSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoEmailDetailViewModel> Insert(TipoEmailDetailInsertModel model);
        Task<bool> Remove(Guid tipoEmailId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoEmailId, TipoPessoa tipoPessoa, string nome);
    }
}