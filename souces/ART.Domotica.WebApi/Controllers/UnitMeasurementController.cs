using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/unitMeasurement")]    
    public class UnitMeasurementController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IUnitMeasurementProducer _unitMeasurementProducer;

        #endregion

        #region constructors

        public UnitMeasurementController(IUnitMeasurementProducer unitMeasurementProducer) //: base(connection)
        {
            _unitMeasurementProducer = unitMeasurementProducer;
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
            await _unitMeasurementProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}