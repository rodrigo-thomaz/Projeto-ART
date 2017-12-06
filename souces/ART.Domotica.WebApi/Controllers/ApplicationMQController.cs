namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;

    [Authorize]
    [RoutePrefix("api/applicationMQ")]
    public class ApplicationMQController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IApplicationMQProducer _applicationMQProducer;

        #endregion Fields

        #region Constructors

        //: base(connection)
        public ApplicationMQController(IApplicationMQProducer applicationMQProducer)
        {
            _applicationMQProducer = applicationMQProducer;
        }

        #endregion Constructors
    }
}