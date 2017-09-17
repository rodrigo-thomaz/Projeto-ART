using RThomaz.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using RThomaz.Infra.CrossCutting.Identity.Entities;

namespace RThomaz.Infra.CrossCutting.Identity.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, Guid> roleStore)
            :base(roleStore)
        {
            
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context.Get<ApplicationDbContext>()));
        }
    }
}
