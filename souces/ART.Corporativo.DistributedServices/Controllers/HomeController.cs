using System.Collections.Generic;
using System.Web.Http;

namespace ART.Corporativo.DistributedServices.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
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
