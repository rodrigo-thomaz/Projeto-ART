using System;

namespace ART.Consumer.Entities
{
    public interface IEntity
    {
        #region Primitive Properties

        Guid Id { get; set; }

        #endregion        
    }
}