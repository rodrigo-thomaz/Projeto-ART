using System;

namespace ART.MQ.Consumer.Entities
{
    public interface IEntity
    {
        #region Primitive Properties

        Guid Id { get; set; }

        #endregion        
    }
}