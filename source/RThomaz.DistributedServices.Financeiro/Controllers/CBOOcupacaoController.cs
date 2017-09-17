using RThomaz.Application.Financeiro.Helpers.SelectListModel;
using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/cboocupacao")]
    [Authorize]
    public class CBOOcupacaoController : BaseApiController
    {
        #region private readonly fields

        private readonly ICBOOcupacaoAppService _cboOcupacaoAppService;

        #endregion

        #region constructors

        public CBOOcupacaoController(ICBOOcupacaoAppService cboOcupacaoAppService)
        {
            _cboOcupacaoAppService = cboOcupacaoAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de CBOOcupacao (Cadastro brasileiro de ocupações)
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de CBOOcupacao para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<CBOOcupacaoSelectViewModel>))]
        [Route("ocupacao/selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOcupacaoSelectViewList([FromUri]SelectListRequestModel param)
        {
            ValidateModelState();
            var selectListModelResponse = await _cboOcupacaoAppService.GetOcupacaoSelectViewList(param);
            return Ok(selectListModelResponse);
        }

        /// <summary>
        /// Retornar uma lista de CBOSinonimo (Cadastro brasileiro de ocupações)
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de CBOSinonimo para ser usado em listas de seleção
        /// </remarks>
        /// <param name="param">model de parâmetros de busca</param>
        /// <param name="cboOcupacaoId">cboOcupacaoId para parâmetro de busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SelectListResponseModel<CBOSinonimoSelectViewModel>))]
        [Route("sinonimo/selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSinonimoSelectViewList([FromUri]SelectListRequestModel param, int cboOcupacaoId)
        {
            ValidateModelState();
            var selectListModelResponse = await _cboOcupacaoAppService.GetSinonimoSelectViewList(param, cboOcupacaoId);
            return Ok(selectListModelResponse);
        }

        #endregion        
    }
}