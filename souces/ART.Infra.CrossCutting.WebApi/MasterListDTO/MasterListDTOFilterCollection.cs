using ART.Infra.CrossCutting.WebApi.MasterList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTOFilterCollection<TEntity, TContract>
    {
        private readonly List<IMasterListFilterColumn> _filterColumns;
        private readonly List<Expression<Func<TEntity, bool>>> _filterExpressions;

        public MasterListDTOFilterCollection(List<IMasterListFilterColumn> filterColumns)
        {
            _filterColumns = filterColumns;
            _filterExpressions = new List<Expression<Func<TEntity, bool>>>();
        }

        public void AddFilter<TProperty>(Expression<Func<TEntity, TProperty>> entitySelector, Expression<Func<TContract, TProperty>> contractSelector)
        {
            var propertyName = ExpressionHelper.GetPropertyName<TEntity, TProperty>(entitySelector);            

            var filterColumn = _filterColumns.FirstOrDefault(x => x.ColumnName == propertyName);
            if (filterColumn != null)
            {
                var expressions = MasterListDTOHelper.CreateFilterExpression<TEntity, TContract, TProperty>(entitySelector, contractSelector, filterColumn);
                _filterExpressions.AddRange(expressions);
            }
        }

        public void ApplyQuery(ref IQueryable<TEntity> query)
        {
            foreach (var filterColumn in _filterColumns)
            {
                foreach (var filterExpression in _filterExpressions)
                {
                    query = ((IQueryable<TEntity>)query).Where(filterExpression);
                }
            }
        }
    }
}
