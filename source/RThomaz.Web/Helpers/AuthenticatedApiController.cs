using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace RThomaz.Web.Helpers
{
    public class AuthenticatedApiController : ApiController
    {
        public long UsuarioId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type.Equals("usuarioId")).Value);
            }
        }

        public long AplicacaoId
        {
            get
            {
                return long.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type.Equals("aplicacaoId")).Value);
            }
        }

        public string StorageBucketName
        {
            get
            {
                return ((ClaimsIdentity)User.Identity).Claims.First(x => x.Type.Equals("storageBucketName")).Value;
            }
        }

        public string NomeExibicao
        {
            get
            {
                return ((ClaimsIdentity)User.Identity).Claims.First(x => x.Type.Equals("nomeExibicao")).Value;
            }
        }

        public string Email
        {
            get
            {
                return ((ClaimsIdentity)User.Identity).Claims.First(x => x.Type.Equals("email")).Value;
            }
        }

        public bool LembrarMe
        {
            get
            {
                return bool.Parse(((ClaimsIdentity)User.Identity).Claims.First(x => x.Type.Equals("lembrarMe")).Value);
            }
        }

        public string AvatarStorageObject
        {
            get
            {
                var claimsIdentity = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type.Equals("avatarStorageObject"));
                return claimsIdentity != null ? claimsIdentity.Value : null;
            }
        }

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