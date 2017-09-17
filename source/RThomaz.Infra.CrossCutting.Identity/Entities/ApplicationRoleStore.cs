using RThomaz.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace RThomaz.Infra.CrossCutting.Identity.Entities
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}