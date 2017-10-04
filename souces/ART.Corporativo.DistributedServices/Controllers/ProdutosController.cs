namespace ART.Corporativo.DistributedServices.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("api/produtos")]
    public class ProdutosController : ApiController
    {
        #region Methods

        [Route("")]
        public IEnumerable<object> Get()
        {
            yield return new
            {
                Type = "Type1",
                Value = "Value"
            };
        }

        #endregion Methods
    }
}