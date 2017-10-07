namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationProducer
    {
        #region Methods

        Task Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}