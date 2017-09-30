using ART.MQ.DistributedServices.Models;
using ART.Infra.CrossCutting.WebApi;
using ART.MQ.Contracts;
using MassTransit;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ART.MQ.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : BaseApiController
    {
        #region private readonly fields

        private readonly IBus _bus;

        #endregion

        #region constructors

        public DSFamilyTempSensorController(IBus bus)
        {
            _bus = bus;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Altera a resolução de um sensor
        /// </summary>
        /// <remarks>
        /// Altera a resolução de um sensor
        /// </remarks>
        /// <param name="model">model do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(UserCreateModel))]
        [Route("setResolution")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResolution(DSFamilyTempSensorSetResolutionModel model)
        {
            var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];

            var sendEndpoint = await _bus.GetSendEndpoint(new Uri(string.Format("rabbitmq://{0}/{1}", hostName, "DSFamilyTempSensorSetResolution")));
            
            await sendEndpoint.Send<DSFamilyTempSensorSetResolutionContract>(new
            {
                DeviceAddress = model.DeviceAddress,
                Value = model.Value,
            });

            return Ok();
        }

        #endregion
    }
}