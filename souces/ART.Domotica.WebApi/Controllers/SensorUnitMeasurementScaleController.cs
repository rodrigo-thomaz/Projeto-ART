using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
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
        /// Altera o valor do limite do gráfico de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o valor do limite do gráfico de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setValue")]
        [HttpPost]
        public async Task<IHttpActionResult> SetValue(SensorUnitMeasurementScaleSetValueRequestContract contract)
        {
            await _sensorUnitMeasurementScaleProducer.SetValue(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o valor do UnitMeasurementNumericalScaleTypeCountry de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o valor do limite do gráfico de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setUnitMeasurementNumericalScaleTypeCountry")]
        [HttpPost]
        public async Task<IHttpActionResult> SetUnitMeasurementNumericalScaleTypeCountry(SensorUnitMeasurementScaleSetUnitMeasurementNumericalScaleTypeCountryRequestContract contract)
        {
            await _sensorUnitMeasurementScaleProducer.SetUnitMeasurementNumericalScaleTypeCountry(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}