using System.Collections.Generic;

namespace ART.Data.Repository.Entities
{
    public abstract class DeviceBase : HardwareBase
    {
        #region Primitive Properties



        #endregion

        #region Navigation Properties

        public ICollection<SensorsInDevice> SensorsInDevice { get; set; }

        #endregion
    }
}