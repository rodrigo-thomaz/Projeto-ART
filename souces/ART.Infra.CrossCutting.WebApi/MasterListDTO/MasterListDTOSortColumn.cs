using System;
using System.Linq.Expressions;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public class MasterListDTOSortColumn<TSource, TProperty> : IMasterListDTOSortColumn
    {
        private readonly string _columnName;
        private readonly MasterListDTOSortDirection _sortDirection;
        private readonly int _priority;

        public MasterListDTOSortColumn(Expression<Func<TSource, TProperty>> selector, MasterListDTOSortDirection sortDirection, int priority)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var propertyName = ExpressionHelper.GetPropertyName<TSource, TProperty>(selector);

            _columnName = propertyName;
            _sortDirection = sortDirection;
            _priority = priority;
        }

        public string ColumnName { get { return _columnName; } }
        public MasterListDTOSortDirection SortDirection { get { return _sortDirection; } }
        public int Priority { get { return _priority; } }
    }
}
