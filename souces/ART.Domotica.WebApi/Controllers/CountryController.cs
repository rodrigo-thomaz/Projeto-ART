using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/country")]    
    public class CountryController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly ICountryProducer _countryProducer;

        #endregion

        #region constructors

        public CountryController(ICountryProducer countryProducer) 
        {
            _countryProducer = countryProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de Paises
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de Paises
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _countryProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}