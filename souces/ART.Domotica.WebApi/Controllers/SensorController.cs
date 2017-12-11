using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensor")]    
    public class SensorController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorProducer _sensorProducer;

        #endregion

        #region constructors

        public SensorController(ISensorProducer sensorProducer) 
        {
            _sensorProducer = sensorProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de sensores da aplicação
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de sensores da aplicação
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAllByApplicationId")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllByApplicationId()
        {           
            await _sensorProducer.GetAllByApplicationId(CreateMessage());
            return Ok();
        }

        #endregion
    }
}