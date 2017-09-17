using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/pessoa")]
    [Authorize]
    public class PessoaController : BaseApiController
    {
        #region private readonly fields

        private readonly IPessoaAppService _pessoaAppService;

        #endregion

        #region constructors

        public PessoaController(IPessoaAppService pessoaAppService)
        {
            _pessoaAppService = pessoaAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de pessoas
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de pessoas 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<PessoaMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]bool? ativo)
        {
            var result = await _pessoaAppService.GetMasterList(param, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma pessoa física
        /// </summary>        
        /// <remarks>
        /// Retorna uma única pessoa física existente por id
        /// </remarks>
        /// <param name="id">id da pessoa física</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PessoaFisicaDetailViewModel))]
        [Route("fisica/{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> GetPessoaFisicaDetail(long id)
        {
            var model = await _pessoaAppService.GetPessoaFisicaDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma pessoa jurídica
        /// </summary>        
        /// <remarks>
        /// Retorna uma única pessoa jurídica existente por id
        /// </remarks>
        /// <param name="id">id da pessoa jurídica</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PessoaJuridicaDetailViewModel))]
        [Route("juridica/{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> GetPessoaJuridicaDetail(long id)
        {
            var model = await _pessoaAppService.GetPessoaJuridicaDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de pessoas
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de pessoas para ser usada em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<PessoaSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param)
        {
            ValidateModelState();
            var selectListModelResponse = await _pessoaAppService.GetSelectViewList(param);
            return Ok(selectListModelResponse);
        }

        /// <summary>
        /// Inserir uma pessoa física
        /// </summary>
        /// <remarks>
        /// Inseri uma nova pessoa física
        /// 
        /// </remarks>
        /// <param name="model">model da pessoa física</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PessoaFisicaDetailViewModel))]
        [Route("fisica")]
        public async Task<IHttpActionResult> PostPessoaFisica(PessoaFisicaDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _pessoaAppService.InsertPessoaFisica(model);
            //return CreatedAtRoute("", new { pessoaId = viewModel.PessoaId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Inserir uma pessoa jurídica
        /// </summary>
        /// <remarks>
        /// Inseri uma nova pessoa jurídica
        /// 
        /// </remarks>
        /// <param name="model">model da pessoa jurídica</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PessoaJuridicaDetailViewModel))]
        [Route("juridica")]
        public async Task<IHttpActionResult> PostPessoaJuridica(PessoaJuridicaDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _pessoaAppService.InsertPessoaJuridica(model);
            //return CreatedAtRoute("", new { pessoaId = viewModel.PessoaId }, viewModel);
            return Ok(viewModel);
        }
        
        /// <summary>
        /// Atualizar uma pessoa física
        /// </summary>
        /// <remarks>
        /// Atualiza uma única pessoa física existente por id        
        /// </remarks>
        /// <param name="model">model da pessoa física</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PessoaFisicaDetailViewModel))]
        [Route("fisica")]
        public async Task<IHttpActionResult> PutPessoaFisica(PessoaFisicaDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _pessoaAppService.EditPessoaFisica(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar uma pessoa jurídica
        /// </summary>
        /// <remarks>
        /// Atualiza uma única pessoa jurídica existente por id        
        /// </remarks>
        /// <param name="model">model da pessoa jurídica</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PessoaJuridicaDetailViewModel))]
        [Route("juridica")]
        public async Task<IHttpActionResult> PutPessoaJuridica(PessoaJuridicaDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _pessoaAppService.EditPessoaJuridica(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover uma pessoa
        /// </summary>        
        /// <remarks>
        /// Tenta remover uma única pessoa existente por id 
        /// Se a pessoa já estiver sendo utilizada pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id da pessoa</param>
        /// <param name="tipoPessoa">Tipo de pessoa: PessoaFisica = 0 | PessoaJuridica = 1</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}/{tipoPessoa}")]
        public async Task<IHttpActionResult> Delete(long id, TipoPessoa tipoPessoa)
        {
            var result = await _pessoaAppService.Remove(id, tipoPessoa);
            return Ok(result);
        }

        #endregion
    }
}