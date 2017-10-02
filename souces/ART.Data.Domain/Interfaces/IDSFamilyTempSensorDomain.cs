using System.Threading.Tasks;

namespace ART.Data.Domain.Interfaces
{
    public interface IDSFamilyTempSensorDomain
    {
        Task SetResolution(string deviceAddres, int value);
    }
}
