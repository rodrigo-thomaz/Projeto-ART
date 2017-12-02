namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public interface INumericalScalePrefixRepository : IRepository<ARTDbContext, NumericalScalePrefix, NumericalScalePrefixEnum>
    {
        #region Methods

        Task<List<NumericalScalePrefix>> GetAll();

        #endregion Methods
    }
}