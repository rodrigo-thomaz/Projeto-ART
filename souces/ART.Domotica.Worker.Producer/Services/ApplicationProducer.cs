namespace ART.Domotica.Worker.Producer.Services
{
    using System.Threading.Tasks;

    using ART.Domotica.Worker.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Model;

    public class ApplicationProducer : IApplicationProducer
    {
        #region Fields

        #endregion Fields

        #region Constructors

        public ApplicationProducer() 
        {
            
        }

        #endregion Constructors

        public async Task<ApplicationGetModel> Get(AuthenticatedMessageContract message)
        {
            throw new System.NotImplementedException();
        }
    }
}