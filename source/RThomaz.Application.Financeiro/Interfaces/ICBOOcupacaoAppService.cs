using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface ICBOOcupacaoAppService
    {
        Task<SelectListResponseModel<CBOOcupacaoSelectViewModel>> GetOcupacaoSelectViewList(SelectListRequestModel selectListModelRequest);
        Task<SelectListResponseModel<CBOSinonimoSelectViewModel>> GetSinonimoSelectViewList(SelectListRequestModel selectListModelRequest, int cboOcupacaoId);
    }
}