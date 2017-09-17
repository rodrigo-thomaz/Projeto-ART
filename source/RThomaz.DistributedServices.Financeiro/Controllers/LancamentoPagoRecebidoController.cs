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
    [RoutePrefix("api/lancamentopagorecebido")]
    [Authorize]
    public class LancamentoPagoRecebidoController : BaseApiController
    {
        #region private readonly fields

        private readonly ILancamentoPagoRecebidoAppService _lancamentoPagoRecebidoAppService;

        #endregion

        #region constructors

        public LancamentoPagoRecebidoController(ILancamentoPagoRecebidoAppService lancamentoPagoRecebidoAppService)
        {
            _lancamentoPagoRecebidoAppService = lancamentoPagoRecebidoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de lançamentos pagos e recebidos
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de lançamentos pagos e recebidos 
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
            if (periodo == null || periodo.Ano == 0 || periodo.Mes == 0)
            {
                return Ok(new jQueryDataTableResult<IEnumerable<string[]>>
                (
                    echo: param.draw,
                    totalRecords: 0,
                    totalDisplayRecords: 0,
                    data: new List<string[]>()
                ));
            }

            var result = await _lancamentoPagoRecebidoAppService.GetMasterList(param, periodo, contaId, tipoConta);
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
            var result = await _lancamentoPagoRecebidoAppService.GetPeriodos(contaId, tipoConta);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um lançamento pago/recebido do contas a pagar/receber
        /// </summary>        
        /// <remarks>
        /// Retorna um único lançamento pago/recebido do contas a pagar/receber existente por id
        /// </remarks>
        /// <param name="id">id do lançamento pago/recebido do contas a pagar/receber</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoPagoRecebidoDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _lancamentoPagoRecebidoAppService.GetDetail(id);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar um lançamento pago/recebido
        /// </summary>
        /// <remarks>
        /// Atualiza um único lançamento pago/recebido existente por id
        /// </remarks>
        /// <param name="model">model do lançamento pago/recebido</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoPagoRecebidoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(LancamentoPagoRecebidoDetailEditModel model)
        {
            ValidateModelState();
            var result = await _lancamentoPagoRecebidoAppService.Edit(model);
            return Ok(result);
        }

        /// <summary>
        /// Remover um lancamento pago/recebido
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único lancamento pago/recebido existente por id 
        /// Se o lancamento pago/recebido já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do lancamento pago/recebido</param>
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
            var result = await _lancamentoPagoRecebidoAppService.Remove(id, tipoTransacao);
            return Ok(result);
        }

        #endregion
    }
}