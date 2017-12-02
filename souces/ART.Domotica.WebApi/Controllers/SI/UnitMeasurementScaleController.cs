using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.WebApi.Controllers.SI
{
    [Authorize]
    [RoutePrefix("api/si/unitMeasurementScale")]    
    public class UnitMeasurementScaleController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IUnitMeasurementScaleProducer _unitMeasurementScaleProducer;

        #endregion

        #region constructors

        public UnitMeasurementScaleController(IUnitMeasurementScaleProducer unitMeasurementScaleProducer) 
        {
            _unitMeasurementScaleProducer = unitMeasurementScaleProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de escalas da unidade de medida
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de escalas da unidade de medida
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _unitMeasurementScaleProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}