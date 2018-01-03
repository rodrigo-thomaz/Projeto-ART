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
        /// Altera o Active do Debug de um device
        /// </summary>
        /// <remarks>
        /// Altera o Active do Debug de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setActive")]
        [HttpPost]
        public async Task<IHttpActionResult> SetActive(DeviceDebugSetActiveRequestContract contract)
        {
            await _deviceDebugProducer.SetActive(CreateMessage(contract));
            return Ok();
        }
        
        #endregion
    }
}