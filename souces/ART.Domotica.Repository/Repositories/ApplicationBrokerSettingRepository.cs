namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationBrokerSettingRepository : RepositoryBase<ARTDbContext, ApplicationBrokerSetting, Guid>, IApplicationBrokerSettingRepository
    {
        #region Constructors

        public ApplicationBrokerSettingRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}