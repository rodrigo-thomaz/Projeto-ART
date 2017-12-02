using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.WebApi.Controllers.SI
{
    [Authorize]
    [RoutePrefix("api/si/numericalScaleTypeCountry")]    
    public class NumericalScaleTypeCountryController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly INumericalScaleTypeCountryProducer _numericalScaleTypeCountryProducer;

        #endregion

        #region constructors

        public NumericalScaleTypeCountryController(INumericalScaleTypeCountryProducer numericalScaleTypeCountryProducer) 
        {
            _numericalScaleTypeCountryProducer = numericalScaleTypeCountryProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipos de escala numérica do Pais
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipos de escala numérica do Pais
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _numericalScaleTypeCountryProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}