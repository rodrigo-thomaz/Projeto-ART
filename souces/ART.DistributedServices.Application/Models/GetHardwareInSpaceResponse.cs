using System;

namespace ART.DistributedServices.Application.Models
{
    public class GetHardwareInSpaceResponse
    {
        #region public properties

        public Guid TransactionKey { get; set; }
        public dynamic Errors { get; set; }

        #endregion
    }
}