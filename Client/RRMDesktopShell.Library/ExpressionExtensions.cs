using System.Reflection;

namespace System.Linq.Expressions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Converts an expression into a <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>The member info.</returns>
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            return (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression)lambdaExpression.Body : (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand).Member;
        }
    }
}
