using System;
using System.Linq.Expressions;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList
{
    public class PagedListOrderColumn<TSource, TProperty> : IPagedListOrderColumn
    {
        private readonly string _columnName;        
        private readonly PagedListOrderDirection _orderDirection;

        public PagedListOrderColumn(Expression<Func<TSource, TProperty>> selector, PagedListOrderDirection orderDirection)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var propertyInfo = ExpressionHelper.GetPropertyInfo(selector);

            _columnName = propertyInfo.Name;
            _orderDirection = orderDirection;
        }

        public string ColumnName { get { return _columnName; } }
        public PagedListOrderDirection OrderDirection { get { return _orderDirection; } }
    }
}
