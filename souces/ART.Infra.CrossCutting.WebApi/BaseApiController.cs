using System;
using System.Linq;
using System.Text;
using System.Web.Http;


namespace ART.Infra.CrossCutting.WebApi
{
    public abstract class BaseApiController : ApiController
    {
        protected void ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                var sb = new StringBuilder();
                foreach (var item in allErrors)
                {
                    sb.AppendLine(item.ErrorMessage);
                }
                throw new ArgumentException(sb.ToString());
            }
        }
    }
}