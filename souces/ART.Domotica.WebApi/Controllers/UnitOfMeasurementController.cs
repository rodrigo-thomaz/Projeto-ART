using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/unitOfMeasurement")]    
    public class UnitOfMeasurementController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IUnitOfMeasurementProducer _unitOfMeasurementProducer;

        #endregion

        #region constructors

        public UnitOfMeasurementController(IUnitOfMeasurementProducer unitOfMeasurementProducer) //: base(connection)
        {
            _unitOfMeasurementProducer = unitOfMeasurementProducer;
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
            await _unitOfMeasurementProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}