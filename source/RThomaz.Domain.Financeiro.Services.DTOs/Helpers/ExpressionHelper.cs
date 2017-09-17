using RThomaz.Domain.Financeiro.Services.DTOs.Helpers.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RThomaz.Domain.Financeiro.Services.DTOs.Helpers
{
    public static class ExpressionHelper
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> selector)
        {
            if(selector == null)
            {
                throw new ArgumentNullException("propertyLambda");
            }

            Type type = typeof(TSource);

            MemberExpression member = selector.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    selector.ToString()));
            }

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    selector.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    selector.ToString(),
                    type));
            }

            return propInfo;
        }
                
        public static void ApplyOrder<TEntity, TEntityProperty, TContract, TContractProperty>(ref IQueryable<TEntity> query, Expression<Func<TEntity, TEntityProperty>> entitySelector, PagedListRequest<TContract> pagedListRequest, Expression<Func<TContract, TContractProperty>> contractSelector, ref bool isFirstOrderable)
        {
            if (pagedListRequest.Orderable<TContractProperty>(contractSelector))
            {
                var order = pagedListRequest.GetOrder<TContractProperty>(contractSelector);

                if (isFirstOrderable)
                {
                    isFirstOrderable = false;
                    if (order == PagedListOrderDirection.Asc)
                        query = query.OrderBy(entitySelector);
                    else
                        query = query.OrderByDescending(entitySelector);
                }
                else
                {
                    if (order == PagedListOrderDirection.Asc)
                        query = ((IOrderedQueryable<TEntity>)query).ThenBy(entitySelector);
                    else
                        query = ((IOrderedQueryable<TEntity>)query).ThenByDescending(entitySelector);
                }
            }
        }

        public static void ApplyOrder<TEntity, TEntityProperty, TContract, TContractProperty>(ref IEnumerable<TEntity> query, Func<TEntity, TEntityProperty> entitySelector, PagedListRequest<TContract> pagedListRequest, Expression<Func<TContract, TContractProperty>> contractSelector, ref bool isFirstOrderable)
        {
            if (pagedListRequest.Orderable<TContractProperty>(contractSelector))
            {
                var order = pagedListRequest.GetOrder<TContractProperty>(contractSelector);

                if (isFirstOrderable)
                {
                    isFirstOrderable = false;
                    if (order == PagedListOrderDirection.Asc)
                        query = query.OrderBy(entitySelector);
                    else
                        query = query.OrderByDescending(entitySelector);
                }
                else
                {
                    if (order == PagedListOrderDirection.Asc)
                        query = ((IOrderedEnumerable<TEntity>)query).ThenBy(entitySelector);
                    else
                        query = ((IOrderedEnumerable<TEntity>)query).ThenByDescending(entitySelector);
                }
            }
        }
    }
}
