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

            if (filterValue == null)
                return source;

            Type elementType = typeof(TSource);

            PropertyInfo? propertyInfo = elementType.GetProperties()
                .Where(p => p.Name.ToLower().Equals(propertyName.ToLower()))
                .FirstOrDefault();

            if (propertyInfo == null)
                return source;

            if (propertyInfo.PropertyType != filterValue.GetType())
                return source;

            ParameterExpression parameterExpression = Expression.Parameter(elementType);
            Expression expression;

            if (propertyInfo.PropertyType == typeof(Int32)
                || propertyInfo.PropertyType == typeof(Int64)
                || propertyInfo.PropertyType == typeof(Int16)
                || propertyInfo.PropertyType == typeof(Int128))
            {
                expression = Expression.Equal(
                    Expression.Property(
                        parameterExpression,
                        propertyInfo
                    ),
                    Expression.Constant(filterValue)
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

        public static IQueryable<TSource> SortBy<TSource>(
        this IQueryable<TSource> source,
        string sortProperty,
        bool desc = false)
        {
            if (string.IsNullOrEmpty(sortProperty))
                return source;

            PropertyInfo? propertyInfo = typeof(TSource).GetProperty(sortProperty);

            if (propertyInfo == null)
                return source;

            return source
                .OrderBy(p => !desc ? propertyInfo.GetValue(p, null) : null)
                .ThenByDescending(p => desc ? propertyInfo.GetValue(p, null) : null);
        }
    }
}