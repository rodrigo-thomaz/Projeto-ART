using System;
using System.ComponentModel.DataAnnotations;

namespace ART.MQ.DistributedServices.Models
{
    public class HardwareInSpaceGetRequest
    {
        #region public properties

        [Required]
        public Guid SpaceId { get; set; }

        #endregion
    }
}