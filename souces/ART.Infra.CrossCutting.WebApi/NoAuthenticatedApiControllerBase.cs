namespace ART.Infra.CrossCutting.WebApi
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    public abstract class NoAuthenticatedApiControllerBase : ApiController
    {
        #region Methods

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

        #endregion Methods
    }
}