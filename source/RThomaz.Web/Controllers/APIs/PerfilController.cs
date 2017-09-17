using RThomaz.Web.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RThomaz.Application.Financeiro.Models;
using System.Web.Http.Description;
using RThomaz.Web.Helpers.Storage;
using System.Collections.Generic;
using RThomaz.Application.Financeiro.Interfaces;

namespace RThomaz.Web.Controllers.APIs
{
    [RoutePrefix("api/perfil")]
    [ThrowingAuthorize]
    public class PerfilController : AuthenticatedApiController
    {
        #region private readonly fields

        private readonly IPerfilAppService _perfilAppService;

        #endregion

        #region constructors

        public PerfilController(IPerfilAppService perfilAppService)
        {
            _perfilAppService = perfilAppService;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar informações pessoais de um perfil
        /// </summary>        
        /// <remarks>
        /// Retorna informações pessoais de um único perfil de um usuário logado
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PerfilPersonalInfoViewModel))]
        [Route("personalinfo")]
        public async Task<IHttpActionResult> Get()
        {
            var model = await _perfilAppService.GetDetail(UsuarioId);
            return Ok(model);
        }

        /// <summary>
        /// Retornar informações do header de um usuário logado
        /// </summary>        
        /// <remarks>
        /// Retorna informações do header de tela de um usuário logado
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(KeyValuePair<string, string>))]
        [Route("headeruserinfo")]
        public IHttpActionResult GetHeaderUserInfo()
        {
            var result = new KeyValuePair<string, string>(AvatarStorageObject, NomeExibicao);
            return Ok(result);
        }

        /// <summary>
        /// Retornar o avatar de um usuário
        /// </summary>        
        /// <remarks>
        /// Retorna o avatar de um único usuário existente por id
        /// </remarks>
        /// <param name="id">id do avatar</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("avatar/{id}")]
        public async Task<HttpResponseMessage> GetAvatar(string id)
        {
            var dto = await _perfilAppService.GetAvatar(id);
            var result = new StorageHttpResponseMessage(dto);
            return result;
        }

        /// <summary>
        /// Atualizar um perfil
        /// </summary>
        /// <remarks>
        /// Atualiza um único perfil existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do perfil</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(PerfilPersonalInfoViewModel))]
        [Route("personalinfo")]
        public async Task<IHttpActionResult> Put(PerfilPersonalInfoEditModel model)
        {
            ValidateModelState();
            var viewModel = await _perfilAppService.EditPersonalInfo(model);
            User.AddUpdateClaim("nomeExibicao", viewModel.NomeExibicao ?? string.Empty);
            return Ok(viewModel);
        }

        /// <summary>
        /// Atualizar um avatar de um perfil
        /// </summary>
        /// <remarks>
        /// Atualiza um único avatar de um perfil existente por id
        /// 
        /// </remarks>
        /// <param name="model">model do avatar</param>        
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ResponseType(typeof(string))]
        [Route("avatar")]
        public async Task<IHttpActionResult> Put(PerfilAvatarEditModel model)
        {
            ValidateModelState();
            var storageObject = await _perfilAppService.EditAvatar(model);
            User.AddUpdateClaim("avatarStorageObject", storageObject ?? string.Empty);
            return Ok(storageObject);
        }

        #endregion
    }
}