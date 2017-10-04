namespace ART.Data.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDSFamilyTempSensorResolutionRepository : IRepository<ARTDbContext, DSFamilyTempSensorResolution, byte>
    {
        #region Methods

        Task<List<DSFamilyTempSensorResolution>> GetAll();

        #endregion Methods
    }
}