using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Helpers.JsTree;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/planoconta")]
    [Authorize]
    public class PlanoContaController : BaseApiController
    {
        #region private readonly fields

        private readonly IPlanoContaAppService _planoContaAppService;

        #endregion

        #region constructors

        public PlanoContaController(IPlanoContaAppService planoContaAppService)
        {
            _planoContaAppService = planoContaAppService;
        }

        #endregion

        #region public voids      

        /// <summary>
        /// Retornar uma lista de planos de conta
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de planos de conta 
        /// </remarks>
        /// <param name="tipoTransacao">filtro por tipo de transação</param>
        /// <param name="search">filtro para pesquisa</param>
        /// <param name="mostrarInativos">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<JsTreeNode>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList(TipoTransacao tipoTransacao, string search, bool mostrarInativos)
        {
            var result = await _planoContaAppService.GetMasterList(tipoTransacao, search, mostrarInativos);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um plano de conta
        /// </summary>        
        /// <remarks>
        /// Retorna um único plano de conta existente por id
        /// </remarks>
        /// <param name="id">id do plano de conta</param>
        /// <param name="tipoTransacao">tipo de transação do plano de conta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PlanoContaDetailViewModel))]
        [Route("{id:long:custom_min(1)}/tipotransacao/{tipoTransacao}")]
        public async Task<IHttpActionResult> Get(long id, TipoTransacao tipoTransacao)
        {
            var model = await _planoContaAppService.GetDetail(id, tipoTransacao);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de planos de conta
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de planos de conta para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="tipoTransacao">tipo de transação dos planos de conta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<PlanoContaSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param, TipoTransacao tipoTransacao)
        {
            ValidateModelState();
            var selectListModelResponse = await _planoContaAppService.GetSelectViewList(param, tipoTransacao);
            return Ok(selectListModelResponse);
        }

        /// <summary>
        /// Mover uma lista de planos de conta
        /// </summary>
        /// <remarks>
        /// Mover uma lista de planos de conta existentes por id
        /// 
        /// </remarks>
        /// <param name="model">model do plano de conta</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("move")]
        public async Task<IHttpActionResult> Move(PlanoContaMasterMoveModel model)
        {
            await _planoContaAppService.Move(model);
            return Ok();
        }

        /// <summary>
        /// Renomear um plano de conta
        /// </summary>
        /// <remarks>
        /// Renomear um plano de conta existente por id
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="planoContaId">id do plano de conta</param>        
        /// <param name="nome">nome do plano de conta</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("rename")]        
        public async Task<IHttpActionResult> Rename(long planoContaId, string nome)
        {
            await _planoContaAppService.Rename(planoContaId, nome);
            return Ok();
        }

        /// <summary>
        /// Verifica se o nome do plano de conta é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do plano de conta é único.
        /// 
        /// </remarks>        
        /// <param name="tipoTransacao">tipo da transação do plano de conta</param>
        /// <param name="nome">nome do plano de conta para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{tipoTransacao}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(TipoTransacao tipoTransacao, string nome)
        {
            var result = await _planoContaAppService.UniqueNome(tipoTransacao, nome);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um plano de conta
        /// </summary>
        /// <remarks>
        /// Inseri um novo plano de conta
        /// 
        /// </remarks>
        /// <param name="model">model do plano de conta</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PlanoContaDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(PlanoContaDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _planoContaAppService.Insert(model);
            //return CreatedAtRoute("", new { planoContaId = viewModel.PlanoContaId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um plano de conta
        /// </summary>
        /// <remarks>
        /// Atualiza um único plano de conta existente por id        
        /// 
        /// </remarks>
        /// <param name="model">model do plano de conta</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PlanoContaDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(PlanoContaDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _planoContaAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um plano de conta
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único plano de conta existente por id 
        /// Se o plano de conta já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do plano de conta</param>
        /// <param name="tipoTransacao">tipo de transação: Credito = 0 | Debito = 1</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}/{tipoTransacao}")]
        public async Task<IHttpActionResult> Delete(long id, TipoTransacao tipoTransacao)
        {
            var result = await _planoContaAppService.Remove(id, tipoTransacao);
            return Ok(result);
        }

        #endregion
    }
}