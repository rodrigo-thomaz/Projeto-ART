namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface ITemperatureScaleDomain
    {
        #region Methods

        Task<List<TemperatureScale>> GetScales();

        #endregion Methods
    }
}