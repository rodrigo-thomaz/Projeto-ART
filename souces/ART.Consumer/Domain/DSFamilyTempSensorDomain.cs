using ART.Consumer.IDomain;
using ART.Consumer.IRepositories;

namespace ART.Consumer.Domain
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
    }
}
