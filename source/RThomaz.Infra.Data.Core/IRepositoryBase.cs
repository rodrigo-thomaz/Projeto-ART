using System;
using System.Collections.Generic;

namespace RThomaz.Infra.Data.Core
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {

        void Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Dispose();

        Guid AplicacaoId { get; set; }
    }
}
