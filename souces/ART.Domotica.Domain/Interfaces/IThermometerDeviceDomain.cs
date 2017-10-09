using ART.Domotica.Model;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Interfaces
{
    public interface IThermometerDeviceDomain
    {
        #region Methods

        Task<List<ThermometerDeviceGetListModel>> GetList(AuthenticatedMessageContract message);

        #endregion Methods
    }
}