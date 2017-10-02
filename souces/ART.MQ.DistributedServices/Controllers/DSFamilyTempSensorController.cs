using System.Web.Http;
using ART.MQ.DistributedServices.Models;
using ART.Infra.CrossCutting.WebApi;
using ART.MQ.DistributedServices.IProducers;

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
        public IHttpActionResult SetResolution(DSFamilyTempSensorSetResolutionModel request)
        {
            _dsFamilyTempSensorProducer.SetResolution(request);
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
        public IHttpActionResult SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request)
        {
            _dsFamilyTempSensorProducer.SetHighAlarm(request);
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
        public IHttpActionResult SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request)
        {
            _dsFamilyTempSensorProducer.SetLowAlarm(request);
            return Ok();
        }

        #endregion
    }
}