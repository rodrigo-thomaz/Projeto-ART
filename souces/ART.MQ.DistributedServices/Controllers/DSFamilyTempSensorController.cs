using System.Web.Http;
using ART.MQ.DistributedServices.Models;
using ART.Infra.CrossCutting.WebApi;
using ART.MQ.DistributedServices.IProducers;
using System.Threading.Tasks;

namespace ART.MQ.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : BaseApiController
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
        /// Altera a resolução de um sensor
        /// </summary>
        /// <remarks>
        /// Altera a resolução de um sensor
        /// </remarks>
        /// <param name="request">model do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setResolution")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResolution(DSFamilyTempSensorSetResolutionModel request)
        {
            await _dsFamilyTempSensorProducer.SetResolution(request);
            return Ok();
        }

        /// <summary>
        /// Altera o alarme alto de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o alarme alto de um sensor
        /// </remarks>
        /// <param name="request">model do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setHighAlarm")]
        [HttpPost]
        public async Task<IHttpActionResult> SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request)
        {
            await _dsFamilyTempSensorProducer.SetHighAlarm(request);
            return Ok();
        }

        /// <summary>
        /// Altera o alarme baixo de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o alarme baixo de um sensor
        /// </remarks>
        /// <param name="request">model do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setLowAlarm")]
        [HttpPost]
        public async Task<IHttpActionResult> SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request)
        {
            await _dsFamilyTempSensorProducer.SetLowAlarm(request);
            return Ok();
        }

        #endregion
    }
}