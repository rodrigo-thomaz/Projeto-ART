using System;
using System.Collections.Generic;

namespace RThomaz.Infra.CrossCutting.Identity.Entities
{
    public class Application
    {
        #region Primitive Properties

        public Guid ApplicationId { get; set; }

        public string StorageBucketName { get; set; }

        public bool Active { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<ApplicationRole> Roles { get; set; }

        #endregion
    }
}
