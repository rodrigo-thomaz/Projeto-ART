namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public interface INumericalScaleRepository : IRepository<ARTDbContext, NumericalScale>
    {
        #region Methods

        Task<List<NumericalScale>> GetAll();

        #endregion Methods
    }
}