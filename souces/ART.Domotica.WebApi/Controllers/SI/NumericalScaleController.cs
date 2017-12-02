using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.WebApi.Controllers.SI
{
    [Authorize]
    [RoutePrefix("api/si/numericalScale")]    
    public class NumericalScaleController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly INumericalScaleProducer _numericalScaleProducer;

        #endregion

        #region constructors

        public NumericalScaleController(INumericalScaleProducer numericalScaleProducer) 
        {
            _numericalScaleProducer = numericalScaleProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de escalas numéricas
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de escalas numéricas
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _numericalScaleProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}