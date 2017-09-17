using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace RThomaz.Infra.CrossCutting.Identity.Entities
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
        #region Primitive Properties

        public Guid ApplicationId { get; set; }

        #endregion

        #region constructors

        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }

        #endregion

        #region Navigation Properties

        public Application Application { get; set; }

        #endregion
    }
}