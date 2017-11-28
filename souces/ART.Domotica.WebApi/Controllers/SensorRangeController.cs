using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorRange")]    
    public class SensorRangeController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorRangeProducer _sensorRangeProducer;

        #endregion

        #region constructors

        public SensorRangeController(ISensorRangeProducer sensorRangeProducer) 
        {
            _sensorRangeProducer = sensorRangeProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de range de atuação de sensores
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de range de atuação de sensores
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _sensorRangeProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}