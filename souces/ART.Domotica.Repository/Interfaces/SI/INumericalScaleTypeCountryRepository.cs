namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface INumericalScaleTypeCountryRepository
    {
        #region Methods

        Task<List<NumericalScaleTypeCountry>> GetAll();

        #endregion Methods
    }
}