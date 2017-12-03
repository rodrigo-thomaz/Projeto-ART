using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensor")]    
    public class SensorController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorProducer _sensorProducer;

        #endregion

        #region constructors

        public SensorController(ISensorProducer sensorProducer) 
        {
            _sensorProducer = sensorProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de sensores da aplicação
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de sensores da aplicação
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _sensorProducer.GetAll(CreateMessage());
            return Ok();
        }

        /// <summary>
        /// Altera a escala de um sensor
        /// </summary>
        /// <remarks>
        /// Altera a escala de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setUnitMeasurement")]
        [HttpPost]
        public async Task<IHttpActionResult> SetUnitMeasurement(SensorSetUnitMeasurementRequestContract contract)
        {
            await _sensorProducer.SetUnitMeasurement(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o rótulo de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o rótulo de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setLabel")]
        [HttpPost]
        public async Task<IHttpActionResult> SetLabel(SensorSetLabelRequestContract contract)
        {
            await _sensorProducer.SetLabel(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}