namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceSensors")]
    public class DeviceSensorsController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceSensorsProducer _deviceSensorsProducer;

        #endregion Fields

        #region Constructors

        public DeviceSensorsController(IDeviceSensorsProducer deviceSensorsProducer)
        {
            _deviceSensorsProducer = deviceSensorsProducer;
        }

        #endregion Constructors

        /// <summary>
        /// Altera o ReadIntervalInMilliSeconds de um device
        /// </summary>
        /// <remarks>
        /// Altera o ReadIntervalInMilliSeconds de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setReadIntervalInMilliSeconds")]
        [HttpPost]
        public async Task<IHttpActionResult> SetReadIntervalInMilliSeconds(DeviceSetIntervalInMilliSecondsRequestContract contract)
        {
            await _deviceSensorsProducer.SetReadIntervalInMilliSeconds(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o PublishIntervalInMilliSeconds de um device
        /// </summary>
        /// <remarks>
        /// Altera o PublishIntervalInMilliSeconds de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setPublishIntervalInMilliSeconds")]
        [HttpPost]
        public async Task<IHttpActionResult> SetPublishIntervalInMilliSeconds(DeviceSetIntervalInMilliSecondsRequestContract contract)
        {
            await _deviceSensorsProducer.SetPublishIntervalInMilliSeconds(CreateMessage(contract));
            return Ok();
        }
    }
}