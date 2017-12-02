namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.Repository;

    public interface IUnitMeasurementTypeRepository : IRepository<ARTDbContext, UnitMeasurementType, UnitMeasurementTypeEnum>
    {
        #region Methods

        Task<List<UnitMeasurementType>> GetAll();

        #endregion Methods
    }
}