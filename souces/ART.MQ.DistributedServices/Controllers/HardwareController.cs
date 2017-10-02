using ART.MQ.DistributedServices.Models;
using ART.Infra.CrossCutting.WebApi;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ART.MQ.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/termometro")]
    public class HardwareController :  BaseApiController
    {
        // https://imasters.com.br/apis/api-rest-com-workflow-async-e-masstransit/?trace=1519021197&source=single


        /// <summary>
        /// Retorna uma lista de hardawares do space
        /// </summary>
        /// <remarks>
        /// Retorna uma lista de hardawares do space
        /// </remarks>
        /// <param name="request">model do request</param>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(HardwareInSpaceGetResponse))]
        [Route("getHardwareInSpace")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHardwareInSpace(HardwareInSpaceGetRequest request)
        {
            try
            {
                //Verifica se há erros
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    //Retorna a lista de erros
                    return BadRequest(message);
                }

                ////Conecta no barramento de servios
                //var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                //{
                //    var host = sbc.Host(new Uri("rabbitmq://file-server/"), h =>
                //    {
                //        h.Username("test");
                //        h.Password("test");
                //    });
                //});
                ////Inicia o barramento
                //await bus.StartAsync();

                //var task = bus.Publish(request);

                //task.Wait();

                ////Cria a resposta
                //var response = new HardwareInSpaceGetResponse()
                //{
                //    TransactionKey = request.SpaceId
                //};
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}