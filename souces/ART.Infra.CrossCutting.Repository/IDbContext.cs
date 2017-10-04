namespace ART.Infra.CrossCutting.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IDbContext
    {
        #region Other

        //virtual DbSet<TEntity> Set<TEntity>() where TEntity : class;

        #endregion Other
    }
}