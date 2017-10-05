using System.Web.Http;
using ART.Domotica.WebApi.Models;
using ART.Infra.CrossCutting.WebApi;
using ART.Domotica.WebApi.IProducers;
using System.Threading.Tasks;
using System;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : AuthenticatedApiController
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
        /// <param name="applicationId">id da aplicação</param>
        /// <param name="session">session do broker do solicitante</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll/{applicationId}/{session}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll(Guid applicationId, string session)
        {
            await _dsFamilyTempSensorProducer.GetAll(applicationId, session);
            return Ok();
        }

        /// <summary>
        /// Retornar uma lista de Resoluções
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de Resoluções
        /// </remarks>
        /// <param name="session">session do broker do solicitante</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getResolutions/{session}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetResolutions(string session)
        {           
            await _dsFamilyTempSensorProducer.GetResolutions(session);
            return Ok();
        }

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