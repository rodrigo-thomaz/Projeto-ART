namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationDomain
    {
        #region Methods

        Task<Application> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}