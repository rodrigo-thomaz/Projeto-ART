using System;

namespace ART.Domotica.DistributedServices.Entities
{
    public interface IEntity
    {
        #region Primitive Properties

        Guid Id { get; set; }

        #endregion        
    }
}