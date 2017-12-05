namespace ART.Infra.CrossCutting.Repository
{
    public interface IEntity
    {
        #region Properties


        #endregion Properties
    }

    public interface IEntity<TKey> : IEntity
    {
        #region Properties

        TKey Id
        {
            get; set;
        }

        #endregion Properties
    }    
}