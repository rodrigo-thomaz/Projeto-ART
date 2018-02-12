using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/deviceType")]    
    public class DeviceTypeController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IDeviceTypeProducer _deviceTypeProducer;

        #endregion

        #region constructors

        public DeviceTypeController(IDeviceTypeProducer deviceTypeProducer) 
        {
            _deviceTypeProducer = deviceTypeProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipo de devices
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipo de devices
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _deviceTypeProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}