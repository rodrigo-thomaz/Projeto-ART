using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using System;
using RThomaz.Application.Financeiro.Helpers.Storage;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/movimentoimportacaoofx")]
    [Authorize]
    public class MovimentoImportacaoOFXController : BaseApiController
    {
        #region private readonly fields

        private readonly IMovimentoImportacaoOFXAppService _movimentoImportacaoOFXAppService;

        #endregion

        #region constructors

        public MovimentoImportacaoOFXController(IMovimentoImportacaoOFXAppService movimentoImportacaoOFXAppService)
        {
            _movimentoImportacaoOFXAppService = movimentoImportacaoOFXAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retorna um preview da importação
        /// </summary>
        /// <remarks>
        /// Transforma o buffer do arquivo em preview da importação
        /// 
        /// </remarks>
        /// <param name="buffer">string buffer Base64 do arquivo ofx</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoImportacaoOFXModel))]
        [Route("preview")]
        [HttpPut]
        public async Task<IHttpActionResult> Preview(StorageUploadModel model)
        {
            ValidateModelState();
            var bytes = Convert.FromBase64String(model.BufferBase64String);
            var viewModel = await _movimentoImportacaoOFXAppService.Preview(bytes);
            return Ok(viewModel);
        }

        /// <summary>
        /// Importa um arquivo OFX
        /// </summary>
        /// <remarks>
        /// Importa um arquivo OFX
        /// 
        /// </remarks>
        /// <param name="model">model para importar</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(MovimentoImportacaoDetailViewModel))]
        [Route("import")]
        [HttpPost]
        public async Task<IHttpActionResult> Import(MovimentoImportacaoOFXModel model)
        {
            ValidateModelState();
            var viewModel = await _movimentoImportacaoOFXAppService.Import(model);
            //return CreatedAtRoute("", new { bancoId = viewModel.MovimentoImportacaoId }, viewModel);
            return Ok(viewModel);
        }

        #endregion
    }
}