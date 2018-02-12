namespace ART.Domotica.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceTypeRepository : IRepository<ARTDbContext, DeviceType, DeviceTypeEnum>
    {
        #region Methods

        Task<List<DeviceType>> GetAll();

        #endregion Methods
    }
}