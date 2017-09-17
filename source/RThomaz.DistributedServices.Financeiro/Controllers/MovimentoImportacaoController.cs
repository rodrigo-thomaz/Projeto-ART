using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/movimentoimportacao")]
    [Authorize]
    public class MovimentoImportacaoController : BaseApiController
    {
        #region private readonly fields

        private readonly IMovimentoImportacaoAppService _movimentoImportacaoAppService;

        #endregion

        #region constructors

        public MovimentoImportacaoController(IMovimentoImportacaoAppService movimentoImportacaoAppService)
        {
            _movimentoImportacaoAppService = movimentoImportacaoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de importações dos movimentos
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de importações dos movimentos 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="movimentoImportacaoId">filtro por movimentoImportacaoId</param>
        /// <param name="tipoTransacao">filtro por tipo de transação</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<MovimentoImportacaoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]long movimentoImportacaoId, TipoTransacao? tipoTransacao)
        {
            var result = await _movimentoImportacaoAppService.GetMasterList(param, movimentoImportacaoId, tipoTransacao);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma lista de importações de movimentos
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de importações de movimentos para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<MovimentoImportacaoSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param)
        {
            ValidateModelState();
            var response = await _movimentoImportacaoAppService.GetSelectViewList(param);
            return Ok(response);
        }

        /// <summary>
        /// Remover um movimento importação
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único movimento importação existente por id 
        /// Se o movimento importação já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do movimento importação</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _movimentoImportacaoAppService.Remove(id);
            return Ok(result);
        } 

        #endregion
    }
}