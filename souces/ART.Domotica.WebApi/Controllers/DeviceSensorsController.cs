using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/deviceSensors")]    
    public class DeviceSensorsController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IDeviceSensorsProducer _deviceSensorsProducer;

        #endregion

        #region constructors

        public DeviceSensorsController(IDeviceSensorsProducer deviceSensorsProducer) 
        {
            _deviceSensorsProducer = deviceSensorsProducer;
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
            await _deviceSensorsProducer.GetAllByApplicationId(CreateMessage());
            return Ok();
        }

        #endregion
    }
}