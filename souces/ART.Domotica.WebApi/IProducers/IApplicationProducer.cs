namespace ART.Domotica.WebApi.IProducers
{
    using System;
    using System.Threading.Tasks;

    public interface IApplicationProducer
    {
        #region Methods

        Task GetAll(Guid applicationUserId);

        #endregion Methods
    }
}