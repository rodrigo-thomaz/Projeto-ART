using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;
using System;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ITipoEnderecoService : IServiceBase
    {
        Task<TipoEnderecoDetailViewDTO> Edit(TipoEnderecoDetailEditDTO dto);
        Task<TipoEnderecoDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<PagedListResponse<TipoEnderecoMasterDTO>> GetMasterList(PagedListRequest<TipoEnderecoMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo);
        Task<List<TipoEnderecoSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoEnderecoDetailViewDTO> Insert(TipoEnderecoDetailInsertDTO dto);
        Task<bool> Remove(Guid tipoEnderecoId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoEnderecoId, TipoPessoa tipoPessoa, string nome);
    }
}