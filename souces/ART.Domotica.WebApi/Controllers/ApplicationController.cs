namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.WebApi.IProducers;
    using ART.Infra.CrossCutting.MQ.WebApi;

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
    }
}