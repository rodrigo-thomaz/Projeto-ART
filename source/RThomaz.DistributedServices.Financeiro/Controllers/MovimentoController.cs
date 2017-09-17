using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.Domain.Financeiro.Services.DTOs.Enums;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/movimento")]
    [Authorize]
    public class MovimentoController : BaseApiController
    {
        #region private readonly fields

        private readonly IMovimentoAppService _movimentoAppService;

        #endregion

        #region constructors

        public MovimentoController(IMovimentoAppService movimentoAppService)
        {
            _movimentoAppService = movimentoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de movimentos bancários
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de movimentos bancários
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="periodo">filtro por periodo</param>
        /// <param name="contaId">filtro por conta</param>
        /// <param name="tipoConta">filtro por tipo de conta</param>
        /// <param name="tipoTransacao">filtro por tipo de transação</param>
        /// <param name="conciliacaoStatus">filtro por status da conciliação</param>
        /// <param name="conciliacaoOrigem">filtro por origem da conciliação</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<MovimentoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]MesAnoModel periodo, long contaId, TipoConta tipoConta, TipoTransacao? tipoTransacao, ConciliacaoStatus? conciliacaoStatus, ConciliacaoOrigem? conciliacaoOrigem)
        {
            if (periodo == null || periodo.Ano == 0 || periodo.Mes == 0 || contaId == 0)
            {
                return Ok(new jQueryDataTableResult<IEnumerable<string[]>>
                (
                    echo: param.draw,
                    totalRecords: 0,
                    totalDisplayRecords: 0,
                    data: new List<string[]>()
                ));
            }

            var result = await _movimentoAppService.GetMasterList(
                param,
                periodo,
                contaId,
                tipoConta,
                tipoTransacao,
                conciliacaoStatus,
                conciliacaoOrigem);

            return Ok(result);
        }

        /// <summary>
        /// Retornar uma lista de movimentos
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de movimentos para ser usada em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="tipoTransacao">filtro por tipo de transação</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<MovimentoSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]SelectListRequestModel param, TipoTransacao tipoTransacao)
        {
            ValidateModelState();
            var selectListModelResponse = await _movimentoAppService.GetSelectViewList(param, tipoTransacao);
            return Ok(selectListModelResponse);
        }

        /// <summary>
        /// Retornar uma lista de períodos
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de períodos para ser usado em listas de seleção
        /// </remarks>
        /// <param name="contaId">contaId para filtro</param>
        /// <param name="tipoConta">tipo de conta para filtro</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<MesAnoModel>))]
        [Route("periodos")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPeriodos(long contaId, TipoConta tipoConta)
        {
            var result = await _movimentoAppService.GetPeriodos(contaId, tipoConta);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um movimento
        /// </summary>        
        /// <remarks>
        /// Retorna um único movimento existente por id
        /// </remarks>
        /// <param name="id">id do movimento</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _movimentoAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Inserir um movimento
        /// </summary>
        /// <remarks>
        /// Inseri um novo movimento
        /// 
        /// </remarks>
        /// <param name="model">model do movimento</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(MovimentoDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _movimentoAppService.Insert(model);
            //return CreatedAtRoute("", new { bancoId = viewModel.MovimentoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um movimento manual
        /// </summary>
        /// <remarks>
        /// Atualiza um único movimento manual existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do movimento manual</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoDetailViewModel))]
        [Route("movimentomanual")]
        public async Task<IHttpActionResult> PutManual(MovimentoManualEditModel model)
        {
            ValidateModelState();
            var viewModel = await _movimentoAppService.EditManual(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um movimento importado
        /// </summary>
        /// <remarks>
        /// Atualiza um único movimento importado existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do movimento importado</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoDetailViewModel))]
        [Route("movimentoimportado")]
        public async Task<IHttpActionResult> PutImportado(MovimentoImportadoEditModel model)
        {
            ValidateModelState();
            var viewModel = await _movimentoAppService.EditImportado(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um movimento conciliado
        /// </summary>
        /// <remarks>
        /// Atualiza um único movimento conciliado existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do movimento conciliado</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoDetailViewModel))]
        [Route("movimentoconciliado")]
        public async Task<IHttpActionResult> PutConciliado(MovimentoConciliadoEditModel model)
        {
            ValidateModelState();
            var viewModel = await _movimentoAppService.EditConciliado(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um movimento
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único movimento existente por id 
        /// Se o movimento já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do movimento</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _movimentoAppService.Remove(id);
            return Ok(result);
        } 

        #endregion
    }
}