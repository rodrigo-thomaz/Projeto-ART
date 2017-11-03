namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;
    using System.Web.Http.Description;
    using ART.Domotica.Constant;
    using ART.Infra.CrossCutting.Setting;

    [Authorize]
    [RoutePrefix("api/espDevice")]
    public class ESPDeviceController : AuthenticatedMQApiControllerBase
    {
        #region Fields

        protected readonly IESPDeviceProducer _espDeviceProducer;        

        #endregion Fields

        #region Constructors

        public ESPDeviceController(IESPDeviceProducer espDeviceProducer)
        {
            _espDeviceProducer = espDeviceProducer;            
        }

        #endregion Constructors

        #region public voids

        /// <summary>
        /// Retornar as configurações de um ESP Device
        /// </summary>        
        /// <remarks>
        /// Retornar as configurações de um ESP Device
        /// </remarks>
        /// <param name="request">Parâmetros da busca</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getConfigurations")]
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(ESPDeviceGetConfigurationsResponseContract))]
        public async Task<IHttpActionResult> GetConfigurations(ESPDeviceGetConfigurationsRequestContract contract)
        {
            var result = await _espDeviceProducer.GetConfigurations(contract);  
            return Ok(result);
        }

        /// <summary>
        /// Retornar um ESP Device pelo pin
        /// </summary>        
        /// <remarks>
        /// Retornar um ESP Device pelo pin
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getByPin")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByPin(ESPDevicePinContract contract)
        {
            await _espDeviceProducer.GetByPin(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Adiciona um ESP Device pelo pin
        /// </summary>        
        /// <remarks>
        /// Adiciona um ESP Device pelo pin
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("insertInApplication")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertInApplication(ESPDevicePinContract contract)
        {
            await _espDeviceProducer.InsertInApplication(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Remove um ESP Device pelo id
        /// </summary>        
        /// <remarks>
        /// Remove um ESP Device pelo id
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("deleteFromApplication")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteFromApplication(ESPDeviceDeleteFromApplicationContract contract)
        {
            await _espDeviceProducer.DeleteFromApplication(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Retornar uma lista de ESP Devices da aplicação
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de ESP Devices da aplicação
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getListInApplication")]
        [HttpPost]
        public async Task<IHttpActionResult> GetListInApplication()
        {
            await _espDeviceProducer.GetListInApplication(CreateMessage());
            return Ok();
        }

        #endregion
    }
}