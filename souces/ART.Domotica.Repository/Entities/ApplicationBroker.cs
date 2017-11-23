namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class ApplicationBroker : IEntity<Guid>
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

        public string Topic
        {
            get; set;
        }

        #endregion Properties
    }
}