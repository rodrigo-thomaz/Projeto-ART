using ART.Infra.CrossCutting.WebApi.MasterList;
using System;
using System.Linq.Expressions;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTOFilterColumn<TSource, TProperty> : IMasterListDTOFilterColumn
    {
        private readonly string _columnName;
        private readonly MasterListFilterCriteria[] _criteria;

        public MasterListDTOFilterColumn(Expression<Func<TSource, TProperty>> selector, MasterListFilterCriteria[] criteria)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var propertyName = ExpressionHelper.GetPropertyName<TSource, TProperty>(selector);

            _columnName = propertyName;
            _criteria = criteria;
        }

        public string ColumnName { get { return _columnName; } }

        public MasterListFilterCriteria[] Criteria { get { return _criteria; } }
    }
}
