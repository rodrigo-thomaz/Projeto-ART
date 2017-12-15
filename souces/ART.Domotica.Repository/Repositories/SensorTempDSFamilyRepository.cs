namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;

    public class SensorTempDSFamilyRepository : RepositoryBase<ARTDbContext, SensorTempDSFamily>, ISensorTempDSFamilyRepository
    {
        #region Constructors

        public SensorTempDSFamilyRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<SensorTempDSFamily> GetByKey(Guid sensorTempDSFamilyId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
        {
            return await _context.SensorTempDSFamily
                .FindAsync(sensorTempDSFamilyId, sensorDatasheetId, sensorTypeId);
        }
    }
}