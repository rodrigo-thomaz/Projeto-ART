namespace ART.Infra.CrossCutting.Repository
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