using System;

namespace ART.MQ.DistributedServices.Models
{
    public class HardwareInSpaceGetResponse
    {
        #region public properties

        public Guid TransactionKey { get; set; }
        public dynamic Errors { get; set; }

        #endregion
    }
}