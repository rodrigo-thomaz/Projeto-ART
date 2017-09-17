using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System.Collections.Generic;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/banco")]
    [Authorize]
    public class BancoController : BaseApiController
    {
        #region private readonly fields

        private readonly IBancoAppService _bancoAppService;

        #endregion

        #region constructors

        public BancoController(IBancoAppService bancoAppService)
        {
            _bancoAppService = bancoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de bancos
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de bancos 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<BancoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]bool? ativo)
        {
            var result = await _bancoAppService.GetMasterList(param, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um banco
        /// </summary>        
        /// <remarks>
        /// Retorna um único banco existente por id
        /// </remarks>
        /// <param name="id">id do banco</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BancoDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _bancoAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar o logo de um banco
        /// </summary>        
        /// <remarks>
        /// Retorna o logo de um único banco existente por id
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
            var dto = await _bancoAppService.GetLogo(id);
            var result = new StorageHttpResponseMessage(dto);
            return result;
        }       

        /// <summary>
        /// Retornar as máscaras bancária de um banco
        /// </summary>        
        /// <remarks>
        /// Retornar as máscaras bancária de um único banco existente por id
        /// </remarks>
        /// <param name="id">id do banco</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BancoMascarasDetailViewModel))]
        [Route("{id:long:custom_min(1)}/mascaras")]
        public async Task<IHttpActionResult> GetMascaras(long id)
        {
            var model = await _bancoAppService.GetMascaras(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de bancos
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de bancos para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<BancoSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param)
        {
            ValidateModelState();            
            var response = await _bancoAppService.GetSelectViewList(param);
            return Ok(response);
        }

        /// <summary>
        /// Verifica se o nome do banco é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do banco é único.
        /// 
        /// Para cadastro de um novo banco, informe id = 0
        /// 
        /// </remarks>        
        /// <param name="id">id do banco</param>
        /// <param name="nome">nome do banco para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:long}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(long id, string nome)
        {
            var result = await _bancoAppService.UniqueNome(id.Equals(0) ? (long?)null : id, nome);
            return Ok(result);
        }

        /// <summary>
        /// Verifica se o nome abreviado do banco é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome abreviado do banco é único.
        /// 
        /// Para cadastro de um novo banco, informe id = 0
        /// 
        /// </remarks>        
        /// <param name="id">id do banco</param>
        /// <param name="nomeAbreviado">Nome abreviado do banco para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:long}/uniqueNomeAbreviado/{nomeAbreviado:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNomeAbreviado(long id, string nomeAbreviado)
        {
            var result = await _bancoAppService.UniqueNomeAbreviado(id.Equals(0) ? (long?)null : id, nomeAbreviado);
            return Ok(result);
        }

        /// <summary>
        /// Verifica se o número do banco é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o número do banco é único.
        /// 
        /// Para cadastro de um novo banco, informe id = 0
        /// 
        /// </remarks>        
        /// <param name="id">id do banco</param>
        /// <param name="numero">Número do banco para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:long}/uniqueNumero/{numero:custom_length(1,15)}")]
        public async Task<IHttpActionResult> UniqueNumero(long id, string numero)
        {
            var result = await _bancoAppService.UniqueNumero(id.Equals(0) ? (long?)null : id, numero);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um banco
        /// </summary>
        /// <remarks>
        /// Inseri um novo banco
        /// 
        /// - O campo 'nome' deve ser único;
        /// - O campo 'nomeAbreviado' deve ser único;
        /// - O campo 'numero' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do banco</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BancoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(BancoDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _bancoAppService.Insert(model);            
            //return CreatedAtRoute("", new { id = viewModel.BancoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um banco
        /// </summary>
        /// <remarks>
        /// Atualiza um único banco existente por id
        /// 
        /// - O campo 'nome' deve ser único;
        /// - O campo 'nomeAbreviado' deve ser único;
        /// - O campo 'numero' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do banco</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BancoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(BancoDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _bancoAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um banco
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único banco existente por id 
        /// Se o banco já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do banco</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _bancoAppService.Remove(id);
            return Ok(result);
        }

        #endregion
    }
}
