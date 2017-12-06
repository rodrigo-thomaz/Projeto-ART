namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;

    [Authorize]
    [RoutePrefix("api/sensorsInDevice")]
    public class SensorsInDeviceController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly ISensorsInDeviceProducer _sensorsInDeviceProducer;

        #endregion Fields

        #region Constructors

        public SensorsInDeviceController(ISensorsInDeviceProducer sensorsInDeviceProducer)
        {
            _sensorsInDeviceProducer = sensorsInDeviceProducer;
        }

        #endregion Constructors
    }
}