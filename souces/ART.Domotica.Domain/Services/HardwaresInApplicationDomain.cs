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
    using log4net;
    using ART.Infra.CrossCutting.Domain;
    using ART.Infra.CrossCutting.Logging;

    public class HardwaresInApplicationDomain : DomainBase, IHardwaresInApplicationDomain
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IHardwaresInApplicationRepository _hardwaresInApplicationRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationDomain(ILogger logger, IHardwaresInApplicationRepository hardwaresInApplicationRepository, IApplicationUserRepository applicationUserRepository)
        {
            _logger = logger;
            _hardwaresInApplicationRepository = hardwaresInApplicationRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message)
        {
            _logger.Debug();

            var data = await _hardwaresInApplicationRepository.GetList(message.ApplicationUserId);
            var result = Mapper.Map<List<HardwaresInApplication>, List<HardwaresInApplicationGetListModel>>(data);
            return result;
        }        

        public async Task<HardwaresInApplicationSearchPinModel> SearchPin(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message)
        {
            _logger.Debug();

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
            _logger.Debug();

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

        public async Task DeleteHardware(AuthenticatedMessageContract<HardwaresInApplicationDeleteHardwareContract> message)
        {
            _logger.Debug();

            var hardwareEntity = await _hardwaresInApplicationRepository.GetById(message.Contract.HardwaresInApplicationId);

            if (hardwareEntity == null)
            {
                throw new Exception("HardwaresInApplication not found");
            }            

            await _hardwaresInApplicationRepository.Delete(hardwareEntity);
        }

        #endregion Methods
    }
}