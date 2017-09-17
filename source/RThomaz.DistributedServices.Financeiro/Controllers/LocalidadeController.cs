using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Infra.CrossCutting.Storage;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [RoutePrefix("api/localidade")]
    [Authorize]
    public class LocalidadeController : BaseApiController
    {
        #region private readonly fields

        private readonly ILocalidadeAppService _localidadeAppService;

        #endregion

        #region constructors

        public LocalidadeController(ILocalidadeAppService localidadeAppService)
        {
            _localidadeAppService = localidadeAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar a bandeira de um pais
        /// </summary>        
        /// <remarks>
        /// Retorna a bandeira de um único pais existente por id
        /// </remarks>
        /// <param name="id">id da bandeira</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("pais/bandeira/{id}")]
        public async Task<HttpResponseMessage> GetPaisBandeira(string id)
        {
            var dto = await _localidadeAppService.GetPaisBandeira(id);
            var result = new StorageHttpResponseMessage(dto);
            return result;
        }

        /// <summary>
        /// Retornar uma lista de paises
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de paises para ser usada em listas de seleção
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<PaisSelectViewModel>))]
        [Route("pais/selectViewList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPaisSelectViewList()
        {
            var result = await _localidadeAppService.GetPaisSelectViewList();
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma lista de estados
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de estados para ser usada em listas de seleção
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<EstadoSelectViewModel>))]
        [Route("estado/selectViewList/pais/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEstadoSelectViewList(long paisId)
        {
            var result = await _localidadeAppService.GetEstadoSelectViewList(paisId);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma lista de cidades
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de cidades para ser usada em listas de seleção
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<CidadeSelectViewModel>))]
        [Route("cidade/selectViewList/estado/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCidadeSelectViewList(long estadoId)
        {
            var result = await _localidadeAppService.GetCidadeSelectViewList(estadoId);
            return Ok(result);
        }

        /// <summary>
        /// Retornar uma lista de bairros
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de bairros para ser usada em listas de seleção
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(List<BairroSelectViewModel>))]
        [Route("bairro/selectViewList/cidade/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBairroSelectViewList(long cidadeId)
        {
            var result = await _localidadeAppService.GetBairroSelectViewList(cidadeId);
            return Ok(result);
        }

        /// <summary>
        /// Atualizar uma pessoa física
        /// </summary>
        /// <remarks>
        /// Atualiza uma única pessoa física existente por id        
        /// </remarks>
        /// <param name="model">model da pessoa física</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(BairroSelectViewDTO))]
        [Route("")]
        public async Task<IHttpActionResult> Put(LocalidadeDetailUpdateModel model)
        {
            ValidateModelState();
            var result = await _localidadeAppService.UpdateLocalidade(model);
            return Ok(result);
        }

        #endregion
    }
}