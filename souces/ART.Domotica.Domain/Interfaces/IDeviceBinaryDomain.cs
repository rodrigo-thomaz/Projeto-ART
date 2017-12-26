namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IDeviceBinaryDomain
    {
        #region Methods

        Task<DeviceBinary> CheckForUpdates();

        #endregion Methods
    }
}