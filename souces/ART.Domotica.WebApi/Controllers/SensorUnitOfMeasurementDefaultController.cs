using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/sensorUnitOfMeasurementDefault")]    
    public class SensorUnitOfMeasurementDefaultController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ISensorUnitOfMeasurementDefaultProducer _sensorUnitOfMeasurementDefaultProducer;

        #endregion

        #region constructors

        public SensorUnitOfMeasurementDefaultController(ISensorUnitOfMeasurementDefaultProducer sensorUnitOfMeasurementDefaultProducer) 
        {
            _sensorUnitOfMeasurementDefaultProducer = sensorUnitOfMeasurementDefaultProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma padrões de unidade de medida dos sensores
        /// </summary>        
        /// <remarks>
        /// Retornar uma padrões de unidade de medida dos sensores
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _sensorUnitOfMeasurementDefaultProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}