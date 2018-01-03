namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceWiFi")]
    public class DeviceWiFiController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceWiFiProducer _deviceWiFiProducer;        

        #endregion Fields

        #region Constructors

        public DeviceWiFiController(IDeviceWiFiProducer deviceWiFiProducer)
        {
            _deviceWiFiProducer = deviceWiFiProducer;            
        }

        #endregion Constructors

        #region public voids      

        /// <summary>
        /// Altera o HostName de um device
        /// </summary>
        /// <remarks>
        /// Altera o HostName de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setHostName")]
        [HttpPost]
        public async Task<IHttpActionResult> SetHostName(DeviceWiFiSetHostNameRequestContract contract)
        {
            await _deviceWiFiProducer.SetHostName(CreateMessage(contract));
            return Ok();
        }
        
        #endregion
    }
}