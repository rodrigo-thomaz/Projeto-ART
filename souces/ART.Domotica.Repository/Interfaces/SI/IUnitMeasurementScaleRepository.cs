namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public interface IUnitMeasurementScaleRepository : IRepository<ARTDbContext, UnitMeasurementScale>
    {
        #region Methods

        Task<List<UnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}