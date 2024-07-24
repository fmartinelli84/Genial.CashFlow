using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Framework.Reflection
{
    public static class ReflectionExtensions
    {
        public static string[] ExtractNames<T, TResult>(this Expression<Func<T, TResult>> nameExpression)
        {
            if (nameExpression == null)
                throw new ArgumentNullException(nameof(nameExpression));

            var names = new string[] { };

            if (nameExpression.Body is MemberInitExpression)
            {
                var memberInitExpression = (MemberInitExpression)nameExpression.Body;

                names = memberInitExpression.Bindings.Select(m => m.Member.Name).ToArray();
            }
            if (nameExpression.Body is NewExpression)
            {
                var newExpression = (NewExpression)nameExpression.Body;

                names = newExpression.Members!.Select(m => m.Name).ToArray();
            }
            if (nameExpression.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)nameExpression.Body;
                var memberExpression = (MemberExpression)unaryExpression.Operand;

                names = new[] { memberExpression.Member.Name };
            }
            else if (nameExpression.Body is MemberExpression)
            {
                var memberExpression = (MemberExpression)nameExpression.Body;

                names = new[] { memberExpression.Member.Name };
            }
            else if (nameExpression.Body is MethodCallExpression)
            {
                var methodCallExpression = (MethodCallExpression)nameExpression.Body;

                names = new[] { methodCallExpression.Method.Name };
            }

            return names;
        }
    }
}
