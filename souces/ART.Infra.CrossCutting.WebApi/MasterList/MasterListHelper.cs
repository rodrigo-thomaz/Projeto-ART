using ART.Infra.CrossCutting.WebApi.MasterListDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public static class MasterListHelper<TSource>
    {
        public static MasterListRequest ConvertToMasterListDTORequest(MasterListRequest masterListModelRequest, Action<MasterListFilterColumnConvertAction> filterAction = null, Action<MasterListSortColumnConvertAction> sortAction = null)
        {
            List<IMasterListSortColumn> sortColumns = null;
            List<IMasterListFilterColumn> filterColumns = null;

            var sortColumnsTask = Task.Run(() =>
            {
                sortColumns = ConvertToMasterListDTOSortColumns(masterListModelRequest.SortColumns, sortAction);
            });

            var filterColumnsTask = Task.Run(() =>
            {
                filterColumns = ConvertToMasterListDTOFilterColumns(masterListModelRequest.FilterColumns, filterAction);
            });

            MasterListRequest result;

            Task.WaitAll(sortColumnsTask, filterColumnsTask);

            result = new MasterListRequest
            {
                PageNumber = masterListModelRequest.PageNumber,
                PageSize = masterListModelRequest.PageSize,
                Search = masterListModelRequest.Search,
                FilterColumns = filterColumns,
                SortColumns = sortColumns
            };

            return result;
        }

        #region sort voids        

        public static IMasterListSortColumn ConvertToMasterListDTOSortColumn<TProperty>(IMasterListSortColumn sortColumn)
        {
            IMasterListSortColumn result;

            var selector = CreateExpression<TSource, TProperty>(sortColumn.ColumnName);
            var sortDirection = sortColumn.SortDirection == MasterListSortDirection.Ascending ? MasterListSortDirection.Ascending : MasterListSortDirection.Descending;

            result = new MasterListDTOSortColumn<TSource, TProperty>(
                selector: selector,
                sortDirection: sortDirection,
                priority: sortColumn.Priority
            );

            return result;
        }

        private static List<IMasterListSortColumn> ConvertToMasterListDTOSortColumns(List<IMasterListSortColumn> sortColumns, Action<MasterListSortColumnConvertAction> action)
        {
            var result = new List<IMasterListSortColumn>();

            var sortColumnsOrdered = sortColumns.OrderBy(x => x.Priority).ToList();

            foreach (var item in sortColumnsOrdered)
            {
                var propertyType = typeof(TSource).GetProperty(item.ColumnName).PropertyType;

                IMasterListSortColumn column;

                if (propertyType.Equals(typeof(string)))
                {
                    column = ConvertToMasterListDTOSortColumn<string>(item);
                }
                else if (propertyType.Equals(typeof(short)))
                {
                    column = ConvertToMasterListDTOSortColumn<short>(item);
                }
                else if (propertyType.Equals(typeof(int)))
                {
                    column = ConvertToMasterListDTOSortColumn<int>(item);
                }
                else if (propertyType.Equals(typeof(long)))
                {
                    column = ConvertToMasterListDTOSortColumn<long>(item);
                }
                else if (propertyType.Equals(typeof(DateTime)))
                {
                    column = ConvertToMasterListDTOSortColumn<DateTime>(item);
                }
                else if (propertyType.Equals(typeof(decimal)))
                {
                    column = ConvertToMasterListDTOSortColumn<decimal>(item);
                }
                else if (propertyType.Equals(typeof(bool)))
                {
                    column = ConvertToMasterListDTOSortColumn<bool>(item);
                }
                else if (propertyType.Equals(typeof(short?)))
                {
                    column = ConvertToMasterListDTOSortColumn<short?>(item);
                }
                else if (propertyType.Equals(typeof(int?)))
                {
                    column = ConvertToMasterListDTOSortColumn<int?>(item);
                }
                else if (propertyType.Equals(typeof(long?)))
                {
                    column = ConvertToMasterListDTOSortColumn<long?>(item);
                }
                else if (propertyType.Equals(typeof(DateTime?)))
                {
                    column = ConvertToMasterListDTOSortColumn<DateTime?>(item);
                }
                else if (propertyType.Equals(typeof(decimal?)))
                {
                    column = ConvertToMasterListDTOSortColumn<decimal?>(item);
                }
                else if (propertyType.Equals(typeof(bool?)))
                {
                    column = ConvertToMasterListDTOSortColumn<bool?>(item);
                }
                else if (propertyType.Equals(typeof(byte)))
                {
                    column = ConvertToMasterListDTOSortColumn<byte>(item);
                }
                else if (propertyType.Equals(typeof(byte?)))
                {
                    column = ConvertToMasterListDTOSortColumn<byte?>(item);
                }
                else if (propertyType.BaseType.Name == "Enum")
                {
                    var eventArgs = new MasterListSortColumnConvertAction(item);
                    action.Invoke(eventArgs);
                    column = eventArgs.SortColumn;
                }
                else
                {
                    throw new InvalidCastException();
                }

                result.Add(column);
            }

            if (result.Any())
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region filter voids        

        public static IMasterListFilterColumn ConvertToMasterListDTOFilterColumn<TProperty>(IMasterListFilterColumn filterColumn)
        {
            IMasterListFilterColumn result;

            var selector = CreateExpression<TSource, TProperty>(filterColumn.ColumnName);

            var masterListFiltersCriteria = new List<MasterListFilterCriteria>();

            foreach (var item in filterColumn.Criteria)
            {
                MasterListFilterCondition masterListDTOFilterCondition;

                switch (item.FilterCondition)
                {
                    case MasterListFilterCondition.StartsWith:
                        masterListDTOFilterCondition = MasterListFilterCondition.StartsWith;
                        break;
                    case MasterListFilterCondition.EndsWith:
                        masterListDTOFilterCondition = MasterListFilterCondition.EndsWith;
                        break;
                    case MasterListFilterCondition.Exact:
                        masterListDTOFilterCondition = MasterListFilterCondition.Exact;
                        break;
                    case MasterListFilterCondition.Contains:
                        masterListDTOFilterCondition = MasterListFilterCondition.Contains;
                        break;
                    case MasterListFilterCondition.GreaterThan:
                        masterListDTOFilterCondition = MasterListFilterCondition.GreaterThan;
                        break;
                    case MasterListFilterCondition.GreaterThanOrEqual:
                        masterListDTOFilterCondition = MasterListFilterCondition.GreaterThanOrEqual;
                        break;
                    case MasterListFilterCondition.LessThan:
                        masterListDTOFilterCondition = MasterListFilterCondition.LessThan;
                        break;
                    case MasterListFilterCondition.LessThanOrEqual:
                        masterListDTOFilterCondition = MasterListFilterCondition.LessThanOrEqual;
                        break;
                    case MasterListFilterCondition.NotEqual:
                        masterListDTOFilterCondition = MasterListFilterCondition.NotEqual;
                        break;
                    default:
                        var type = typeof(TProperty);
                        if (type.Equals(typeof(long)))
                        {
                            masterListDTOFilterCondition = MasterListFilterCondition.Exact;
                        }
                        else if (type.Equals(typeof(string)))
                        {
                            masterListDTOFilterCondition = MasterListFilterCondition.Contains;
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                        break;
                }

                masterListFiltersCriteria.Add(new MasterListFilterCriteria { Search = item.Search, FilterCondition = masterListDTOFilterCondition} );
	        }
            
            result = new MasterListDTOFilterColumn<TSource, TProperty>(
                selector: selector,
                criteria: masterListFiltersCriteria.ToArray()
            );

            return result;
        }        

        private static List<IMasterListFilterColumn> ConvertToMasterListDTOFilterColumns(List<IMasterListFilterColumn >filterColumns, Action<MasterListFilterColumnConvertAction> action)
        {
            var result = new List<IMasterListFilterColumn>();

            foreach (var item in filterColumns)
            {
                var propertyType = typeof(TSource).GetProperty(item.ColumnName).PropertyType;

                IMasterListFilterColumn column;

                if (propertyType.Equals(typeof(string)))
                {
                    column = ConvertToMasterListDTOFilterColumn<string>(item);
                }
                else if (propertyType.Equals(typeof(short)))
                {
                    column = ConvertToMasterListDTOFilterColumn<short>(item);
                }
                else if (propertyType.Equals(typeof(int)))
                {
                    column = ConvertToMasterListDTOFilterColumn<int>(item);
                }
                else if (propertyType.Equals(typeof(long)))
                {
                    column = ConvertToMasterListDTOFilterColumn<long>(item);
                }
                else if (propertyType.Equals(typeof(DateTime)))
                {
                    column = ConvertToMasterListDTOFilterColumn<DateTime>(item);
                }
                else if (propertyType.Equals(typeof(decimal)))
                {
                    column = ConvertToMasterListDTOFilterColumn<decimal>(item);
                }
                else if (propertyType.Equals(typeof(bool)))
                {
                    column = ConvertToMasterListDTOFilterColumn<bool>(item);
                }
                else if (propertyType.Equals(typeof(short?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<short?>(item);
                }
                else if (propertyType.Equals(typeof(int?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<int?>(item);
                }
                else if (propertyType.Equals(typeof(long?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<long?>(item);
                }
                else if (propertyType.Equals(typeof(DateTime?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<DateTime?>(item);
                }
                else if (propertyType.Equals(typeof(decimal?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<decimal?>(item);
                }
                else if (propertyType.Equals(typeof(bool?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<bool?>(item);
                }
                else if (propertyType.Equals(typeof(byte)))
                {
                    column = ConvertToMasterListDTOFilterColumn<byte>(item);
                }
                else if (propertyType.Equals(typeof(byte?)))
                {
                    column = ConvertToMasterListDTOFilterColumn<byte?>(item);
                }
                else if (propertyType.BaseType.Name == "Enum")
                {
                    var eventArgs = new MasterListFilterColumnConvertAction(item);
                    action.Invoke(eventArgs);
                    column = eventArgs.FilterColumn;
                }
                else
                {
                    throw new InvalidCastException();
                }

                result.Add(column);
            }

            if (result.Any())
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region private voids

        private static Expression<Func<TModel, TProperty>> CreateExpression<TModel, TProperty>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TModel), "x");
            return Expression.Lambda<Func<TModel, TProperty>>(
                Expression.PropertyOrField(param, propertyName), param);
        } 

        #endregion
    }
}
