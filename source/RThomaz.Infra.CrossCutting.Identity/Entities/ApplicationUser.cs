using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using RThomaz.Infra.CrossCutting.Identity.Managers;
using System;

namespace RThomaz.Infra.CrossCutting.Identity.Entities
{
    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region Primitive Properties

        public Guid ApplicationId { get; set; }

        public string DisplayName { get; set; }

        public string AvatarStorageObject { get; set; }

        public bool RememberMe { get; set; }

        #endregion

        #region public voids

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here    

            return userIdentity;
        }

        #endregion

        #region Navigation Properties

        public Application Application { get; set; }

        #endregion
    }
}