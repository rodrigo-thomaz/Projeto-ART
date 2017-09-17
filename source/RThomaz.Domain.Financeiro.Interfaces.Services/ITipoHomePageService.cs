using System.Collections.Generic;
using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;
using System;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ITipoHomePageService : IServiceBase
    {
        Task<TipoHomePageDetailViewDTO> Edit(TipoHomePageDetailEditDTO dto);
        Task<TipoHomePageDetailViewDTO> GetDetail(Guid id, TipoPessoa tipoPessoa);
        Task<PagedListResponse<TipoHomePageMasterDTO>> GetMasterList(PagedListRequest<TipoHomePageMasterDTO> pagedListRequest, TipoPessoa tipoPessoa, bool? ativo);
        Task<List<TipoHomePageSelectViewDTO>> GetSelectViewList(TipoPessoa tipoPessoa);
        Task<TipoHomePageDetailViewDTO> Insert(TipoHomePageDetailInsertDTO dto);
        Task<bool> Remove(Guid tipoHomePageId, TipoPessoa tipoPessoa);
        Task<bool> UniqueNome(TipoPessoa tipoPessoa, string nome);
        Task<bool> UniqueNome(Guid tipoHomePageId, TipoPessoa tipoPessoa, string nome);
    }
}