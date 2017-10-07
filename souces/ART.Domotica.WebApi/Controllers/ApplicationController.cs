namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;

    [Authorize]
    [RoutePrefix("api/application")]
    public class ApplicationController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IApplicationProducer _applicationProducer;

        #endregion Fields

        #region Constructors

        //: base(connection)
        public ApplicationController(IApplicationProducer applicationProducer)
        {
            _applicationProducer = applicationProducer;
        }

        #endregion Constructors

        #region public voids

        /// <summary>
        /// Retornar a aplicação do usuário
        /// </summary>        
        /// <remarks>
        /// Retornar a aplicação do usuário
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("get")]
        [HttpPost]
        public async Task<IHttpActionResult> Get()
        {
            await _applicationProducer.Get(CreateMessage());
            return Ok();
        }

        #endregion
    }
}