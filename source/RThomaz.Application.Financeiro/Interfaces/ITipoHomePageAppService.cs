using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ITipoHomePageAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<TipoHomePageMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo);
        Task<TipoHomePageDetailViewModel> Edit(TipoHomePageDetailEditModel model);
        Task<TipoHomePageDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<List<TipoHomePageSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoHomePageDetailViewModel> Insert(TipoHomePageDetailInsertModel model);
        Task<bool> Remove(Guid tipoHomePageId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoHomePageId, TipoPessoa tipoPessoa, string nome);
    }
}