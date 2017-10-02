using ART.Data.Repository.Entities;
using ART.Data.Repository.Interfaces;
using System;

namespace ART.Data.Repository.Repositories
{
    public class DSFamilyTempSensorRepository : RepositoryBase<DSFamilyTempSensor, Guid>, IDSFamilyTempSensorRepository
    {
        public DSFamilyTempSensorRepository(ARTDbContext context) : base(context)
        {

        }
    }
}
