namespace ART.Domotica.WebApi.IProducers
{
    using ART.Infra.CrossCutting.MQ;
    using System.Threading.Tasks;

    public interface IApplicationProducer
    {
        #region Methods

        Task GetAll(AuthenticatedMessageContract message);

        #endregion Methods
    }
}