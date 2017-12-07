namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;

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
    }
}