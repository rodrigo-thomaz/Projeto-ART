using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IPessoaAppService : IAppServiceBase
    {
        Task<jQueryDataTableResult<List<PessoaMasterModel>>> GetMasterList(jQueryDataTableParameter param, bool? ativo);
        Task<PessoaFisicaDetailViewModel> EditPessoaFisica(PessoaFisicaDetailEditModel model);
        Task<PessoaJuridicaDetailViewModel> EditPessoaJuridica(PessoaJuridicaDetailEditModel model);
        Task<PessoaFisicaDetailViewModel> GetPessoaFisicaDetail(long id);
        Task<PessoaJuridicaDetailViewModel> GetPessoaJuridicaDetail(long id);
        Task<SelectListResponseModel<PessoaSelectViewModel>> GetSelectViewList(SelectListRequestModel selectListModelRequest);
        Task<PessoaFisicaDetailViewModel> InsertPessoaFisica(PessoaFisicaDetailInsertModel model);
        Task<PessoaJuridicaDetailViewModel> InsertPessoaJuridica(PessoaJuridicaDetailInsertModel model);
        Task<bool> Remove(long pessoaId, TipoPessoa tipoPessoa);
    }
}