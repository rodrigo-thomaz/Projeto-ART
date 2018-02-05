namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/deviceInApplication")]
    public class DeviceInApplicationController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceInApplicationProducer _deviceInApplicationProducer;

        #endregion Fields

        #region Constructors

        public DeviceInApplicationController(IDeviceInApplicationProducer deviceInApplicationProducer)
        {
            _deviceInApplicationProducer = deviceInApplicationProducer;
        }

        #endregion Constructors

        #region public voids        

        /// <summary>
        /// Adiciona um ESP Device pelo pin
        /// </summary>        
        /// <remarks>
        /// Adiciona um ESP Device pelo pin
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Insert(DeviceInApplicationInsertRequestContract contract)
        {
            await _deviceInApplicationProducer.Insert(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Remove um ESP Device pelo id
        /// </summary>        
        /// <remarks>
        /// Remove um ESP Device pelo id
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("remove")]
        [HttpPost]
        public async Task<IHttpActionResult> Remove(DeviceInApplicationRemoveRequestContract contract)
        {
            await _deviceInApplicationProducer.Remove(CreateMessage(contract));
            return Ok();
        }       

        #endregion
    }
}