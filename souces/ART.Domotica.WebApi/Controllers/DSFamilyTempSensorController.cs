using System.Web.Http;
using ART.Domotica.WebApi.IProducers;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IDSFamilyTempSensorProducer _dsFamilyTempSensorProducer;

        #endregion

        #region constructors

        public DSFamilyTempSensorController(IDSFamilyTempSensorProducer dsFamilyTempSensorProducer) //: base(connection)
        {
            _dsFamilyTempSensorProducer = dsFamilyTempSensorProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de sensores
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de sensores
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {
            await _dsFamilyTempSensorProducer.GetAll(CreateMessage());
            return Ok();
        }

        /// <summary>
        /// Retornar uma lista de Resoluções
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de Resoluções
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getResolutions")]
        [HttpPost]
        public async Task<IHttpActionResult> GetResolutions()
        {
            await _dsFamilyTempSensorProducer.GetResolutions(CreateMessage());
            return Ok();
        }

        /// <summary>
        /// Altera a resolução de um sensor
        /// </summary>
        /// <remarks>
        /// Altera a resolução de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setResolution")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResolution(DSFamilyTempSensorSetResolutionContract contract)
        {
            await _dsFamilyTempSensorProducer.SetResolution(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o alarme alto de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o alarme alto de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setHighAlarm")]
        [HttpPost]
        public async Task<IHttpActionResult> SetHighAlarm(DSFamilyTempSensorSetHighAlarmContract contract)
        {
            await _dsFamilyTempSensorProducer.SetHighAlarm(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o alarme baixo de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o alarme baixo de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setLowAlarm")]
        [HttpPost]
        public async Task<IHttpActionResult> SetLowAlarm(DSFamilyTempSensorSetLowAlarmContract contract)
        {
            await _dsFamilyTempSensorProducer.SetLowAlarm(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}