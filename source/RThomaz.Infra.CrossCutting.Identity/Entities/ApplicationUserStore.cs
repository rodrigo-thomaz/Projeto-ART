using RThomaz.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace RThomaz.Infra.CrossCutting.Identity.Entities
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, Guid,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}