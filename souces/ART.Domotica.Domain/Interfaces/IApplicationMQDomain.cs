namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationMQDomain
    {
        #region Methods

        Task<ApplicationMQ> GetByApplicationUserId(AuthenticatedMessageContract message);

        Task<ApplicationMQ> GetByDeviceId(Guid deviceId);

        #endregion Methods
    }
}