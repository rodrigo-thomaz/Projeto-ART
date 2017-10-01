using System.Threading.Tasks;

namespace ART.MQ.Worker.IDomain
{
    public interface IDSFamilyTempSensorDomain
    {
        Task SetResolution(string deviceAddres, int value);
    }
}
