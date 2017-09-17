using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ITipoEnderecoAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<TipoEnderecoMasterModel>>> GetMasterList(jQueryDataTableParameter param, TipoPessoa tipoPessoa, bool? ativo);
        Task<TipoEnderecoDetailViewModel> Edit(TipoEnderecoDetailEditModel model);
        Task<TipoEnderecoDetailViewModel> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<List<TipoEnderecoSelectViewModel>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoEnderecoDetailViewModel> Insert(TipoEnderecoDetailInsertModel model);
        Task<bool> Remove(Guid tipoEnderecoId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoEnderecoId, TipoPessoa tipoPessoa, string nome);
    }
}