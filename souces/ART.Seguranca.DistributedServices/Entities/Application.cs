namespace ART.Seguranca.DistributedServices.Entities
{
    using System;
    using System.Collections.Generic;

    public class Application
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<UsersInApplication> UsersInApplication
        {
            get; set;
        }

        #endregion Properties
    }
}