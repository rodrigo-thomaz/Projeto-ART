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
        /// adiciona uma trigger no sensor
        /// </summary>
        /// <remarks>
        /// adiciona uma trigger no sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("insertTrigger")]
        [HttpPost]
        public async Task<IHttpActionResult> Insert(SensorTriggerInsertRequestContract contract)
        {
            await _sensorTriggerProducer.Insert(CreateMessage(contract));
            return Ok();
        }
        
        /// <summary>
        /// remove uma trigger do sensor
        /// </summary>
        /// <remarks>
        /// remove uma trigger do sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("deleteTrigger")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(SensorTriggerDeleteRequestContract contract)
        {
            await _sensorTriggerProducer.Delete(CreateMessage(contract));
            return Ok();
        }

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
        [Route("setTriggerOn")]
        [HttpPost]
        public async Task<IHttpActionResult> SetTriggerOn(SensorTriggerSetTriggerOnRequestContract contract)
        {
            await _sensorTriggerProducer.SetTriggerOn(CreateMessage(contract));
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
        [Route("setTriggerValue")]
        [HttpPost]
        public async Task<IHttpActionResult> SetTriggerValue(SensorTriggerSetTriggerValueRequestContract contract)
        {
            await _sensorTriggerProducer.SetTriggerValue(CreateMessage(contract));
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
        [Route("setBuzzerOn")]
        [HttpPost]
        public async Task<IHttpActionResult> SetBuzzerOn(SensorTriggerSetBuzzerOnRequestContract contract)
        {
            await _sensorTriggerProducer.SetBuzzerOn(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}