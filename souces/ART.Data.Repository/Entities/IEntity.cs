namespace ART.Data.Repository.Entities
{
    public interface IEntity<TKey>
    {
        #region Properties

        TKey Id
        {
            get; set;
        }

        #endregion Properties
    }
}