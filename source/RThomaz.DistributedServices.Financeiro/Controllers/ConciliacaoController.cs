using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/conciliacao")]
    [Authorize]
    public class ConciliacaoController : BaseApiController
    {
        #region private readonly fields

        private readonly IConciliacaoAppService _conciliacaoAppService;

        #endregion

        #region constructors

        public ConciliacaoController(IConciliacaoAppService conciliacaoAppService)
        {
            _conciliacaoAppService = conciliacaoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de lançamentos conciliados
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de lançamentos conciliados por um movimentoId existente
        /// </remarks>
        /// <param name="movimentoId">movimentoId do movimento</param>
        /// <param name="tipoTransacao">tipoTransacao do movimento</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<ConciliacaoLancamentoMasterViewModel>))]
        [Route("getlancamentosconciliados/{movimentoid:long:custom_min(1)}/{tipotransacao}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLancamentosConciliados(long movimentoId, TipoTransacao tipoTransacao)
        {
            var result = await _conciliacaoAppService.GetLancamentosConciliados(movimentoId, tipoTransacao);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma lista de movimentos conciliados
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de movimentos conciliados por um lancamentoId existente
        /// </remarks>
        /// <param name="lancamentoId">lancamentoId do lançamento</param>
        /// <param name="tipoTransacao">tipoTransacao do lançamento</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<ConciliacaoLancamentoMasterViewModel>))]
        [Route("getmovimentosconciliados/{lancamentoid:long:custom_min(1)}/{tipotransacao}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMovimentosConciliados(long lancamentoId, TipoTransacao tipoTransacao)
        {
            var result = await _conciliacaoAppService.GetMovimentosConciliados(lancamentoId, tipoTransacao);
            return Ok(result);
        }

        #endregion
    }
}