namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;
    using System.Web.Http.Description;

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
        /// Retornar todos os ESP Devices para o Admin
        /// </summary>        
        /// <remarks>
        /// Retornar todos os de ESP Devices para o Admin
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {
            await _espDeviceProducer.GetAll(CreateMessage());
            return Ok();
        }

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
        [ResponseType(typeof(ESPDeviceGetConfigurationsRPCResponseContract))]
        public async Task<IHttpActionResult> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract)
        {
            var result = await _espDeviceProducer.GetConfigurationsRPC(contract);  
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
        public async Task<IHttpActionResult> GetByPin(ESPDeviceGetByPinRequestContract contract)
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
        public async Task<IHttpActionResult> InsertInApplication(ESPDeviceInsertInApplicationRequestContract contract)
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
        public async Task<IHttpActionResult> DeleteFromApplication(ESPDeviceDeleteFromApplicationRequestContract contract)
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

        /// <summary>
        /// Altera o TimeZone de um device
        /// </summary>
        /// <remarks>
        /// Altera o TimeZone de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setTimeZone")]
        [HttpPost]
        public async Task<IHttpActionResult> SetTimeZone(ESPDeviceSetTimeZoneRequestContract contract)
        {
            await _espDeviceProducer.SetTimeZone(CreateMessage(contract));
            return Ok();
        }

        /// <summary>
        /// Altera o UpdateIntervalInMilliSecond de um device
        /// </summary>
        /// <remarks>
        /// Altera o UpdateIntervalInMilliSecond de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setUpdateIntervalInMilliSecond")]
        [HttpPost]
        public async Task<IHttpActionResult> SetUpdateIntervalInMilliSecond(ESPDeviceSetUpdateIntervalInMilliSecondRequestContract contract)
        {
            await _espDeviceProducer.SetUpdateIntervalInMilliSecond(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}