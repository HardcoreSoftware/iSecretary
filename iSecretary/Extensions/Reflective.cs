using System;
using System.Linq.Expressions;

namespace Extensions
{
    public class Nameof<T>
    {
        public static string Property<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("'expression' should be a member expression");
            }
            return body.Member.Name;
        }
    }
}
