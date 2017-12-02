using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.WebApi.Controllers.SI
{
    [Authorize]
    [RoutePrefix("api/si/numericalScalePrefix")]    
    public class NumericalScalePrefixController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly INumericalScalePrefixProducer _numericalScalePrefixProducer;

        #endregion

        #region constructors

        public NumericalScalePrefixController(INumericalScalePrefixProducer numericalScalePrefixProducer) 
        {
            _numericalScalePrefixProducer = numericalScalePrefixProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de prefixos de escala numérica
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de prefixos de escala numérica
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _numericalScalePrefixProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}