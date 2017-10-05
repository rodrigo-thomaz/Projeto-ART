namespace ART.Corporativo.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("api/home")]
    public class HomeController : ApiController
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