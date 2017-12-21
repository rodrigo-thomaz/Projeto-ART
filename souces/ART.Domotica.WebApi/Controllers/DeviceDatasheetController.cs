using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/deviceDatasheet")]    
    public class DeviceDatasheetController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IDeviceDatasheetProducer _deviceDatasheetProducer;

        #endregion

        #region constructors

        public DeviceDatasheetController(IDeviceDatasheetProducer deviceDatasheetProducer) 
        {
            _deviceDatasheetProducer = deviceDatasheetProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de datasheet dos devices
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de datasheet dos devices
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _deviceDatasheetProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}