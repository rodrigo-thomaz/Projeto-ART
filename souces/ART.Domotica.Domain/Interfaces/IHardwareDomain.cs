namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IHardwareDomain
    {
        #region Methods

        Task<HardwareBase> SetLabel(Guid hardwareId, string label);

        #endregion Methods
    }
}