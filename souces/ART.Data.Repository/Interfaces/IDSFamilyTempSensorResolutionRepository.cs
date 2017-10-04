namespace ART.Data.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;

    public interface IDSFamilyTempSensorResolutionRepository : IRepository<DSFamilyTempSensorResolution, byte>
    {
        #region Methods

        Task<List<DSFamilyTempSensorResolution>> GetAll();

        #endregion Methods
    }
}