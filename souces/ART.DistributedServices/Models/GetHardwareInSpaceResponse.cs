using System;

namespace ART.DistributedServices.Models
{
    public class GetHardwareInSpaceResponse
    {
        #region public properties

        public Guid TransactionKey { get; set; }
        public dynamic Errors { get; set; }

        #endregion
    }
}