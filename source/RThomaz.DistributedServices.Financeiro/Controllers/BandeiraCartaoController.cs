using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/bandeiracartao")]
    [Authorize]
    public class BandeiraCartaoController :  BaseApiController
    {
        #region private readonly fields

        private readonly IBandeiraCartaoAppService _bandeiraCartaoAppService;

        #endregion

        #region constructors

        public BandeiraCartaoController(IBandeiraCartaoAppService bandeiraCartaoAppService)
        {
            _bandeiraCartaoAppService = bandeiraCartaoAppService;
        }

        #endregion

        #region public voids      

        /// <summary>
        /// Retornar uma lista de bandeiras de cartão de crédito
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de bandeiras de cartão de crédito 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<BandeiraCartaoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]bool? ativo)
        {
            var result = await _bandeiraCartaoAppService.GetMasterList(param, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma bandeira de cartão de crédito
        /// </summary>        
        /// <remarks>
        /// Retorna uma única bandeira de cartão de crédito existente por id
        /// </remarks>
        /// <param name="id">id da bandeira de cartão de crédito</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BandeiraCartaoDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _bandeiraCartaoAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar o logo de uma bandeira de cartão de crédito
        /// </summary>        
        /// <remarks>
        /// Retorna o logo de uma única bandeira de cartão de crédito existente por id
        /// </remarks>
        /// <param name="id">id do logo</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("logo/{id}")]
        public async Task<HttpResponseMessage> GetLogo(string id)
        {
            var dto = await _bandeiraCartaoAppService.GetLogo(id);
            var result = new StorageHttpResponseMessage(dto);
            return result;
        }

        /// <summary>
        /// Retornar uma lista de bandeiras de cartão de crédito
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de bandeiras de cartão de crédito para ser usada em listas de seleção
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<BandeiraCartaoSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList()
        {
            var selectListModel = await _bandeiraCartaoAppService.GetSelectViewList();
            return Ok(selectListModel);
        }        

        /// <summary>
        /// Verifica se o nome da bandeira do cartão de crédito é única
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome da bandeira do cartão de crédito é única.
        /// 
        /// Para cadastro de uma nova bandeira do cartão de crédito, informe id = 0
        /// 
        /// </remarks>        
        /// <param name="id">id da bandeira do cartão de crédito</param>
        /// <param name="nome">nome da bandeira do cartão de crédito para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:long}/uniqueNome/{nome:custom_length(1,250)}")]
        public async Task<IHttpActionResult> UniqueNome(long id, string nome)
        {
            var result = await _bandeiraCartaoAppService.UniqueNome(id.Equals(0) ? (long?)null : id, nome);
            return Ok(result);
        }

        /// <summary>
        /// Inserir uma bandeira de cartão de crédito
        /// </summary>
        /// <remarks>
        /// Inseri uma nova bandeira de cartão de crédito
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model da bandeira de cartão de crédito</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BandeiraCartaoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(BandeiraCartaoDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _bandeiraCartaoAppService.Insert(model);
            //return CreatedAtRoute("", new { bandeiraCartaoId = viewModel.BandeiraCartaoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar uma bandeira de cartão de crédito
        /// </summary>
        /// <remarks>
        /// Atualiza uma única bandeira de cartão de crédito existente por id
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model da bandeira de cartão de crédito</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BandeiraCartaoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(BandeiraCartaoDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _bandeiraCartaoAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover uma bandeira de cartão de crédito
        /// </summary>        
        /// <remarks>
        /// Tenta remover uma única bandeira de cartão de crédito existente por id 
        /// Se a bandeira de cartão de crédito já estiver sendo utilizada pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id da bandeira de cartão de crédito</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _bandeiraCartaoAppService.Remove(id);
            return Ok(result);
        }

        #endregion
    }
}