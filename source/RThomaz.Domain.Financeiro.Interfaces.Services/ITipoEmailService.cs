using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Core;
using System;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ITipoEmailService : IServiceBase
    {
        Task<TipoEmailDetailViewDTO> Edit(TipoEmailDetailEditDTO dto);
        Task<TipoEmailDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<PagedListResponse<TipoEmailMasterDTO>> GetMasterList(PagedListRequest<TipoEmailMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo);
        Task<List<TipoEmailSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoEmailDetailViewDTO> Insert(TipoEmailDetailInsertDTO dto);
        Task<bool> Remove(Guid tipoEmailId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoEmailId, TipoPessoa tipoPessoa, string nome);
    }
}
