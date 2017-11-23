namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class ApplicationMQ : IEntity<Guid>
    {
        #region Properties

        public Application Application
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string Topic
        {
            get; set;
        }

        public string User
        {
            get; set;
        }

        #endregion Properties
    }
}