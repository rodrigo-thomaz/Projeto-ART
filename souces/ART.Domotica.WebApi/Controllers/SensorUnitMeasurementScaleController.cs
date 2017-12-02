using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorUnitMeasurementScale")]    
    public class SensorUnitMeasurementScaleController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorUnitMeasurementScaleProducer _sensorUnitMeasurementScaleProducer;

        #endregion

        #region constructors

        public SensorUnitMeasurementScaleController(ISensorUnitMeasurementScaleProducer sensorUnitMeasurementScaleProducer) 
        {
            _sensorUnitMeasurementScaleProducer = sensorUnitMeasurementScaleProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de escalas do sensor
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de escalas do sensor
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _sensorUnitMeasurementScaleProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}