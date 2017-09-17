using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/transferencia")]
    [Authorize]
    public class TransferenciaController : BaseApiController
    {
        #region private readonly fields

        private readonly ITransferenciaAppService _transferenciaAppService;

        #endregion

        #region constructors

        public TransferenciaController(ITransferenciaAppService transferenciaAppService)
        {
            _transferenciaAppService = transferenciaAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma transferência 
        /// </summary>        
        /// <remarks>
        /// Retorna uma única transferência existente por id
        /// </remarks>
        /// <param name="id">id da transferência</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TransferenciaDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var model = await _transferenciaAppService.GetDetail(id);
            return Ok(model);
        }

        /// <summary>
        /// Inserir uma transferência
        /// </summary>
        /// <remarks>
        /// Inseri uma nova transferência
        /// 
        /// </remarks>
        /// <param name="model">model da transferência</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TransferenciaDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(TransferenciaDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _transferenciaAppService.Insert(model);
            //return CreatedAtRoute("", new { transferenciaId = viewModel.TransferenciaId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar uma transferência
        /// </summary>
        /// <remarks>
        /// Atualiza uma única transferência existente por id
        /// 
        /// </remarks>
        /// <param name="model">model da transferência</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BancoDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(TransferenciaDetailEditModel model)
        {
            ValidateModelState();
            var viewModel = await _transferenciaAppService.Edit(model);
            return Ok(viewModel);
        }

        /// <summary>
        /// Remover uma tranferência
        /// </summary>        
        /// <remarks>
        /// Tenta remover uma única tranferência existente por id 
        /// Se a tranferência já estiver sendo utilizada pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id da tranferência</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _transferenciaAppService.Remove(id);
            return Ok(result);
        }

        #endregion        
    }
}