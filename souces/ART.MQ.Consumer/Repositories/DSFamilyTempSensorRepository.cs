using ART.MQ.Consumer.Entities;
using ART.MQ.Consumer.IRepositories;
using System;

namespace ART.MQ.Consumer.Repositories
{
    public class DSFamilyTempSensorRepository : RepositoryBase<DSFamilyTempSensor, Guid>, IDSFamilyTempSensorRepository
    {
        public DSFamilyTempSensorRepository(ARTDbContext context) : base(context)
        {

        }
    }
}
