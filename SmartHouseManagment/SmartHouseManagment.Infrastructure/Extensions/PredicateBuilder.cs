using System.Linq.Expressions;

namespace SmartHouseManagment.Infrastructure.Extensions;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> firstExpression,
        Expression<Func<T, bool>> secondExpression)
    {
        var parameter = firstExpression.Parameters;

        var visitor = new SubstExpressionVisitor();
        visitor.Substitutions[secondExpression.Parameters[0]] = parameter[0];
        
        var body = Expression.AndAlso(firstExpression.Body, visitor.Visit(secondExpression.Body));
        
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
    
    private sealed class SubstExpressionVisitor : ExpressionVisitor
    {
        public readonly Dictionary<Expression, Expression> Substitutions = new();
        
        protected override Expression VisitParameter(ParameterExpression node)
            => Substitutions.TryGetValue(node, out var replacement) ? replacement : node;
    }
}