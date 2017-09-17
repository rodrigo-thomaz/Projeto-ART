using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Application.Financeiro.Helpers.jQueryDataTable;
using System.Collections.Generic;
using RThomaz.Domain.Financeiro.Enums;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/lancamentoprogramado")]
    [Authorize]
    public class LancamentoProgramadoController : BaseApiController
    {
        #region private readonly fields

        private readonly ILancamentoProgramadoAppService _lancamentoProgramadoAppService;

        #endregion

        #region constructors

        public LancamentoProgramadoController(ILancamentoProgramadoAppService lancamentoProgramadoAppService)
        {
            _lancamentoProgramadoAppService = lancamentoProgramadoAppService;
        }

        #endregion

        #region public voids        

        /// <summary>
        /// Retornar uma lista de lançamentos programados
        /// </summary>        
        /// <remarks>
        /// Retorna uma lista de lançamentos programados 
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="contaId">filtro por conta</param>
        /// <param name="tipoConta">filtro por tipo de conta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(jQueryDataTableResult<List<LancamentoProgramadoMasterModel>>))]
        [Route("masterViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMasterViewList([FromUri]jQueryDataTableParameter param, long? contaId, TipoConta? tipoConta)
        {            
            var result = await _lancamentoProgramadoAppService.GetMasterList(param, contaId, tipoConta);
            return Ok(result);
        }

        /// <summary>
        /// Retornar um lançamento programado
        /// </summary>        
        /// <remarks>
        /// Retorna um único lançamento programado existente por id
        /// </remarks>
        /// <param name="id">id do lançamento programado</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoProgramadoDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _lancamentoProgramadoAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Inserir um lançamento programado
        /// </summary>
        /// <remarks>
        /// Inseri um novo lançamento programado
        /// 
        /// </remarks>
        /// <param name="model">model do lançamento programado</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoProgramadoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(LancamentoProgramadoDetailUpdateModel model)
        {
            ValidateModelState();
            var viewModel = await _lancamentoProgramadoAppService.Insert(model);
            //return CreatedAtRoute("", new { programacaoId = viewModel.ProgramacaoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um lançamento programado
        /// </summary>
        /// <remarks>
        /// Atualiza um único lançamento programado existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do lançamento programado</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(LancamentoProgramadoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(LancamentoProgramadoDetailUpdateModel model)
        {
            ValidateModelState();
            var viewModel = await _lancamentoProgramadoAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover um lançamento programado
        /// </summary>        
        /// <remarks>
        /// Tenta remover um único lançamento programado existente por id 
        /// Se o lançamento programado já estiver sendo utilizado pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id do lançamento programado</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _lancamentoProgramadoAppService.Remove(id);
            return Ok(result);
        } 

        #endregion
    }
}