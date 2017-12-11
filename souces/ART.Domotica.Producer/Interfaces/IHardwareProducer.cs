namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwareProducer
    {
        #region Methods

        Task SetLabel(AuthenticatedMessageContract<HardwareSetLabelRequestContract> message);

        #endregion Methods
    }
}