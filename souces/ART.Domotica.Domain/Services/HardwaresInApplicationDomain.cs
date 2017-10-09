namespace ART.Domotica.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Interfaces;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Contract;
    using System;

    public class HardwaresInApplicationDomain : IHardwaresInApplicationDomain
    {
        #region Fields

        private readonly IHardwaresInApplicationRepository _hardwaresInApplicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationDomain(IHardwaresInApplicationRepository hardwaresInApplicationRepository, IApplicationUserRepository applicationUserRepository)
        {
            _hardwaresInApplicationRepository = hardwaresInApplicationRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            var data = await _hardwaresInApplicationRepository.GetList(message.ApplicationUserId);
            var result = Mapper.Map<List<HardwaresInApplication>, List<HardwaresInApplicationGetListModel>>(data);
            return result;
        }        

        public async Task<HardwaresInApplicationSearchPinModel> SearchPin(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message)
        {
            var data = await _hardwaresInApplicationRepository.GetByPin(message.Contract.Pin);

            if (data == null)
            {
                throw new Exception("Pin not found");
            }

            var result = Mapper.Map<HardwareBase, HardwaresInApplicationSearchPinModel>(data);

            return result;
        }

        public async Task InsertHardware(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message)
        {
            var hardwareEntity = await _hardwaresInApplicationRepository.GetByPin(message.Contract.Pin);

            if (hardwareEntity == null)
            {
                throw new Exception("Pin not found");
            }

            var applicationUserEntity = await _applicationUserRepository.GetById(message.ApplicationUserId);

            if (hardwareEntity == null)
            {
                throw new Exception("ApplicationUser not found");
            }

            var hardwaresInApplicationEntity = new HardwaresInApplication
            {
                ApplicationId = applicationUserEntity.ApplicationId,
                HardwareBaseId = hardwareEntity.Id,
                CreateByApplicationUserId = applicationUserEntity.Id,
                CreateDate = DateTime.Now.ToUniversalTime(),
            };

            await _hardwaresInApplicationRepository.Insert(hardwaresInApplicationEntity);
        }

        #endregion Methods
    }
}