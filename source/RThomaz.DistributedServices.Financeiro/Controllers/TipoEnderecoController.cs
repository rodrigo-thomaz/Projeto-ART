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
    [RoutePrefix("api/tipoendereco")]
    [Authorize]
    public class TipoEnderecoController : BaseApiController
    {
        #region private readonly fields

        private readonly ITipoEnderecoAppService _tipoEnderecoAppService;

        #endregion

        #region constructors

        public TipoEnderecoController(ITipoEnderecoAppService tipoEnderecoAppService)
        {
            _tipoEnderecoAppService = tipoEnderecoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipos de endereço
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de tipos de endereço 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="tipoPessoa">filtra por tipo de pessoa</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<TipoEnderecoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, TipoPessoa tipoPessoa, [FromUri]bool? ativo)
        {
            var result = await _tipoEnderecoAppService.GetMasterList(param, tipoPessoa, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um tipo de endereço
        /// </summary>        
        /// <remarks>
        /// Retorna um único tipo de endereço existente por id
        /// </remarks>
        /// <param name="id">id do tipo de endereço</param>
        /// <param name="tipoPessoa">Tipo de pessoa do tipo de endereço</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TipoEnderecoDetailViewModel))]
        [Route("{id:Guid}/{tipoPessoa}")]
        public async Task<IHttpActionResult> Get(Guid id, TipoPessoa tipoPessoa)
        {
            var model = await _tipoEnderecoAppService.GetDetail(id, tipoPessoa);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de tipo de endereço
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipo de endereço para ser usada em listas de seleção
        /// </remarks>
        /// <param name="tipoPessoa">filtra o resultado por tipo de pessoa</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<TipoEnderecoSelectViewModel>))]
        [Route("selectViewList/{tipoPessoa}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList(TipoPessoa tipoPessoa)
        {
            var models = await _tipoEnderecoAppService.GetSelectViewList(tipoPessoa);
            return Ok(models);
        }

        /// <summary>
        /// Verifica se o nome do tipo de endereço é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do tipo de endereço é único.
        /// 
        /// </remarks>        
        /// <param name="tipoPessoa">tipo pessoa do tipo de endereço</param>
        /// <param name="nome">nome do tipo de endereço para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("tipoPessoa/{tipoPessoa}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEnderecoAppService.UniqueNome(tipoPessoa, nome);
            return Ok(result);
        }

        /// <summary>
        /// Verifica se o nome do tipo de endereço é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do tipo de endereço é único.
        /// 
        /// </remarks>        
        /// <param name="id">id do tipo de endereço</param>
        /// <param name="tipoPessoa">tipo pessoa do tipo de endereço</param>
        /// <param name="nome">nome do tipo de endereço para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:Guid}/tipoPessoa/{tipoPessoa}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(Guid id, TipoPessoa tipoPessoa, string nome)
        {
            var result = await _tipoEnderecoAppService.UniqueNome(id, tipoPessoa, nome);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um tipo de endereço
        /// </summary>
        /// <remarks>
        /// Inseri um novo tipo de endereço
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do tipo de endereço</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TipoEnderecoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(TipoEnderecoDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _tipoEnderecoAppService.Insert(model);
            //return CreatedAtRoute("", new { tipoEnderecoId = viewModel.TipoEnderecoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um tipo de endereço
        /// </summary>
        /// <remarks>
        /// Atualiza um único tipo de endereço existente por id
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="model">model do tipo de endereço</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TipoEnderecoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(TipoEnderecoDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _tipoEnderecoAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um tipo de endereço
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único tipo de endereço existente por id 
        /// Se o tipo de endereço já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do tipo de endereço</param>
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
            var result = await _tipoEnderecoAppService.Remove(id, tipoPessoa);
            return Ok(result);
        }       

        #endregion
    }
}