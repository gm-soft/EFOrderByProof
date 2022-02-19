using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Shared.OrderingHack;

// https://github.com/dotnet/efcore/issues/19828#issuecomment-858880227
public static class QueryableExtensions
{
    public static IQueryable<TEntity> RemoveOrdering<TEntity>(
        [NotNull] this IQueryable<TEntity> source)
        where TEntity : class
    {
        return source.Provider is EntityQueryProvider
            ? source.Provider.CreateQuery<TEntity>(new RemoveOrderingExpression(source.Expression))
            : source;
    }
}

public class RemoveOrderingExpression : Expression, IPrintableExpression
{
    public Expression Source { get; }
    public sealed override ExpressionType NodeType => ExpressionType.Extension;
    public override Type Type => typeof(RemoveOrderingExpression);

    public RemoveOrderingExpression(Expression source)
    {
        Source = source;
    }

    protected override Expression VisitChildren(ExpressionVisitor visitor)
    {
        var source = visitor.Visit(Source);

        if (source is ShapedQueryExpression sqe &&
            sqe.QueryExpression is SelectExpression se &&
            0 < se.Orderings.Count)
        {
            se.ClearOrdering();
            return source;
        }
        
        return new RemoveOrderingExpression(source);
    }

    /// <inheritdoc />
    void IPrintableExpression.Print(ExpressionPrinter expressionPrinter)
    {
        expressionPrinter.AppendLine($"{nameof(RemoveOrderingExpression)}(");
        using (expressionPrinter.Indent())
        {
            expressionPrinter.Visit(Source);
            expressionPrinter.AppendLine(")");
        }
    }
}