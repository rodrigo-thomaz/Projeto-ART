using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Repositories
{
    public class SensorTriggerRepository : RepositoryBase<ARTDbContext, SensorTrigger, Guid>, ISensorTriggerRepository
    {
        public SensorTriggerRepository(ARTDbContext context) : base(context)
        {

        }
    }
}
