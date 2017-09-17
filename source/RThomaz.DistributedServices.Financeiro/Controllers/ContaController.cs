using RThomaz.Domain.Financeiro.Enums;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Helpers;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/conta")]
    [Authorize]
    public class ContaController : BaseApiController
    {
        #region private readonly fields

        private readonly IContaAppService _contaAppService;

        #endregion

        #region constructors

        public ContaController(IContaAppService contaAppService)
        {
            _contaAppService = contaAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de contas
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de contas 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="tipoConta">tipo de conta</param>
        /// <param name="ativo">filtro de atividade</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<ContaMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, TipoConta? tipoConta, [FromUri]bool? ativo)
        {
            var result = await _contaAppService.GetMasterList(param, tipoConta, ativo);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma conta espécie
        /// </summary>        
        /// <remarks>
        /// Retorna uma única conta espécie existente por id
        /// </remarks>
        /// <param name="id">id da conta espécie</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaEspecieDetailViewModel))]
        [Route("especie/{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> GetContaEspecieDetail(long id)
        {
            var model = await _contaAppService.GetContaEspecieDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma conta corrente
        /// </summary>        
        /// <remarks>
        /// Retorna uma única conta corrente existente por id
        /// </remarks>
        /// <param name="id">id da conta corrente</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaCorrenteDetailViewModel))]
        [Route("corrente/{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> GetContaCorrenteDetail(long id)
        {
            var model = await _contaAppService.GetContaCorrenteDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma conta poupança
        /// </summary>        
        /// <remarks>
        /// Retorna uma única conta poupança existente por id
        /// </remarks>
        /// <param name="id">id da conta poupança</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaPoupancaDetailViewModel))]
        [Route("poupanca/{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> GetContaPoupancaDetail(long id)
        {
            var model = await _contaAppService.GetContaPoupancaDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma conta cartão de crédito
        /// </summary>        
        /// <remarks>
        /// Retorna uma única conta cartão de crédito existente por id
        /// </remarks>
        /// <param name="id">id da conta cartão de crédito</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaCartaoCreditoDetailViewModel))]
        [Route("cartaocredito/{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> GetContaCartaoCreditoDetail(long id)
        {
            var model = await _contaAppService.GetContaCartaoCreditoDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Retornar uma lista de contas
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de contas para ser usada em listas de seleção
        /// </remarks>
        /// <param name="tipoConta">filtra o resultado por tipo de conta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<ContaSelectViewModel>))]
        [Route("selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSelectViewList([FromUri]TipoConta? tipoConta)
        {
            var selectListModel = await _contaAppService.GetSelectViewList(tipoConta);
            return Ok(selectListModel);
        }

        /// <summary>
        /// Retornar uma lista de contas com lançamentos programados existentes
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de contas com lançamentos programados existentes para ser usada em listas de seleção
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<CountModel<ContaSelectViewModel>>))]
        [Route("selectViewListWithProgramacao")]
        [HttpGet]
        public async Task<IHttpActionResult> GetWithProgramacaoSelectViewList()
        {
            var selectListModel = await _contaAppService.GetWithProgramacaoSelectViewList();
            return Ok(selectListModel);
        }

        /// <summary>
        /// Retornar o sumário das contas
        /// </summary>        
        /// <remarks>
        /// Retornar o sumário das contas para ser usado como painel de saldo
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<ContaSummaryViewModel>))]
        [Route("summaryViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSummaryViewList()
        {
            var selectListModel = await _contaAppService.GetSummaryViewList();
            return Ok(selectListModel);
        }

        /// <summary>
        /// Inserir uma conta espécie
        /// </summary>
        /// <remarks>
        /// Inseri uma nova conta espécie
        /// 
        /// </remarks>
        /// <param name="model">model da conta espécie</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaEspecieDetailViewModel))]
        [Route("especie")]
        public async Task<IHttpActionResult> PostContaEspecie(ContaEspecieDetailInsertModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.InsertContaEspecie(model);
            //return CreatedAtRoute("", new { contaId = result.ContaId }, result);
            return Ok(result);
        }

        /// <summary>
        /// Inserir uma conta corrente
        /// </summary>
        /// <remarks>
        /// Inseri uma nova conta corrente
        /// 
        /// </remarks>
        /// <param name="model">model da conta corrente</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaCorrenteDetailViewModel))]
        [Route("corrente")]
        public async Task<IHttpActionResult> PostContaCorrente(ContaCorrenteDetailInsertModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.InsertContaCorrente(model);
            //return CreatedAtRoute("", new { contaId = result.ContaId }, result);
            return Ok(result);
        }

        /// <summary>
        /// Inserir uma conta poupança
        /// </summary>
        /// <remarks>
        /// Inseri uma nova conta poupança
        /// 
        /// </remarks>
        /// <param name="model">model da conta poupança</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaPoupancaDetailViewModel))]
        [Route("poupanca")]
        public async Task<IHttpActionResult> PostContaPoupanca(ContaPoupancaDetailInsertModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.InsertContaPoupanca(model);
            //return CreatedAtRoute("", new { contaId = result.ContaId }, result);
            return Ok(result);
        }

        /// <summary>
        /// Inserir uma conta cartão de crédito
        /// </summary>
        /// <remarks>
        /// Inseri uma nova conta cartão de crédito
        /// 
        /// </remarks>
        /// <param name="model">model da conta cartão de crédito</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaCartaoCreditoDetailViewModel))]
        [Route("cartaocredito")]
        public async Task<IHttpActionResult> PostContaCartaoCredito(ContaCartaoCreditoDetailInsertModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.InsertContaCartaoCredito(model);
            //return CreatedAtRoute("", new { contaId = result.ContaId }, result);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar uma conta espécie
        /// </summary>
        /// <remarks>
        /// Atualiza uma única conta espécie existente por id        
        /// </remarks>
        /// <param name="model">model da conta espécie</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaEspecieDetailViewModel))]
        [Route("especie")]
        public async Task<IHttpActionResult> PutContaEspecie(ContaEspecieDetailEditModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.EditContaEspecie(model);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar uma conta corrente
        /// </summary>
        /// <remarks>
        /// Atualiza uma única conta corrente existente por id        
        /// </remarks>
        /// <param name="model">model da conta corrente</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaCorrenteDetailViewModel))]
        [Route("corrente")]
        public async Task<IHttpActionResult> PutContaCorrente(ContaCorrenteDetailEditModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.EditContaCorrente(model);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar uma conta poupança
        /// </summary>
        /// <remarks>
        /// Atualiza uma única conta poupança existente por id        
        /// </remarks>
        /// <param name="model">model da conta poupança</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaPoupancaDetailViewModel))]
        [Route("poupanca")]
        public async Task<IHttpActionResult> PutContaPoupanca(ContaPoupancaDetailEditModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.EditContaPoupanca(model);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar uma conta cartão de crédito
        /// </summary>
        /// <remarks>
        /// Atualiza uma única conta cartão de crédito existente por id        
        /// </remarks>
        /// <param name="model">model da conta cartão de crédito</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(ContaCartaoCreditoDetailViewModel))]
        [Route("cartaocredito")]
        public async Task<IHttpActionResult> PutContaCartaoCredito(ContaCartaoCreditoDetailEditModel model)
        {
            ValidateModelState();
            var result = await _contaAppService.EditContaCartaoCredito(model);
            return Ok(result);
        }

        /// <summary>
        /// Remover uma conta
        /// </summary>        
        /// <remarks>
        /// Tenta remover uma única conta existente por id 
        /// Se a conta já estiver sendo utilizada pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id da conta</param>
        /// <param name="tipoConta">Tipo de conta: ContaEspecie = 0 | ContaCorrente = 1 | ContaPoupanca = 2 | ContaCartaoCredito = 3</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}/{tipoConta}")]
        public async Task<IHttpActionResult> Delete(long id, TipoConta tipoConta)
        {
            var result = await _contaAppService.Remove(id, tipoConta);
            return Ok(result);
        }

        #endregion       
    }
}