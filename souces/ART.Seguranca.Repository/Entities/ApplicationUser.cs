namespace ART.Seguranca.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        #region Properties

        public ICollection<UsersInApplication> UsersInApplication
        {
            get; set;
        }

        #endregion Properties
    }
}