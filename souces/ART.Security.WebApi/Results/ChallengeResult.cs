namespace ART.Security.WebApi.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    public class ChallengeResult : IHttpActionResult
    {
        #region Constructors

        public ChallengeResult(string loginProvider, ApiController controller)
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
        }

        #endregion Constructors

        #region Properties

        public string LoginProvider
        {
            get; set;
        }

        public HttpRequestMessage Request
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }

        #endregion Methods
    }
}