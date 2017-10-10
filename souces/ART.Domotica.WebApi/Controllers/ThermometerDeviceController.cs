namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Infra.CrossCutting.MQ.WebApi;
    using ART.Domotica.Producer.Interfaces;
    using System.Threading.Tasks;

    [Authorize]
    [RoutePrefix("api/thermometerDevice")]
    public class ThermometerDeviceController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IThermometerDeviceProducer _thermometerDeviceProducer;

        #endregion Fields

        #region Constructors

        public ThermometerDeviceController(IThermometerDeviceProducer thermometerDeviceProducer)
        {
            _thermometerDeviceProducer = thermometerDeviceProducer;
        }

        #endregion Constructors

        #region public voids

        /// <summary>
        /// Retornar uma lista de ThermometerDevice
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de ThermometerDevice
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getList")]
        [HttpPost]
        public async Task<IHttpActionResult> GetList()
        {
            await _thermometerDeviceProducer.GetList(CreateMessage());
            return Ok();
        }

        #endregion
    }
}