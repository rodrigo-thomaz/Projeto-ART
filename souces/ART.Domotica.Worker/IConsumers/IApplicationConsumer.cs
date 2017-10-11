namespace ART.Domotica.Worker.IConsumers
{
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Model;

    public interface IApplicationConsumer
    {
        #region Methods        

        void SendGetCompleted(AuthenticatedMessageContract message, ApplicationGetModel data);

        #endregion Methods
    }
}