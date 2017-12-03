using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorDatasheetUnitMeasurementScale")]    
    public class SensorDatasheetUnitMeasurementScaleController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorDatasheetUnitMeasurementScaleProducer _sensorDatasheetUnitMeasurementScaleProducer;

        #endregion

        #region constructors

        public SensorDatasheetUnitMeasurementScaleController(ISensorDatasheetUnitMeasurementScaleProducer sensorDatasheetUnitMeasurementScaleProducer) 
        {
            _sensorDatasheetUnitMeasurementScaleProducer = sensorDatasheetUnitMeasurementScaleProducer;
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
            await _sensorDatasheetUnitMeasurementScaleProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}