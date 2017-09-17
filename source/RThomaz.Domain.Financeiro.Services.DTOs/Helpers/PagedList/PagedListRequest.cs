using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList
{
    public class PagedListRequest<TSource>
    {        
        private readonly PagedListParam _param;
        private readonly PagedListSearch _search;
        private readonly ReadOnlyCollection<IPagedListOrderColumn> _orderColumns;

        public PagedListRequest(PagedListParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }

            _param = param;            
        }

        public PagedListRequest(PagedListSearch search)
        {
            if (search == null)
            {
                throw new ArgumentNullException("search");
            }

            _search = search;
        }

        public PagedListRequest(List<IPagedListOrderColumn> orderColumns)
        {
            if (orderColumns == null)
            {
                throw new ArgumentNullException("orderColumns");
            }
            else if (orderColumns.Count == 0)
            {
                throw new ArgumentException("orderColumns");
            }

            _orderColumns = orderColumns.AsReadOnly();
        }

        public PagedListRequest(PagedListParam param, PagedListSearch search)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            else if (search == null)
            {
                throw new ArgumentNullException("search");
            }

            _param = param;
            _search = search;
        }

        public PagedListRequest(PagedListParam param, List<IPagedListOrderColumn> orderColumns)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            else if (orderColumns == null)
            {
                throw new ArgumentNullException("orderColumns");
            }
            else if (orderColumns.Count == 0)
            {
                throw new ArgumentException("orderColumns");
            }

            _param = param;
            _orderColumns = orderColumns.AsReadOnly();
        }

        public PagedListRequest(PagedListSearch search, List<IPagedListOrderColumn> orderColumns)
        {
            if (search == null)
            {
                throw new ArgumentNullException("search");
            }
            else if (orderColumns == null)
            {
                throw new ArgumentNullException("orderColumns");
            }
            else if (orderColumns.Count == 0)
            {
                throw new ArgumentException("orderColumns");
            }

            _search = search;
            _orderColumns = orderColumns.AsReadOnly();
        }

        public PagedListRequest(PagedListParam param, PagedListSearch search, List<IPagedListOrderColumn> orderColumns)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            else if (search == null)
            {
                throw new ArgumentNullException("search");
            }
            else if (orderColumns == null)
            {
                throw new ArgumentNullException("orderColumns");
            }
            else if (orderColumns.Count == 0)
            {
                throw new ArgumentException("orderColumns");
            }

            _param = param;
            _search = search;
            _orderColumns = orderColumns.AsReadOnly();
        }

        public PagedListParam Param { get { return _param; } }
        public PagedListSearch Search { get { return _search; } }
        public ReadOnlyCollection<IPagedListOrderColumn> OrderColumns { get { return _orderColumns; } }
        
        public bool Orderable<TProperty>(Expression<Func<TSource, TProperty>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            if (_orderColumns == null)
            {
                throw new ArgumentNullException("OrderColumns");
            }

            var propertyInfo = ExpressionHelper.GetPropertyInfo(selector);
            var columnName = propertyInfo.Name;
            var result = _orderColumns.Any(x => x.ColumnName == columnName );
            return result;
        }
       
        public PagedListOrderDirection GetOrder<TProperty>(Expression<Func<TSource, TProperty>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            if (_orderColumns == null)
            {
                throw new ArgumentNullException("OrderColumns");
            }

            var propertyInfo = ExpressionHelper.GetPropertyInfo(selector);
            var columnName = propertyInfo.Name;
            var result = _orderColumns.First(x => x.ColumnName == columnName );
            return result.OrderDirection;
        }       
    }
}
