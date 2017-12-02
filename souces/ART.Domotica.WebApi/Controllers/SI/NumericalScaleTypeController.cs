using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.WebApi.Controllers.SI
{
    [Authorize]
    [RoutePrefix("api/si/numericalScaleType")]    
    public class NumericalScaleTypeController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly INumericalScaleTypeProducer _numericalScaleTypeProducer;

        #endregion

        #region constructors

        public NumericalScaleTypeController(INumericalScaleTypeProducer numericalScaleTypeProducer) 
        {
            _numericalScaleTypeProducer = numericalScaleTypeProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipos de escala numérica
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipos de escala numérica
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _numericalScaleTypeProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}