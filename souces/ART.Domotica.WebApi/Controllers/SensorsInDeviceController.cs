using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorsInDevice")]    
    public class SensorsInDeviceController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorsInDeviceProducer _sensorsInDeviceProducer;

        #endregion

        #region constructors

        public SensorsInDeviceController(ISensorsInDeviceProducer sensorsInDeviceProducer) 
        {
            _sensorsInDeviceProducer = sensorsInDeviceProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de Device Sensors
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de Device Sensors
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAllByApplicationId")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllByApplicationId()
        {           
            await _sensorsInDeviceProducer.GetAllByApplicationId(CreateMessage());
            return Ok();
        }

        #endregion
    }
}