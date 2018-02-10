namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceSerial")]
    public class DeviceSerialController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceSerialProducer _deviceSerialProducer;        

        #endregion Fields

        #region Constructors

        public DeviceSerialController(IDeviceSerialProducer deviceSerialProducer)
        {
            _deviceSerialProducer = deviceSerialProducer;            
        }

        #endregion Constructors

        #region public voids      

        /// <summary>
        /// Altera o Enabled de um DeviceSerial
        /// </summary>
        /// <remarks>
        /// Altera o Enabled de um DeviceSerial
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setEnabled")]
        [HttpPost]
        public async Task<IHttpActionResult> SetEnabled(DeviceSerialSetEnabledRequestContract contract)
        {
            await _deviceSerialProducer.SetEnabled(CreateMessage(contract));
            return Ok();
        }
        
        #endregion
    }
}