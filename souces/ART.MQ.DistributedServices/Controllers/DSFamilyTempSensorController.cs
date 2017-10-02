using System.Web.Http;
using ART.MQ.DistributedServices.Models;
using ART.Infra.CrossCutting.WebApi;
using ART.MQ.DistributedServices.IProducers;

namespace ART.MQ.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : BaseApiController
    {
        #region private readonly fields

        protected readonly IDSFamilyTempSensorProducer _dsFamilyTempSensorProducer;

        #endregion

        #region constructors

        public DSFamilyTempSensorController(IDSFamilyTempSensorProducer dsFamilyTempSensorProducer) //: base(connection)
        {
            _dsFamilyTempSensorProducer = dsFamilyTempSensorProducer;
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
        [Route("setResolution")]
        [HttpPost]
        public IHttpActionResult SetResolution(DSFamilyTempSensorSetResolutionModel request)
        {
            _dsFamilyTempSensorProducer.SetResolution(request);

            //var contract = new DSFamilyTempSensorSetResolutionContract
            //{
            //    DeviceAddress = request.DeviceAddress,
            //    Value = request.Value,
            //};

            //byte[] payload = SerializationHelpers.SerialiseIntoBinary(contract);
            
            //var model = _connection.CreateModel();

            //model.QueueDeclare(
            //      queue: "DSFamilyTempSensor.SetResolution"
            //    , durable: true
            //    , exclusive: false
            //    , autoDelete: false
            //    , arguments: null);

            //model.QueueDeclare(
            //      queue: "DSFamilyTempSensor.SetHighAlarm"
            //    , durable: true
            //    , exclusive: false
            //    , autoDelete: false
            //    , arguments: null);

            //model.QueueDeclare(
            //      queue: "DSFamilyTempSensor.SetLowAlarm"
            //    , durable: true
            //    , exclusive: false
            //    , autoDelete: false
            //    , arguments: null);

            //IBasicProperties basicProperties = model.CreateBasicProperties();

            ////basicProperties.ContentType = "text/plain";
            //basicProperties.Persistent = true;
            ////basicProperties.DeliveryMode = 2;
            ////basicProperties.Expiration = "36000000";

            //model.BasicPublish("", "DSFamilyTempSensor.SetResolution", null, payload);

            return Ok();
        }

        #endregion
    }
}