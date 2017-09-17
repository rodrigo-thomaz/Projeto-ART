using RThomaz.Web.Helpers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RThomaz.Infra.CrossCutting.Identity.Models;
using System.Security.Claims;
using RThomaz.Infra.CrossCutting.Identity.Managers;
using Microsoft.AspNet.Identity.Owin;
using RThomaz.Application.Financeiro.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using System;
using RThomaz.Infra.CrossCutting.Identity.Entities;

namespace RThomaz.Web.Controllers.APIs
{
    [RoutePrefix("api/account")]
    [ThrowingAuthorize]
    public class AccountController : AuthenticatedApiController
    {
        #region private readonly fields

        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IAccountAppService _accountAppService;

        #endregion

        #region constructors

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAccountAppService accountAppService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountAppService = accountAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Login do usuário no sistema
        /// </summary>
        /// <remarks>
        /// Efetua o login do usuário no sistema
        /// 
        /// </remarks>
        /// <param name="model">model do login</param>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(SignInStatus))]
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            ValidateModelState();

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, shouldLockout: false);

            if(result == SignInStatus.Success)
            {
                await AddUserClaimAsync(model.Email, model.Senha);
            }            

            return Ok(result);
        }

        /// <summary>
        /// Altera a senha
        /// </summary>
        /// <remarks>
        /// Alterar a senha do usuário
        /// 
        /// </remarks>
        /// <param name="model">model com as senhas</param>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(IdentityResult))]
        [Route("changePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ValidateModelState();

            var userId = Guid.Parse(User.Identity.GetUserId());
            var result = await _userManager.ChangePasswordAsync(userId, model.SenhaAntiga, model.NovaSenha);
            return Ok(result);
        }

        /// <summary>
        /// Efetua LogOff do usuário no sistema
        /// </summary>
        /// <remarks>
        /// Efetua LogOff do usuário no sistema
        /// 
        /// </remarks>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("logOff")]
        [HttpPost]
        public IHttpActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Ok();
        }

        /// <summary>
        /// Registrar um novo usuário no sistema
        /// </summary>
        /// <remarks>
        /// Registrar um novo usuário no sistema
        /// 
        /// </remarks>
        /// <param name="model">model com os dados do novo usuário</param>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(IdentityResult))]
        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            ValidateModelState();

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _accountAppService.Register(user.Id, model.Email, model.Senha);

                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                await AddUserClaimAsync(model.Email, model.Senha);

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                //var callbackUrl = "Url.Action('ConfirmEmail', 'Account', new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);";
                //await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");                
            }

            return Ok(result);
        }

        /// <summary>
        /// Esqueçeu sua senha?
        /// </summary>
        /// <remarks>
        /// Esqueçeu sua senha?
        /// 
        /// </remarks>
        /// <param name="email">email do usuário</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(bool))]
        [Route("forgotYourPassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ForgotYourPassword(string email)
        {
            var success = await _accountAppService.ForgotYourPassword(email);
            return Ok(success);
        }

        #endregion

        #region private voids

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private async Task AddUserClaimAsync(string email, string senha)
        {
            var user = await _userManager.FindAsync(email, senha);

            var detail = await _accountAppService.GetDetail(email);

            await _userManager.AddClaimAsync(user.Id, new Claim("usuarioId", detail.UsuarioId.ToString()));
            await _userManager.AddClaimAsync(user.Id, new Claim("aplicacaoId", detail.AplicacaoId.ToString()));
            await _userManager.AddClaimAsync(user.Id, new Claim("storageBucketName", detail.StorageBucketName));
            await _userManager.AddClaimAsync(user.Id, new Claim("nomeExibicao", detail.NomeExibicao));
            await _userManager.AddClaimAsync(user.Id, new Claim("email", detail.Email));
            await _userManager.AddClaimAsync(user.Id, new Claim("lembrarMe", "True"));
            await _userManager.AddClaimAsync(user.Id, new Claim("avatarStorageObject", detail.AvatarStorageObject ?? ""));
        }

        #endregion
    }
}