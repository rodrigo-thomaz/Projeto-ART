namespace ART.Domotica.Worker.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationProducer
    {
        #region Methods

        Task<ApplicationGetModel> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}