using ART.Seguranca.Domain.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace ART.Seguranca.DistributedServices.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private readonly IAuthDomain _authDomain;

        public RefreshTokensController(IAuthDomain authDomain)
        {
            _authDomain = authDomain;
        }

        [Authorize(Users="Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_authDomain.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _authDomain.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authDomain.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
