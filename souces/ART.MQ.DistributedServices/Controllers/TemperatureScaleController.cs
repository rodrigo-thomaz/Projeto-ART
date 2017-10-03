using System.Web.Http;
using ART.Infra.CrossCutting.WebApi;
using ART.MQ.DistributedServices.IProducers;
using System.Threading.Tasks;

namespace ART.MQ.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/temperatureScale")]    
    public class TemperatureScaleController : AuthenticatedApiController
    {
        #region private readonly fields

        protected readonly ITemperatureScaleProducer _temperatureScaleProducer;

        #endregion

        #region constructors

        public TemperatureScaleController(ITemperatureScaleProducer temperatureScaleProducer) //: base(connection)
        {
            _temperatureScaleProducer = temperatureScaleProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de escalas
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de escalas
        /// </remarks>
        /// <param name="session">session do broker do solicitante</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getScales/{session}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetScales(string session)
        {           
            await _temperatureScaleProducer.GetScales(session);
            return Ok();
        }

        #endregion
    }
}