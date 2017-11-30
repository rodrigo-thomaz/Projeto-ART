using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorType")]    
    public class SensorTypeController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorTypeProducer _sensorTypeProducer;

        #endregion

        #region constructors

        public SensorTypeController(ISensorTypeProducer sensorTypeProducer) 
        {
            _sensorTypeProducer = sensorTypeProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipo de sensores
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipo de sensores
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _sensorTypeProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}