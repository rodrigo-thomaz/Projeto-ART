namespace ART.Data.Repository.Entities
{
    public interface IEntity<TKey>
    {
        #region Primitive Properties

        TKey Id { get; set; }

        #endregion        
    }
}