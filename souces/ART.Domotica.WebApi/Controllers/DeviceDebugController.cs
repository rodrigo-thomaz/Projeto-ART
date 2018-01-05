namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceDebug")]
    public class DeviceDebugController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceDebugProducer _deviceDebugProducer;        

        #endregion Fields

        #region Constructors

        public DeviceDebugController(IDeviceDebugProducer deviceDebugProducer)
        {
            _deviceDebugProducer = deviceDebugProducer;            
        }

        #endregion Constructors

        #region public voids      

        /// <summary>
        /// Altera o RemoteEnabled do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o RemoteEnabled do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setRemoteEnabled")]
        [HttpPost]
        public async Task<IHttpActionResult> SetRemoteEnabled(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetRemoteEnabled(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o SerialEnabled do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o SerialEnabled do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setSerialEnabled")]
        [HttpPost]
        public async Task<IHttpActionResult> SetSerialEnabled(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetSerialEnabled(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o ResetCmdEnabled do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o ResetCmdEnabled do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setResetCmdEnabled")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResetCmdEnabled(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetResetCmdEnabled(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o ShowDebugLevel do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o ShowDebugLevel do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setShowDebugLevel")]
        [HttpPost]
        public async Task<IHttpActionResult> SetShowDebugLevel(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetShowDebugLevel(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o ShowTime do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o ShowTime do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setShowTime")]
        [HttpPost]
        public async Task<IHttpActionResult> SetShowTime(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetShowTime(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o ShowProfiler do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o ShowProfiler do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setShowProfiler")]
        [HttpPost]
        public async Task<IHttpActionResult> SetShowProfiler(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetShowProfiler(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o ShowColors do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o ShowColors do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setShowColors")]
        [HttpPost]
        public async Task<IHttpActionResult> SetShowColors(DeviceDebugSetValueRequestContract contract)
        {
            await _deviceDebugProducer.SetShowColors(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}