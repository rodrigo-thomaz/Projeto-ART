using System.Threading.Tasks;

namespace ART.MQ.Consumer.IDomain
{
    public interface IDSFamilyTempSensorDomain
    {
        Task SetResolution(string deviceAddres, int value);
    }
}
