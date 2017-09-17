using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/transferenciaprogramada")]
    [Authorize]
    public class TransferenciaProgramadaController : BaseApiController
    {
        #region private readonly fields

        private readonly ITransferenciaProgramadaAppService _transferenciaProgramadaAppService;

        #endregion

        #region constructors

        public TransferenciaProgramadaController(ITransferenciaProgramadaAppService transferenciaProgramadaAppService)
        {
            _transferenciaProgramadaAppService = transferenciaProgramadaAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma transferência programada
        /// </summary>        
        /// <remarks>
        /// Retorna uma única transferência programada existente por id
        /// </remarks>
        /// <param name="id">id da transferência programada</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TransferenciaProgramadaDetailViewModel))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _transferenciaProgramadaAppService.GetDetail(id);
            return Ok(result);
        }

        /// <summary>
        /// Inserir uma transferência programada
        /// </summary>
        /// <remarks>
        /// Inseri uma transferência programada
        /// </remarks>
        /// <param name="model">model da transferência programada</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TransferenciaProgramadaDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Post(TransferenciaProgramadaDetailInsertModel model)
        {
            ValidateModelState();
            var viewModel = await _transferenciaProgramadaAppService.Insert(model);
            //return CreatedAtRoute("", new { programacaoId = viewModel.ProgramacaoId }, viewModel);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar uma transferência programada
        /// </summary>
        /// <remarks>
        /// Atualiza uma única transferência programada existente por id
        /// </remarks>
        /// <param name="model">model da transferência programada</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(TransferenciaProgramadaDetailViewModel))]
        [Route("")]
        public async Task<IHttpActionResult> Put(TransferenciaProgramadaDetailEditModel model)
        {
            ValidateModelState();
            var result = await _transferenciaProgramadaAppService.Edit(model);
            return Ok(result);
        }

        /// <summary>
        /// Remover uma transferência programada
        /// </summary>        
        /// <remarks>
        /// Tenta remover uma única transferência programada existente por id 
        /// Se a transferência programada já estiver sendo utilizada pelo sistema, não remove e retorna false, caso contrário remove e retorna true
        /// 
        /// </remarks>
        /// <param name="id">id da transferência programada</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("{id:long:custom_min(1)}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _transferenciaProgramadaAppService.Remove(id);
            return Ok(result);
        }

        #endregion
    }
}