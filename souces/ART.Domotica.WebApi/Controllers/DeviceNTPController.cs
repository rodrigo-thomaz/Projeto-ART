namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceNTP")]
    public class DeviceNTPController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceNTPProducer _deviceNTPProducer;        

        #endregion Fields

        #region Constructors

        public DeviceNTPController(IDeviceNTPProducer deviceNTPProducer)
        {
            _deviceNTPProducer = deviceNTPProducer;            
        }

        #endregion Constructors

        #region public voids      

        /// <summary>
        /// Altera o TimeZone de um device
        /// </summary>
        /// <remarks>
        /// Altera o TimeZone de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setTimeZone")]
        [HttpPost]
        public async Task<IHttpActionResult> SetTimeZone(DeviceNTPSetTimeZoneRequestContract contract)
        {
            await _deviceNTPProducer.SetTimeZone(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o UpdateIntervalInMilliSecond de um device
        /// </summary>
        /// <remarks>
        /// Altera o UpdateIntervalInMilliSecond de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setUpdateIntervalInMilliSecond")]
        [HttpPost]
        public async Task<IHttpActionResult> SetUpdateIntervalInMilliSecond(DeviceNTPSetUpdateIntervalInMilliSecondRequestContract contract)
        {
            await _deviceNTPProducer.SetUpdateIntervalInMilliSecond(CreateMessage(contract));
            return Ok();
        }
        
        #endregion
    }
}