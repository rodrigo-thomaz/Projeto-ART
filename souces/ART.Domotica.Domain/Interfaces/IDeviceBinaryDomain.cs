namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    public interface IDeviceBinaryDomain
    {
        #region Methods

        Task<byte[]> CheckForUpdates(string stationMacAddress, string softAPMacAddress);

        #endregion Methods
    }
}