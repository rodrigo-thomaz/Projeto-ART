using System;
using System.Collections.Generic;

namespace ART.Seguranca.DistributedServices.Entities
{
    public class Application
    {
        #region Primitive Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<UsersInApplication> UsersInApplication { get; set; }

        #endregion
    }
}