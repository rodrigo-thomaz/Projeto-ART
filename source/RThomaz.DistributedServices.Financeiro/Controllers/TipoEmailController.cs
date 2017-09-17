using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.DistributedServices.Core;
using System;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/tipoemail")]
    [Authorize]
    public class TipoEmailController : BaseApiController
    {
        #region private fields

        private readonly ITipoEmailAppService _tipoEmailAppService;

        #endregion

        #region constructors

        public TipoEmailController(ITipoEmailAppService tipoEmailAppService)
        {
            _tipoEmailAppService = tipoEmailAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipos de email
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de tipos de email 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="tipoPessoa">filtra por tipo de pessoa</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<TipoEmailMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, TipoPessoa tipoPessoa, [FromUri]bool? ativo)
        {
            var result = await _tipoEmailAppService.GetMasterList(param, tipoPessoa, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um tipo de email
        /// </summary>        
        /// <remarks>
        /// Retorna um único tipo de email existente por id
        /// </remarks>
        /// <param name="id">id do tipo de email</param>
        /// <param name="tipoPessoa">Tipo de pessoa do tipo de email</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TipoEmailDetailViewModel))]
        [Route("{id:Guid}/{tipoPessoa}")]
        public async Task<IHttpActionResult> Get(Guid id, TipoPessoa tipoPessoa)
        {
            var model = await _tipoEmailAppService.GetDetail(id, tipoPessoa);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de tipo de email
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipo de email para ser usada em listas de seleção
        /// </remarks>
        /// <param name="tipoPessoa">filtra o resultado por tipo de pessoa</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<TipoEmailSelectViewModel>))]
        [Route("selectViewList/{tipoPessoa}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            var models = await _tipoEmailAppService.GetSelectViewList(tipoPessoa);
            return Ok(models);
        }

        /// <summary>
        /// Verifica se o nome do tipo de email é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do tipo de email é único.
        /// 
        /// </remarks>        
        /// <param name="tipoPessoa">tipo pessoa do tipo de email</param>
        /// <param name="nome">nome do tipo de email para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("tipoPessoa/{tipoPessoa}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEmailAppService.UniqueNome(tipoPessoa, nome);
            return Ok(result);
        }

        /// <summary>
        /// Verifica se o nome do tipo de email é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do tipo de email é único.
        /// 
        /// </remarks>        
        /// <param name="id">id do tipo de email</param>
        /// <param name="tipoPessoa">tipo pessoa do tipo de email</param>
        /// <param name="nome">nome do tipo de email para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:Guid}/tipoPessoa/{tipoPessoa}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(Guid id, TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEmailAppService.UniqueNome(id, tipoPessoa, nome);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um tipo de email
        /// </summary>
        /// <remarks>
        /// Inseri um novo tipo de email
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do tipo de email</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TipoEmailDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(TipoEmailDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _tipoEmailAppService.Insert(model);
            //return CreatedAtRoute("", new { tipoEmailId = viewModel.TipoEmailId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um tipo de email
        /// </summary>
        /// <remarks>
        /// Atualiza um único tipo de email existente por id
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do tipo de email</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TipoEmailDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(TipoEmailDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _tipoEmailAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um tipo de email
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único tipo de email existente por id 
        /// Se o tipo de email já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do tipo de email</param>
        /// <param name="tipoPessoa">Tipo de pessoa: PessoaFisica = 0 | PessoaJuridica = 1</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}/{tipoPessoa}")]
        public async Task<IHttpActionResult> Delete(Guid id, TipoPessoa tipoPessoa)
        {
            var result = await _tipoEmailAppService.Remove(id, tipoPessoa);
            return Ok(result);
        }

        #endregion
    }
}