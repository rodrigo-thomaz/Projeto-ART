namespace ART.MQ.Worker.Entities
{
    public interface IEntity<TKey>
    {
        #region Primitive Properties

        TKey Id { get; set; }

        #endregion        
    }
}