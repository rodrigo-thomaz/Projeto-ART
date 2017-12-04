using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorTempDSFamily")]    
    public class SensorTempDSFamilyController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorTempDSFamilyProducer _sensorTempDSFamilyProducer;

        #endregion

        #region constructors

        public SensorTempDSFamilyController(ISensorTempDSFamilyProducer sensorTempDSFamilyProducer) 
        {
            _sensorTempDSFamilyProducer = sensorTempDSFamilyProducer;
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
            await _sensorTempDSFamilyProducer.GetAllResolutions(CreateMessage());
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
        public async Task<IHttpActionResult> SetResolution(SensorTempDSFamilySetResolutionRequestContract contract)
        {
            await _sensorTempDSFamilyProducer.SetResolution(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}