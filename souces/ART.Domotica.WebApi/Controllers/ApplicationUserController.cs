namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;

    [Authorize]
    [RoutePrefix("api/applicationUser")]
    public class ApplicationUserController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        private readonly IApplicationUserProducer _applicationUserProducer;

        #endregion Fields

        #region Constructors

        public ApplicationUserController(IApplicationUserProducer applicationUserProducer)
        {
            _applicationUserProducer = applicationUserProducer;
        }

        #endregion Constructors
    }
}