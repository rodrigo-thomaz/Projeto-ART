using ART.DistributedServices.Application.Models;
using System.Threading.Tasks;

namespace ART.DistributedServices.Application.Interfaces
{
    public interface IDSFamilyTempSensorAppService
    {
        Task<DSFamilyTempSensorSetResolutionResponse> SetResolution(DSFamilyTempSensorSetResolutionRequest request);
    }
}
