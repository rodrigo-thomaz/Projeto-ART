namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceBaseDomain
    {
        #region Methods

        Task<DeviceBase> SetLabel(Guid deviceId, string label);

        #endregion Methods
    }
}