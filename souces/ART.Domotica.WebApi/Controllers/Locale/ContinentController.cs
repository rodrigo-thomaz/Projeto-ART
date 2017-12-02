using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.Locale;

namespace ART.Domotica.WebApi.Controllers.Locale
{
    [Authorize]
    [RoutePrefix("api/locale/continent")]    
    public class ContinentController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IContinentProducer _continentProducer;

        #endregion

        #region constructors

        public ContinentController(IContinentProducer continentProducer) 
        {
            _continentProducer = continentProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de Continentes
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de Continentes
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _continentProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}