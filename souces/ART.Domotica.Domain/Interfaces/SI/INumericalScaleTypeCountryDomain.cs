namespace ART.Domotica.Domain.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface INumericalScaleTypeCountryDomain
    {
        #region Methods

        Task<List<NumericalScaleTypeCountry>> GetAll();

        #endregion Methods
    }
}