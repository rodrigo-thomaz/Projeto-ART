using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorTrigger")]    
    public class SensorTriggerController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorTriggerProducer _sensorTriggerProducer;

        #endregion

        #region constructors

        public SensorTriggerController(ISensorTriggerProducer sensorTriggerProducer) 
        {
            _sensorTriggerProducer = sensorTriggerProducer;
        }

        #endregion

        #region public voids 

        /// <summary>
        /// Liga/Desliga o alarme de um sensor
        /// </summary>
        /// <remarks>
        /// Liga/Desliga o alarme de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setAlarmOn")]
        [HttpPost]
        public async Task<IHttpActionResult> SetAlarmOn(SensorTriggerSetAlarmOnRequestContract contract)
        {
            await _sensorTriggerProducer.SetAlarmOn(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o valor do alarme de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o valor do alarme de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setAlarmCelsius")]
        [HttpPost]
        public async Task<IHttpActionResult> SetAlarmCelsius(SensorTriggerSetAlarmCelsiusRequestContract contract)
        {
            await _sensorTriggerProducer.SetAlarmCelsius(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Liga/Desliga o som do alarme de um sensor
        /// </summary>
        /// <remarks>
        /// Liga/Desliga o som do alarme de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setAlarmBuzzerOn")]
        [HttpPost]
        public async Task<IHttpActionResult> SetAlarmBuzzerOn(SensorTriggerSetAlarmBuzzerOnRequestContract contract)
        {
            await _sensorTriggerProducer.SetAlarmBuzzerOn(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}