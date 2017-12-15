namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Contract;

    [Authorize]
    [RoutePrefix("api/sensorInDevice")]
    public class SensorInDeviceController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly ISensorInDeviceProducer _sensorInDeviceProducer;

        #endregion Fields

        #region Constructors

        public SensorInDeviceController(ISensorInDeviceProducer sensorInDeviceProducer)
        {
            _sensorInDeviceProducer = sensorInDeviceProducer;
        }

        #endregion Constructors

        /// <summary>
        /// Altera a de um sensor em um device
        /// </summary>
        /// <remarks>
        /// Altera a de um sensor em um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setOrdination")]
        [HttpPost]
        public async Task<IHttpActionResult> SetOrdination(SensorInDeviceSetOrdinationRequestContract contract)
        {
            await _sensorInDeviceProducer.SetOrdination(CreateMessage(contract));
            return Ok();
        }
    }
}