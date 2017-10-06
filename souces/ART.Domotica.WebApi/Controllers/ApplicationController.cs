namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.WebApi.IProducers;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;

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
        /// Retornar uma lista de aplicações do usuário
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de aplicações do usuário
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {
            await _applicationProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}