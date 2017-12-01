using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

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

        public DSFamilyTempSensorController(IDSFamilyTempSensorProducer dsFamilyTempSensorProducer) 
        {
            _dsFamilyTempSensorProducer = dsFamilyTempSensorProducer;
        }

        #endregion

        #region public voids        
        
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
        [Route("getAllResolutions")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllResolutions()
        {
            await _dsFamilyTempSensorProducer.GetAllResolutions(CreateMessage());
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
        public async Task<IHttpActionResult> SetResolution(DSFamilyTempSensorSetResolutionRequestContract contract)
        {
            await _dsFamilyTempSensorProducer.SetResolution(CreateMessage(contract));
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
        public async Task<IHttpActionResult> SetUnitMeasurement(DSFamilyTempSensorSetUnitMeasurementRequestContract contract)
        {
            await _dsFamilyTempSensorProducer.SetUnitMeasurement(CreateMessage(contract));
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
        public async Task<IHttpActionResult> SetLabel(DSFamilyTempSensorSetLabelRequestContract contract)
        {
            await _dsFamilyTempSensorProducer.SetLabel(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}