using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorChartLimiter")]    
    public class SensorChartLimiterController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorChartLimiterProducer _sensorChartLimiterProducer;

        #endregion

        #region constructors

        public SensorChartLimiterController(ISensorChartLimiterProducer sensorChartLimiterProducer) 
        {
            _sensorChartLimiterProducer = sensorChartLimiterProducer;
        }

        #endregion

        #region public voids       

        /// <summary>
        /// Altera o valor do limite do gráfico de um sensor
        /// </summary>
        /// <remarks>
        /// Altera o valor do limite do gráfico de um sensor
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setValue")]
        [HttpPost]
        public async Task<IHttpActionResult> SetValue(SensorChartLimiterSetValueRequestContract contract)
        {
            await _sensorChartLimiterProducer.SetValue(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}