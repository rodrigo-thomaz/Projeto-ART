using System.Threading.Tasks;
using ART.MQ.Consumer.IDomain;
using ART.MQ.Consumer.IRepositories;

namespace ART.MQ.Consumer.Domain
{
    public class DSFamilyTempSensorDomain : IDSFamilyTempSensorDomain
    {
        #region private readonly fields

        private readonly IDSFamilyTempSensorRepository _dsFamilyTempSensorRepository;

        #endregion

        #region constructors

        public DSFamilyTempSensorDomain(IDSFamilyTempSensorRepository dsFamilyTempSensorRepository)
        {
            _dsFamilyTempSensorRepository = dsFamilyTempSensorRepository;
        }

        #endregion

        #region public voids

        public async Task SetResolution(string deviceAddres, int value)
        {
            
        } 

        #endregion
    }
}
 