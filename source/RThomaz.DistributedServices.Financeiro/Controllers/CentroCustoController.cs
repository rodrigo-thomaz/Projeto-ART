using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.JsTree;
using System.Collections.Generic;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/centrocusto")]
    [Authorize]
    public class CentroCustoController : BaseApiController
    {
        #region private readonly fields

        private readonly ICentroCustoAppService _centroCustoAppService;

        #endregion

        #region constructors

        public CentroCustoController(ICentroCustoAppService centroCustoAppService)
        {
            _centroCustoAppService = centroCustoAppService;
        }

        #endregion

        #region public voids      

        /// <summary>
        /// Retornar uma lista de centros de custo
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de centros de custo 
        /// </remarks>
        /// <param name="search">filtro para pesquisa</param>
        /// <param name="mostrarInativos">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<JsTreeNode>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList(string search, bool mostrarInativos)
        {
            var result = await _centroCustoAppService.GetMasterList(search, mostrarInativos);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um centro de custo
        /// </summary>        
        /// <remarks>
        /// Retorna um único centro de custo existente por id
        /// </remarks>
        /// <param name="id">id do centro de custo</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(CentroCustoDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _centroCustoAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de centros de custo
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de centros de custo para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<CentroCustoSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param)
        {
            ValidateModelState();
            var selectListModelResponse = await _centroCustoAppService.GetSelectViewList(param);
            return Ok(selectListModelResponse);
        }

        /// <summary>
        /// Mover uma lista de centros de custo
        /// </summary>
        /// <remarks>
        /// Mover uma lista de centros de custo existentes por id
        /// 
        /// </remarks>
        /// <param name="model">model do centro de custo</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("move")]
        public async Task<IHttpActionResult> Move(CentroCustoMasterMoveModel model)
        {
            await _centroCustoAppService.Move(model);
            return Ok();
        }

        /// <summary>
        /// Renomear um centro de custo
        /// </summary>
        /// <remarks>
        /// Renomear um centro de custo existente por id
        /// 
        /// - O campo 'nome' deve ser único;
        /// 
        /// </remarks>
        /// <param name="centroCustoId">id do centro de custo</param>        
        /// <param name="nome">nome do centro de custo</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("rename")]
        public async Task<IHttpActionResult> Rename(long centroCustoId, string nome)
        {
            await _centroCustoAppService.Rename(centroCustoId, nome);
            return Ok();
        }

        /// <summary>
        /// Verifica se o nome do centro de custo é único
        /// </summary>        
        /// <remarks>
        /// Verifica se o nome do centro de custo é único.
        /// 
        /// Para cadastro de um novo centro de custo, informe id = 0
        /// 
        /// </remarks>        
        /// <param name="id">id do centro de custo</param>
        /// <param name="nome">nome do centro de custo para pesquisar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [HttpGet]
        [Route("{id:long}/uniqueNome/{nome:custom_length(1,255)}")]
        public async Task<IHttpActionResult> UniqueNome(long id, string nome)
        {            
            var result = await _centroCustoAppService.UniqueNome(id.Equals(0) ? (long?)null : id, nome);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um centro de custo
        /// </summary>
        /// <remarks>
        /// Inseri um novo centro de custo        
        /// 
        /// </remarks>
        /// <param name="model">model do centro de custo</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(CentroCustoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(CentroCustoDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _centroCustoAppService.Insert(model);
            //return CreatedAtRoute("", new { centroCustoId = viewModel.CentroCustoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um centro de custo
        /// </summary>
        /// <remarks>
        /// Atualiza um único centro de custo existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do centro de custo</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(CentroCustoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(CentroCustoDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _centroCustoAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um centro de custo
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único centro de custo existente por id 
        /// Se o centro de custo já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do centro de custo</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _centroCustoAppService.Remove(id);
            return Ok(result);
        }

        #endregion
    }
}