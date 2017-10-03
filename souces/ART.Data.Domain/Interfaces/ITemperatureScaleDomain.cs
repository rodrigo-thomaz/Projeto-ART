using ART.Data.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Data.Domain.Interfaces
{
    public interface ITemperatureScaleDomain
    {
        Task<List<TemperatureScale>> GetScales();
    }
}
