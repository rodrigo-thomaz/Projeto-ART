namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Model;

    public interface ITemperatureScaleDomain
    {
        #region Methods

        Task<List<TemperatureScaleGetAllModel>> GetAll();

        #endregion Methods
    }
}