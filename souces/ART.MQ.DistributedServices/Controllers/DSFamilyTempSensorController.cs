using MassTransit;
using System.Threading.Tasks;
using System.Web.Http;
using ART.MQ.Common.QueueNames;
using ART.MQ.Common.Contracts.DSFamilyTempSensorContracts;
using ART.MQ.DistributedServices.Helpers;
using ART.MQ.DistributedServices.Models.DSFamilyTempSensorModels;

namespace ART.MQ.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : MQApiControllerBase
    {
        #region constructors

        public DSFamilyTempSensorController(IBus bus): base(bus)
        {
            
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
        [Route("setResolution")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResolution(DSFamilyTempSensorSetResolutionModel model)
        {   
            var sendEndpoint = await GetSendEndpoint(DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetResolutionQueue);
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