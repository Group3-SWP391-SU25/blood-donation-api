using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Utilities
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(first.Parameters[0], parameter);
            var left = leftVisitor.Visit(first.Body);

            var rightVisitor = new ReplaceExpressionVisitor(second.Parameters[0], parameter);
            var right = rightVisitor.Visit(second.Body);

            var andExpression = Expression.AndAlso(left!, right!);
            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression? Visit(Expression? node)
            {
                if (node == _oldValue) return _newValue;
                return base.Visit(node);
            }
        }
    }
}
