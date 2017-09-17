using System.Threading.Tasks;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.SelectListDTO;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface ICBOOcupacaoService 
    {
        Task<SelectListResponseDTO<CBOOcupacaoSelectViewDTO>> GetOcupacaoSelectViewList(SelectListRequestDTO selectListDTORequest);
        Task<SelectListResponseDTO<CBOSinonimoSelectViewDTO>> GetSinonimoSelectViewList(SelectListRequestDTO selectListDTORequest, int cboOcupacaoId);
    }
}