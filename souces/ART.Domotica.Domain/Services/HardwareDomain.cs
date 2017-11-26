namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Entities;
    using System;
    using ART.Infra.CrossCutting.Domain;
    using Autofac;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Repositories;

    public class HardwareDomain : DomainBase, IHardwareDomain
    {
        #region Fields

        private readonly IHardwareRepository _hardwareRepository;

        #endregion Fields

        #region Constructors

        public HardwareDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _hardwareRepository = new HardwareRepository(context);
        }

        #endregion Constructors

        #region Methods
        
        public async Task<HardwareBase> SetLabel(Guid deviceId, string label)
        {
            var entity = await _hardwareRepository.GetById(deviceId);

            if (entity == null)
            {
                throw new Exception("Hardware not found");
            }

            entity.Label = label;

            await _hardwareRepository.Update(entity);

            return entity;
        }

        #endregion Methods
    }
}