namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;

    [Authorize]
    [RoutePrefix("api/applicationBrokerSetting")]
    public class ApplicationBrokerSettingController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IApplicationBrokerSettingProducer _applicationBrokerSettingProducer;

        #endregion Fields

        #region Constructors

        //: base(connection)
        public ApplicationBrokerSettingController(IApplicationBrokerSettingProducer applicationBrokerSettingProducer)
        {
            _applicationBrokerSettingProducer = applicationBrokerSettingProducer;
        }

        #endregion Constructors

        #region public voids

        /// <summary>
        /// Retornar as configurações do broker da aplicação Web
        /// </summary>        
        /// <remarks>
        /// Retornar as configurações do broker da aplicação Web
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("get")]
        [HttpPost]
        public async Task<IHttpActionResult> Get()
        {
            await _applicationBrokerSettingProducer.Get(CreateMessage());
            return Ok();
        }

        #endregion
    }
}