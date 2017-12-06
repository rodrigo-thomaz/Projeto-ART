namespace ART.Domotica.Producer.Interfaces.Globalization
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ITimeZoneProducer
    {
        #region Methods

        Task GetAll(AuthenticatedMessageContract message);

        #endregion Methods
    }
}