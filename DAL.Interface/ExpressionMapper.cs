using System.Linq.Expressions;
using System.Linq;
using System;

namespace DAL.Interface
{
    public class ExpressionMapper<From, To, TResult>
    {
        private class Visitor<TFrom, TTo> : ExpressionVisitor
        {
            public ParameterExpression ParameterExpression { get; private set; }

            public Visitor(ParameterExpression parameterExpression)
            {
                this.ParameterExpression = parameterExpression;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ParameterExpression;
            }
            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.DeclaringType == typeof(TFrom))
                {
                    return Expression.MakeMemberAccess(this.Visit(node.Expression), typeof(TTo).GetMember(node.Member.Name).FirstOrDefault());
                }
                else
                {
                    return base.VisitMember(node);
                }
            }
        }

        public static Expression<Func<To, TResult>> Map(Expression<Func<From, TResult>> expression)
        {
            Visitor<From, To> expressionMapper = new Visitor<From, To>(Expression.Parameter(typeof(To), expression.Parameters[0].Name));
            return Expression.Lambda<Func<To, TResult>>(expressionMapper.Visit(expression.Body), expressionMapper.ParameterExpression);
        }
    }
}
