using ART.Infra.CrossCutting.WebApi.MasterList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ART.Infra.CrossCutting.WebApi.MasterListDTO
{
    public static class MasterListDTOHelper
    {
        #region filter voids

        public static List<Expression<Func<TEntity, bool>>> CreateFilterExpression<TEntity, TContract, TProperty>(Expression<Func<TEntity, TProperty>> entitySelector, Expression<Func<TContract, TProperty>> contractSelector, IMasterListDTOFilterColumn filterColumn)
        {
            var propertyInfo = ExpressionHelper.GetPropertyInfo(entitySelector);

            var result = new List<Expression<Func<TEntity, bool>>>();

            foreach (var criteria in filterColumn.Criteria)
            {
                TProperty retorno = (TProperty)TypeDescriptor.GetConverter(typeof(TProperty)).ConvertFrom(criteria.Search);

                Expression<Func<TEntity, bool>> expression = null;

                switch (criteria.FilterCondition)
                {
                    case MasterListFilterCondition.StartsWith:
                    case MasterListFilterCondition.EndsWith:
                    case MasterListFilterCondition.Contains:
                        expression = CreateMethodCallExpression<TEntity, TProperty>(propertyInfo, retorno, criteria.FilterCondition);
                        break;
                    case MasterListFilterCondition.Exact:
                    case MasterListFilterCondition.GreaterThan:
                    case MasterListFilterCondition.GreaterThanOrEqual:
                    case MasterListFilterCondition.LessThan:
                    case MasterListFilterCondition.LessThanOrEqual:
                    case MasterListFilterCondition.NotEqual:
                        expression = CreateBinaryExpression<TEntity, TProperty>(propertyInfo, retorno, criteria.FilterCondition);
                        break;
                    default:
                        break;
                }

                result.Add(expression);
            }

            return result;
        }

        private static Expression<Func<TEntity, bool>> CreateBinaryExpression<TEntity, TProperty>(PropertyInfo propertyInfo, TProperty value, MasterListFilterCondition filterCondition)
        {
            var parameterExpression = Expression.Parameter(typeof(TEntity), @"x");
            var memberExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);

            ConstantExpression constantExpression = null;

            var fieldType = typeof(TProperty);

            if (fieldType.IsEnum)
            {
                constantExpression = Expression.Constant(Enum.ToObject(fieldType, value));
            }
            else
            {
                constantExpression = Expression.Constant(value, fieldType);
            }

            BinaryExpression binaryExpression = null;

            switch (filterCondition)
            {
                case MasterListFilterCondition.StartsWith:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.EndsWith:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.Exact:
                    binaryExpression = Expression.Equal(memberExpression, constantExpression);
                    break;
                case MasterListFilterCondition.Contains:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.GreaterThan:
                    binaryExpression = Expression.GreaterThan(memberExpression, constantExpression);
                    break;
                case MasterListFilterCondition.GreaterThanOrEqual:
                    binaryExpression = Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                    break;
                case MasterListFilterCondition.LessThan:
                    binaryExpression = Expression.LessThan(memberExpression, constantExpression);
                    break;
                case MasterListFilterCondition.LessThanOrEqual:
                    binaryExpression = Expression.LessThanOrEqual(memberExpression, constantExpression);
                    break;
                case MasterListFilterCondition.NotEqual:
                    binaryExpression = Expression.NotEqual(memberExpression, constantExpression);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameterExpression);
        }

        private static Expression<Func<TEntity, bool>> CreateMethodCallExpression<TEntity, TProperty>(PropertyInfo propertyInfo, TProperty value, MasterListFilterCondition filterCondition)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

            var methodName = string.Empty;

            switch (filterCondition)
            {
                case MasterListFilterCondition.StartsWith:
                    methodName = "StartsWith";
                    break;
                case MasterListFilterCondition.EndsWith:
                    methodName = "EndsWith";
                    break;
                case MasterListFilterCondition.Exact:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.Contains:
                    methodName = "Contains";
                    break;
                case MasterListFilterCondition.GreaterThan:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.GreaterThanOrEqual:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.LessThan:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.LessThanOrEqual:
                    throw new ArgumentOutOfRangeException();
                case MasterListFilterCondition.NotEqual:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var methodCallExpression = Expression.Call(propertyAccess, methodName, null, Expression.Constant(value, typeof(TProperty)));

            return Expression.Lambda<Func<TEntity, bool>>(methodCallExpression, parameter);
        }

        #endregion

        #region sort voids

        public static void ApplySort<TEntity, TContract, TProperty>(IMasterListDTOSortColumn sortColumn, ref IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> entitySelector, Expression<Func<TContract, TProperty>> contractSelector, ref bool isFirstSortable)
        {
            var propertyName = ExpressionHelper.GetPropertyName<TContract, TProperty>(contractSelector);

            if (propertyName != sortColumn.ColumnName)
            {
                return;
            }

            if (isFirstSortable)
            {
                isFirstSortable = false;
                if (sortColumn.SortDirection == MasterListSortDirection.Ascending)
                    query = query.OrderBy(entitySelector);
                else
                    query = query.OrderByDescending(entitySelector);
            }
            else
            {
                if (sortColumn.SortDirection == MasterListSortDirection.Ascending)
                    query = ((IOrderedQueryable<TEntity>)query).ThenBy(entitySelector);
                else
                    query = ((IOrderedQueryable<TEntity>)query).ThenByDescending(entitySelector);
            }
        }        

        public static void ApplySort<TEntity, TContract, TProperty>(IMasterListDTOSortColumn sortColumn, ref IEnumerable<TEntity> query, Func<TEntity, TProperty> entitySelector, Expression<Func<TContract, TProperty>> contractSelector, ref bool isFirstSortable)
        {
            var propertyName = ExpressionHelper.GetPropertyName<TContract, TProperty>(contractSelector);

            if (propertyName != sortColumn.ColumnName)
            {
                return;
            }

            if (isFirstSortable)
            {
                isFirstSortable = false;
                if (sortColumn.SortDirection == MasterListSortDirection.Ascending)
                    query = query.OrderBy(entitySelector);
                else
                    query = query.OrderByDescending(entitySelector);
            }
            else
            {
                if (sortColumn.SortDirection == MasterListSortDirection.Ascending)
                    query = ((IOrderedEnumerable<TEntity>)query).ThenBy(entitySelector);
                else
                    query = ((IOrderedEnumerable<TEntity>)query).ThenByDescending(entitySelector);
            }
        }

        #endregion
    }
}
