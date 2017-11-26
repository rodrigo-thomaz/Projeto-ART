namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using Autofac;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Utils;
    using ART.Infra.CrossCutting.Setting;
    using ART.Infra.CrossCutting.MQ;

    public class ApplicationUserDomain : DomainBase, IApplicationUserDomain
    {
        #region Fields

        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISettingManager _settingsManager;

        #endregion Fields

        #region Constructors

        public ApplicationUserDomain(IComponentContext componentContext, ISettingManager settingsManager)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _applicationUserRepository = new ApplicationUserRepository(context);

            _settingsManager = settingsManager;
        }

        public async Task<ApplicationUser> GetById(Guid applicationUserId)
        {
            var data = await _applicationUserRepository.GetById(applicationUserId);

            if (data == null)
            {
                throw new Exception("ApplicationUser not found");
            }

            return data;
        }

        #endregion Constructors

        #region Methods

        public async Task RegisterUser(ApplicationUser entity)
        {
            var mqUser = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerUserSettingsKey);
            var mqPwd = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerPwdSettingsKey);

            entity.Application = new Application
            {
                ApplicationMQ = new ApplicationMQ
                {
                    User = mqUser,
                    Password = mqPwd,
                    Topic = RandonHelper.RandomString(10),
                },
                CreateDate = DateTime.Now,
            };

            entity.CreateDate = DateTime.Now;

            await _applicationUserRepository.Insert(entity);           
        }

        #endregion Methods
    }
}