using System.Linq.Expressions;
using System.Reflection;

namespace NMeter.Api.Reporting.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<TSource> FilterBy<TSource>(
            this IQueryable<TSource> source,
            string propertyName,
            object filterValue)
        {
            if (string.IsNullOrEmpty(propertyName))
                return source;

            Type elementType = typeof(TSource);

            PropertyInfo? propertyInfo = elementType.GetProperties()
                .Where(p => p.Name.ToLower().Equals(propertyName.ToLower()))
                .FirstOrDefault();

            if (propertyInfo == null)
                return source;

            ParameterExpression parameterExpression = Expression.Parameter(elementType);
            Expression expression;

            if (propertyInfo.PropertyType == typeof(int))
            {
                expression = Expression.Equal(
                    Expression.Property(
                        parameterExpression,
                        propertyInfo
                    ),
                    Expression.Constant((int)filterValue)
                );
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                expression = Expression.Call(
                    Expression.Property(
                        parameterExpression,
                        propertyInfo
                    ),
                    containsMethod,
                    Expression.Constant((string)filterValue)
                );
            }
            else
                return source;
            
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(expression, parameterExpression);
            return source.Where(lambda);
        }
    }
}