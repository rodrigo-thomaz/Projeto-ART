using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.Globalization;

namespace ART.Domotica.WebApi.Controllers.Globalization
{
    [Authorize]
    [RoutePrefix("api/globalization/timeZone")]    
    public class TimeZoneController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ITimeZoneProducer _timeZoneProducer;

        #endregion

        #region constructors

        public TimeZoneController(ITimeZoneProducer timeZoneProducer) 
        {
            _timeZoneProducer = timeZoneProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de Time Zones
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de Time Zones
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _timeZoneProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}