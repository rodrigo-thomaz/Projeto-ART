using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ART.Corporativo.DistributedServices.Controllers
{
    [RoutePrefix("api/contato")]
    public class ContatoController : ApiController
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
