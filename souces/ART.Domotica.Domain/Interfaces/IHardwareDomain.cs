namespace ART.Domotica.Domain.Interfaces
{
    using ART.Domotica.Contract;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHardwareDomain
    {
        #region Methods

        Task<List<HardwareUpdatePinsContract>> UpdatePins();

        #endregion Methods
    }
}