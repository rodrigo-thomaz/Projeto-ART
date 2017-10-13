namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using System;
    using ART.Infra.CrossCutting.Domain;

    public class HardwareDomain : DomainBase, IHardwareDomain
    {
        #region Fields

        private readonly IHardwareRepository _hardwareRepository;

        #endregion Fields

        #region Constructors

        public HardwareDomain(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }
        
        #endregion Constructors

        #region Methods

        public async Task UpdatePins()
        {
            await _hardwareRepository.GetById(Guid.NewGuid());
        }

        #endregion Methods
    }
}