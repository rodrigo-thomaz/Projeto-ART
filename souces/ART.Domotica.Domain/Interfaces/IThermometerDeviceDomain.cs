namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IThermometerDeviceDomain
    {
        #region Methods

        Task<List<ThermometerDeviceGetListModel>> GetList(AuthenticatedMessageContract message);        

        #endregion Methods
    }
}