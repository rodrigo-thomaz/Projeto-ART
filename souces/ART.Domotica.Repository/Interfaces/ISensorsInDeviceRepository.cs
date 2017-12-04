using ART.Domotica.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Interfaces
{
    public interface ISensorsInDeviceRepository
    {
        Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId);
    }
}
