using System;

namespace ART.Data.Repository.Entities
{
    public class HardwareInApplication
    {
        #region Primitive Properties

        public Guid HardwareBaseId { get; set; }
        public Guid ApplicationId { get; set; }

        #endregion

        #region Navigation Properties

        public HardwareBase HardwareBase { get; set; }
        public Application Application { get; set; }

        #endregion
    }
}