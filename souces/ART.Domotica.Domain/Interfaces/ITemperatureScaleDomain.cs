namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Domotica.Contract;

    public interface ITemperatureScaleDomain
    {
        #region Methods

        Task<List<TemperatureScaleGetAllModel>> GetAll();

        Task<List<TemperatureScaleGetAllForDeviceResponseContract>> GetAllForDevice();

        #endregion Methods
    }
}