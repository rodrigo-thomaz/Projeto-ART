using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System.Collections.Generic;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/usuario")]
    [Authorize]
    public class UsuarioController : BaseApiController
    {
        #region private readonly fields

        private readonly IUsuarioAppService _usuarioAppService;

        #endregion

        #region constructors

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de usuários
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de usuários 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<UsuarioMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]bool? ativo)
        {
            var result = await _usuarioAppService.GetMasterList(param, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um usuário
        /// </summary>        
        /// <remarks>
        /// Retorna um único usuário existente por id
        /// </remarks>
        /// <param name="id">id do usuário</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(UsuarioDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _usuarioAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de usuários
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de usuários para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<UsuarioSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param)
        {
            ValidateModelState();
            var selectListModelResponse = await _usuarioAppService.GetSelectViewList(param);
            return Ok(selectListModelResponse);
        }

        /// <summary>
        /// Verifica se o email do usuário é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o email do usuário é único.        
        /// 
        /// </remarks>        
        /// <param name="email">email do usuário para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("uniqueEmail/{email:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueEmail(string email)
        {
            var result = await _usuarioAppService.UniqueEmail(email);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um usuário
        /// </summary>
        /// <remarks>
        /// Inseri um novo usuário
        /// 
        /// - O campo 'email' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do usuário</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(UsuarioDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(UsuarioDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _usuarioAppService.Insert(model);
            //return CreatedAtRoute("", new { usuarioId = viewModel.UsuarioId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um usuário
        /// </summary>
        /// <remarks>
        /// Atualiza um único usuário existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do usuário</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(UsuarioDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(UsuarioDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _usuarioAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um usuário
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único usuário existente por id 
        /// Se o usuário já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do usuário</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _usuarioAppService.Remove(id);
            return Ok(result);
        }

        #endregion
    }
}