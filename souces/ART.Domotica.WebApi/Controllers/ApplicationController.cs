namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;

    using ART.Domotica.WebApi.IProducers;
    using ART.Infra.CrossCutting.WebApi;

    [Authorize]
    [RoutePrefix("api/application")]
    public class ApplicationController : AuthenticatedApiController
    {
        #region Fields

        protected readonly IDSFamilyTempSensorProducer _dsFamilyTempSensorProducer;

        #endregion Fields

        #region Constructors

        //: base(connection)
        public ApplicationController(IDSFamilyTempSensorProducer dsFamilyTempSensorProducer)
        {
            _dsFamilyTempSensorProducer = dsFamilyTempSensorProducer;
        }

        #endregion Constructors
    }
}