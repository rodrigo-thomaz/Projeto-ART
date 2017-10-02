using ART.Data.Repository.Entities;
using ART.Data.Repository.Interfaces;

namespace ART.Data.Repository.Repositories
{
    public class DSFamilyTempSensorResolutionRepository : RepositoryBase<DSFamilyTempSensorResolution, byte>, IDSFamilyTempSensorResolutionRepository
    {
        public DSFamilyTempSensorResolutionRepository(ARTDbContext context) : base(context)
        {

        }
    }
}
