using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace RThomaz.Web.Helpers
{
    public class AuthenticatedController : Controller
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
    }
}