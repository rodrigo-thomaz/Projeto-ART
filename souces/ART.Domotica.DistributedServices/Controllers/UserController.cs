using ART.Domotica.DistributedServices.Entities;
using ART.Domotica.DistributedServices.Models;
using ART.Domotica.DistributedServices.Repositories;
using ART.Infra.CrossCutting.WebApi;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ART.Domotica.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        #region private readonly fields

        private readonly UserRepository _userRepository;

        #endregion

        #region constructors

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        #endregion

        #region public voids

        /// <summary>
        /// Inserir um usuário
        /// </summary>
        /// <remarks>
        /// Inseri um novo usuário
        /// </remarks>
        /// <param name="model">model do usuário</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(UserCreateModel))]
        [Route("insert")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Insert(UserCreateModel model)
        {
            ValidateModelState();
            var user = new User
            {
                Id = model.Id,
            };
            await _userRepository.Insert(user);
            return Ok();
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            _userRepository.Dispose();
            base.Dispose(disposing);
        } 
        #endregion
    }
}