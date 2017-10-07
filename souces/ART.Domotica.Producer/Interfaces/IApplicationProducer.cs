namespace ART.Domotica.Producer.Interfaces
{
    using ART.Infra.CrossCutting.MQ.Contract;
    using System.Threading.Tasks;

    public interface IApplicationProducer
    {
        #region Methods

        Task Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}