using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ITipoTelefoneAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<TipoTelefoneMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo);
        Task<TipoTelefoneDetailViewModel> Edit(TipoTelefoneDetailEditModel model);
        Task<TipoTelefoneDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<List<TipoTelefoneSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoTelefoneDetailViewModel> Insert(TipoTelefoneDetailInsertModel model);
        Task<bool> Remove(Guid tipoTelefoneId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoTelefoneId, TipoPessoa tipoPessoa, string nome);
    }
}