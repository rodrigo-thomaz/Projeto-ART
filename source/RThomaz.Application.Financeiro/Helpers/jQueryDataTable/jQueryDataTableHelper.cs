using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RThomaz.Application.Financeiro.Helpers.jQueryDataTable
{
    public static class jQueryDataTableHelper<TSource>
    {
        public static PagedListRequest<TSource> ConvertToPagedListRequest(jQueryDataTableParameter param)
        {
            //search

            PagedListSearch search = null;

            if (param.search != null && !string.IsNullOrWhiteSpace(param.search.value))
            {
                search = new PagedListSearch(param.search.value, param.search.regex);
            }

            //columns

            var columns = ConvertToPagedListOrderColumns(param);

            //PagedListParam

            PagedListParam pagedListParam = null;

            if (param.length > 0 && param.start >= 0)
            {
                pagedListParam = new PagedListParam((uint)param.start, (uint)param.length);
            }

            //Result

            PagedListRequest<TSource> result;

            if (search != null && columns != null && pagedListParam != null)
            {
                result = new PagedListRequest<TSource>
                (
                    param: pagedListParam,
                    orderColumns: columns,
                    search: search
                );
            }
            else if (search != null && columns == null && pagedListParam == null)
            {
                result = new PagedListRequest<TSource>
                (
                    search: search
                );
            }
            else if (search == null && columns != null && pagedListParam == null)
            {
                result = new PagedListRequest<TSource>
                (
                    orderColumns: columns
                );
            }
            else if (search == null && columns == null && pagedListParam != null)
            {
                result = new PagedListRequest<TSource>
                (
                    param: pagedListParam
                );
            }
            else if (search != null && columns != null && pagedListParam == null)
            {
                result = new PagedListRequest<TSource>
                (
                    orderColumns: columns,
                    search: search
                );
            }
            else if (search != null && columns == null && pagedListParam != null)
            {
                result = new PagedListRequest<TSource>
                (
                    param: pagedListParam,
                    search: search
                );
            }
            else if (search == null && columns != null && pagedListParam != null)
            {
                result = new PagedListRequest<TSource>
                (
                    param: pagedListParam,
                    orderColumns: columns
                );
            }
            else
            {
                throw new ArgumentNullException();
            }

            return result;
        }

        public static IPagedListOrderColumn ConvertToPagedListOrderColumn<TProperty>(jQueryDataTableColumn[] columns, jQueryDataTableOrder[] orders, int columnIndex)
        {
            
            IPagedListOrderColumn result = null;

            if (orders.Any(x => x.column == columnIndex))
            {
                var selector = CreateExpression<TSource, TProperty>(columns[columnIndex].name);
                var orderDirection = orders.First(x => x.column == columnIndex).dir == "asc" ? PagedListOrderDirection.Asc : PagedListOrderDirection.Desc;

                result = new PagedListOrderColumn<TSource, TProperty>(
                    selector: selector,
                    orderDirection: orderDirection
                );
            }

            return result;
        }

        public static List<IPagedListOrderColumn> ConvertToPagedListOrderColumns(jQueryDataTableParameter param)
        {
            var columns = new List<IPagedListOrderColumn>();

            var orderableColumns = param.columns.Where(x => x.orderable && !string.IsNullOrEmpty(x.name)).ToArray();

            for (int i = 0; i < orderableColumns.Length; i++)
            {
                var propertyType = typeof(TSource).GetProperty(orderableColumns[i].name).PropertyType;

                IPagedListOrderColumn column;

                if (propertyType.Equals(typeof(string)))
                {
                    column = ConvertToPagedListOrderColumn<string>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(short)))
                {
                    column = ConvertToPagedListOrderColumn<short>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(int)))
                {
                    column = ConvertToPagedListOrderColumn<int>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(long)))
                {
                    column = ConvertToPagedListOrderColumn<long>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(DateTime)))
                {
                    column = ConvertToPagedListOrderColumn<DateTime>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(decimal)))
                {
                    column = ConvertToPagedListOrderColumn<decimal>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(bool)))
                {
                    column = ConvertToPagedListOrderColumn<bool>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(short?)))
                {
                    column = ConvertToPagedListOrderColumn<short?>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(int?)))
                {
                    column = ConvertToPagedListOrderColumn<int?>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(long?)))
                {
                    column = ConvertToPagedListOrderColumn<long?>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(DateTime?)))
                {
                    column = ConvertToPagedListOrderColumn<DateTime?>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(decimal?)))
                {
                    column = ConvertToPagedListOrderColumn<decimal?>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(bool?)))
                {
                    column = ConvertToPagedListOrderColumn<bool?>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(byte)))
                {
                    column = ConvertToPagedListOrderColumn<byte>(orderableColumns, param.order, i);
                }
                else if (propertyType.Equals(typeof(byte?)))
                {
                    column = ConvertToPagedListOrderColumn<byte?>(orderableColumns, param.order, i);
                }
                else
                {
                    throw new InvalidCastException();
                }
                if (column != null)
                {
                    columns.Add(column);
                }
            }

            if (columns.Any())
            {
                return columns;
            }
            else
            {
                return null;
            }
        }

        private static Expression<Func<TModel, TProperty>> CreateExpression<TModel, TProperty>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TModel), "x");
            return Expression.Lambda<Func<TModel, TProperty>>(
                Expression.PropertyOrField(param, propertyName), param);
        }
    }
}