using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;
using System;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ITipoTelefoneService : IServiceBase
    {
        Task<TipoTelefoneDetailViewDTO> Edit(TipoTelefoneDetailEditDTO dto);
        Task<TipoTelefoneDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<PagedListResponse<TipoTelefoneMasterDTO>> GetMasterList(PagedListRequest<TipoTelefoneMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo);
        Task<List<TipoTelefoneSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoTelefoneDetailViewDTO> Insert(TipoTelefoneDetailInsertDTO dto);
        Task<bool> Remove(Guid tipoTelefoneId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoTelefoneId, TipoPessoa tipoPessoa, string nome);
    }
}