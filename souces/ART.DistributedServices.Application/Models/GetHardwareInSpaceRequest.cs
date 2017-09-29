using System;
using System.ComponentModel.DataAnnotations;

namespace ART.DistributedServices.Application.Models
{
    public class GetHardwareInSpaceRequest
    {
        #region public properties

        [Required]
        public Guid SpaceId { get; set; }

        #endregion
    }
}