using ART.DistributedServices.Application.Models;
using ART.Infra.CrossCutting.WebApi;
using MassTransit;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ART.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : BaseApiController
    {
        #region private readonly fields

        private readonly IPublishEndpoint _publishEndpoint;

        #endregion

        #region constructors

        public DSFamilyTempSensorController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Altera a resolução de um sensor
        /// </summary>
        /// <remarks>
        /// Altera a resolução de um sensor
        /// </remarks>
        /// <param name="request">model do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(UserCreateModel))]
        [Route("setResolution")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResolution(DSFamilyTempSensorSetResolutionRequest request)
        {
            await _publishEndpoint.Publish(new DSFamilyTempSensorSetResolutionResponse
            {
                DeviceAddress = request.DeviceAddress,
                Value = request.Value,
            });

            return Ok();
        }

        #endregion
    }
}