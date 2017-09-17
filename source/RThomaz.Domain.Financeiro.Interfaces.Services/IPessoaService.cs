using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Domain.Core;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IPessoaService : IServiceBase
    {
        Task<PessoaFisicaDetailViewDTO> EditPessoaFisica(PessoaFisicaDetailEditDTO dto);
        Task<PessoaJuridicaDetailViewDTO> EditPessoaJuridica(PessoaJuridicaDetailEditDTO dto);
        Task<PagedListResponse<PessoaMasterDTO>> GetMasterList(PagedListRequest<PessoaMasterDTO> pagedListRequest, bool? ativo);
        Task<PessoaFisicaDetailViewDTO> GetPessoaFisicaDetail(long id);
        Task<PessoaJuridicaDetailViewDTO> GetPessoaJuridicaDetail(long id);
        Task<SelectListResponseDTO<PessoaSelectViewDTO>> GetSelectViewList(SelectListRequestDTO selectListDTORequest);
        Task<PessoaFisicaDetailViewDTO> InsertPessoaFisica(PessoaFisicaDetailInsertDTO dto);
        Task<PessoaJuridicaDetailViewDTO> InsertPessoaJuridica(PessoaJuridicaDetailInsertDTO dto);
        Task<bool> Remove(long pessoaId, TipoPessoa tipoPessoa);
    }
}