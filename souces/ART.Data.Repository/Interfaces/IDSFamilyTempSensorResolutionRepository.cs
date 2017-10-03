using ART.Data.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Data.Repository.Interfaces
{
    public interface IDSFamilyTempSensorResolutionRepository : IRepository<DSFamilyTempSensorResolution, byte>
    {
        Task<List<DSFamilyTempSensorResolution>> GetAll();
    }
}
