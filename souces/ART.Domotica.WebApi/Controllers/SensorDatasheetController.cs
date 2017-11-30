using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorDatasheet")]    
    public class SensorDatasheetController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorDatasheetProducer _sensorDatasheetProducer;

        #endregion

        #region constructors

        public SensorDatasheetController(ISensorDatasheetProducer sensorDatasheetProducer) 
        {
            _sensorDatasheetProducer = sensorDatasheetProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de datasheet dos sensores
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de datasheet dos sensores
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _sensorDatasheetProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}