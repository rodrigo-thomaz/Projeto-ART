using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ART.Infra.CrossCutting.WebApi
{
    public static class ExpressionHelper
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
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

        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
