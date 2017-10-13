namespace ART.Domotica.Domain.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Domain;
    using ART.Infra.CrossCutting.Utils;

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
            var existingPins = await _hardwareRepository.GetExistingPins();
            var entities = await _hardwareRepository.GetHardwaresNotInApplication();

            foreach (var item in entities)
            {
                var pin = RandonHelper.RandomString(4);
                while (existingPins.Contains(pin))
                {
                    pin = RandonHelper.RandomString(4);
                }
                item.Pin = pin;                
            }
            await _hardwareRepository.Update(entities);
        }

        #endregion Methods
    }
}