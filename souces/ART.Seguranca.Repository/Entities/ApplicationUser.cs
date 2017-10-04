namespace ART.Seguranca.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IEntity<Guid>
    {
        #region Properties

        public ICollection<UsersInApplication> UsersInApplication
        {
            get; set;
        }

        #endregion Properties
    }
}