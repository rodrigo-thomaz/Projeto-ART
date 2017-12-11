using System.Web.Http;
using System.Threading.Tasks;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/hardware")]    
    public class HardwareController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IHardwareProducer _hardwareProducer;

        #endregion

        #region constructors

        public HardwareController(IHardwareProducer hardwareProducer) 
        {
            _hardwareProducer = hardwareProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Altera o rótulo de um hardware
        /// </summary>
        /// <remarks>
        /// Altera o rótulo de um hardware
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setLabel")]
        [HttpPost]
        public async Task<IHttpActionResult> SetLabel(HardwareSetLabelRequestContract contract)
        {
            await _hardwareProducer.SetLabel(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}