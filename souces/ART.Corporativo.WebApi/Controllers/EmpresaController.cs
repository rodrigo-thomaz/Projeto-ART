namespace ART.Corporativo.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
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