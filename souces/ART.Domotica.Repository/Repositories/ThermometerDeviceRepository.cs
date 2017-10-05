using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Repositories
{
    public class ThermometerDeviceRepository : RepositoryBase<ARTDbContext, ThermometerDevice, Guid>, IThermometerDeviceRepository
    {
        public ThermometerDeviceRepository(ARTDbContext context) : base(context)
        {
        }
    }
}
