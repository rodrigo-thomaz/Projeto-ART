namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;

    [Authorize]
    [RoutePrefix("api/hardware")]
    public class HardwareController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IHardwareProducer _hardwareProducer;

        #endregion Fields

        #region Constructors

        public HardwareController(IHardwareProducer hardwareProducer)
        {
            _hardwareProducer = hardwareProducer;
        }

        #endregion Constructors

        #region public voids

        /// <summary>
        /// Retornar uma lista de hardwares
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de hardwares
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getList")]
        [HttpPost]
        public async Task<IHttpActionResult> GetList()
        {
            await _hardwareProducer.GetList(CreateMessage());
            return Ok();
        }

        #endregion
    }
}