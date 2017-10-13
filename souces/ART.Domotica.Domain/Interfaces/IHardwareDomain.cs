namespace ART.Domotica.Domain.Interfaces
{
    using ART.Domotica.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHardwareDomain
    {
        #region Methods

        Task<List<HardwareUpdatePinsModel>> UpdatePins();

        #endregion Methods
    }
}