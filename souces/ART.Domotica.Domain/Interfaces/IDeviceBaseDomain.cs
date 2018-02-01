namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IDeviceBaseDomain
    {
        #region Methods

        Task<DeviceBase> SetLabel(Guid deviceId, Guid deviceDatasheetId, string label);

        #endregion Methods
    }
}