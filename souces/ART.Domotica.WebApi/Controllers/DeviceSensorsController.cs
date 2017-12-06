namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.WebApi;

    [Authorize]
    [RoutePrefix("api/deviceSensors")]
    public class DeviceSensorsController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IDeviceSensorsProducer _deviceSensorsProducer;

        #endregion Fields

        #region Constructors

        public DeviceSensorsController(IDeviceSensorsProducer deviceSensorsProducer)
        {
            _deviceSensorsProducer = deviceSensorsProducer;
        }

        #endregion Constructors
    }
}