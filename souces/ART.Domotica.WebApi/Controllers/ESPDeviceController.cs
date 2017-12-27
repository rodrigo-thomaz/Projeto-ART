namespace ART.Domotica.WebApi.Controllers
{
    using System.Web.Http;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using System.Threading.Tasks;
    using ART.Domotica.Producer.Interfaces;
    using ART.Domotica.Contract;
    using System.Web.Http.Description;
    using System.Net.Http;
    using ART.Infra.CrossCutting.WebApi;
    using System.Net;
    using System.Net.Http.Headers;

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
        /// Atualiza o código do ESP Device
        /// </summary>        
        /// <remarks>
        /// Atualiza o código do ESP Device
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("checkForUpdates")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(ESPDeviceCheckForUpdatesRPCResponseContract))]
        public async Task<IHttpActionResult> CheckForUpdates()
        {
            var contract = new ESPDeviceCheckForUpdatesRPCRequestContract
            {
                StationMacAddress = Request.GetFirstHeaderValueOrDefault<string>("x-ESP8266-STA-MAC"),
                SoftAPMacAddress = Request.GetFirstHeaderValueOrDefault<string>("x-ESP8266-AP-MAC"),
                FreeSpace = Request.GetFirstHeaderValueOrDefault<long>("x-ESP8266-FREE-SPACE"),
                SketchSize = Request.GetFirstHeaderValueOrDefault<long>("x-ESP8266-SKETCH-SIZE"),
                ChipSize = Request.GetFirstHeaderValueOrDefault<long>("x-ESP8266-CHIP-SIZE"),
                SDKVersion = Request.GetFirstHeaderValueOrDefault<string>("x-ESP8266-SDK-VERSION"),
                Mode = Request.GetFirstHeaderValueOrDefault<string>("x-ESP8266-MODE")
            };

            var data = await _espDeviceProducer.CheckForUpdates(contract);

            if(data.Buffer == null)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NotModified));
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(data.Buffer)
            };

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = ResponseMessage(result);

            return response;
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
        [Route("getAllByApplicationId")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllByApplicationId()
        {
            await _espDeviceProducer.GetAllByApplicationId(CreateMessage());
            return Ok();
        }

        /// <summary>
        /// Altera o Label de um device
        /// </summary>
        /// <remarks>
        /// Altera o Label de um device
        /// </remarks>
        /// <param name="contract">contrato do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("setLabel")]
        [HttpPost]
        public async Task<IHttpActionResult> SetLabel(DeviceSetLabelRequestContract contract)
        {
            await _espDeviceProducer.SetLabel(CreateMessage(contract));
            return Ok();
        }

        #endregion
    }
}