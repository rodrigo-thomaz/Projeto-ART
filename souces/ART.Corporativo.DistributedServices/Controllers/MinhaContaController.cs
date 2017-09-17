using System.Collections.Generic;
using System.Web.Http;

namespace ART.Corporativo.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/minhaConta")]
    public class MinhaContaController : ApiController
    {
        [Route("")]
        public IEnumerable<object> Get()
        {
            yield return new
            {
                Type = "Type1",
                Value = "Value"
            };
        }
    }
}
