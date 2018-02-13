namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceDisplay")]
    public class DeviceDisplayController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceDisplayProducer _deviceDisplayProducer;        

        #endregion Fields

        #region Constructors

        public DeviceDisplayController(IDeviceDisplayProducer deviceDisplayProducer)
        {
            _deviceDisplayProducer = deviceDisplayProducer;            
        }

        #endregion Constructors

        #region public voids      
        
        /// <summary>
        /// Altera o Enabled do Display de um device
        /// </summary>
        /// <remarks>
        /// Altera o Enabled do Display de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setEnabled")]
        [HttpPost]
        public async Task<IHttpActionResult> SetEnabled(DeviceDisplaySetValueRequestContract contract)
        {
            await _deviceDisplayProducer.SetEnabled(CreateMessage(contract));
            return Ok();
        }       

        #endregion
    }
}