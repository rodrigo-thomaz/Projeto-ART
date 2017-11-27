namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    public interface IUnitOfMeasurementTypeRepository : IRepository<ARTDbContext, UnitOfMeasurementType, UnitOfMeasurementTypeEnum>
    {
        #region Methods

        Task<List<UnitOfMeasurementType>> GetAll();

        #endregion Methods
    }
}