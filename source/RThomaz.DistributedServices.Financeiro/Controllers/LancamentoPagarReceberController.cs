using RThomaz.Domain.Financeiro.Enums;
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
    [RoutePrefix("api/lancamentopagarreceber")]
    [Authorize]
    public class LancamentoPagarReceberController : BaseApiController
    {
        #region private readonly fields

        private readonly ILancamentoPagarReceberAppService _lancamentoPagarReceberAppService;

        #endregion

        #region constructors

        public LancamentoPagarReceberController(ILancamentoPagarReceberAppService lancamentoPagarReceberAppService)
        {
            _lancamentoPagarReceberAppService = lancamentoPagarReceberAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de lançamentos a pagar e a receber
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de lançamentos a pagar e a receber 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="periodo">filtro por periodo</param>
        /// <param name="contaId">filtro por conta</param>
        /// <param name="tipoConta">filtro por tipo de conta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<LancamentoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, [FromUri]MesAnoModel periodo, long contaId, TipoConta tipoConta)
        {
            if (periodo == null)
            {
                return Ok(new jQueryDataTableResult<IEnumerable<string[]>>
                (
                    echo: param.draw,
                    totalRecords: 0,
                    totalDisplayRecords: 0,
                    data: new List<string[]>()
                ));
            }

            var result = await _lancamentoPagarReceberAppService.GetMasterList(param, periodo, contaId, tipoConta);
            return Ok(result);
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
            var result = await _lancamentoPagarReceberAppService.GetPeriodos(contaId, tipoConta);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um lançamento do contas a pagar/receber
        /// </summary>        
        /// <remarks>
        /// Retorna um único lançamento do contas a pagar/receber existente por id
        /// </remarks>
        /// <param name="id">id do lançamento do contas a pagar/receber</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoPagarReceberDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _lancamentoPagarReceberAppService.GetDetail(id);
            return Ok(result);
        }

        /// <summary>
        /// Inserir um lancamento pagar/receber
        /// </summary>
        /// <remarks>
        /// Inseri um novo lancamento pagar/receber
        /// </remarks>
        /// <param name="model">model do lancamento pagar/receber</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoPagarReceberDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(LancamentoPagarReceberDetailInsertModel model)
        {
            ValidateModelState();
            var result = await _lancamentoPagarReceberAppService.Insert(model);
            //return CreatedAtRoute("", new { lancamentoId = result.LancamentoId }, result);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar um lancamento pagar/receber
        /// </summary>
        /// <remarks>
        /// Atualiza um único lancamento pagar/receber existente por id
        /// </remarks>
        /// <param name="model">model do lancamento pagar/receber</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoPagarReceberDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(LancamentoPagarReceberDetailEditModel model)
        {
            ValidateModelState();
            var result = await _lancamentoPagarReceberAppService.Edit(model);
            return Ok(result);
        }

        /// <summary>
        /// Remover um lançamento pagar/receber
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único lançamento pagar/receber existente por id 
        /// Se o lançamento pagar/receber já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do lançamento pagar/receber</param>
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
            var result = await _lancamentoPagarReceberAppService.Remove(id, tipoTransacao);
            return Ok(result);
        }

        #endregion
    }
}