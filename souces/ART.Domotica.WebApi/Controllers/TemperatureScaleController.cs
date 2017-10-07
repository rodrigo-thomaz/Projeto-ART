using System.Web.Http;
using ART.Domotica.WebApi.IProducers;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/temperatureScale")]    
    public class TemperatureScaleController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ITemperatureScaleProducer _temperatureScaleProducer;

        #endregion

        #region constructors

        public TemperatureScaleController(ITemperatureScaleProducer temperatureScaleProducer) //: base(connection)
        {
            _temperatureScaleProducer = temperatureScaleProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de escalas
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de escalas
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _temperatureScaleProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}