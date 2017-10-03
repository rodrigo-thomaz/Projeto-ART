using ART.Data.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Data.Repository.Interfaces
{
    public interface ITemperatureScaleRepository : IRepository<TemperatureScale, byte>
    {
        Task<List<TemperatureScale>> GetAll();
    }
}
