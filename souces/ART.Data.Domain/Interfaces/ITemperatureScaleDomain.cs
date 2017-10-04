namespace ART.Data.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;

    public interface ITemperatureScaleDomain
    {
        #region Methods

        Task<List<TemperatureScale>> GetScales();

        #endregion Methods
    }
}