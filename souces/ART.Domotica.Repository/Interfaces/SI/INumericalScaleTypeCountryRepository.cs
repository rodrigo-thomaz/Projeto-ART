namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public interface INumericalScaleTypeCountryRepository : IRepository<ARTDbContext, NumericalScaleTypeCountry>
    {
        #region Methods

        Task<List<NumericalScaleTypeCountry>> GetAll();

        Task<NumericalScaleTypeCountry> GetByKey(NumericalScaleTypeEnum numericalScaleTypeId, short countryId);

        #endregion Methods
    }
}