/*
    MIT License

    Copyright (c) 2020 Mateo Mađerić

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
*/

using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Builder.Abstractions;
using FluentGraphQL.Builder.Atoms;
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentGraphQL.Builder.Converters
{
    public class GraphQLExpressionConverter : IGraphQLExpressionConverter
    {
        private readonly IGraphQLValueFactory _graphQLValueFactory;
        private readonly IGraphQLStringFactory _graphQLStringFactory;

        public GraphQLExpressionConverter(IGraphQLValueFactory graphQLValueFactory, IGraphQLStringFactory graphQLStringFactory)
        {
            _graphQLValueFactory = graphQLValueFactory;
            _graphQLStringFactory = graphQLStringFactory;
        }

        public virtual IGraphQLValueStatement Convert<TEntity>(Expression<Func<TEntity, bool>> expressionPredicate)
        {
            return EvaluateExpression(expressionPredicate.Body);
        }

        public virtual IGraphQLValueStatement Convert<TEntity, TKey>(Expression<Func<TEntity, TKey>> keySelector, OrderByDirection orderByDirection)
        {
            var expression = keySelector.Body;

            if (expression is MemberExpression memberExpression)
                return EvaluateOrderByExpression(memberExpression, orderByDirection);

            if (expression is MethodCallExpression methodCallExpression)
                return EvaluateOrderByExpression(methodCallExpression, orderByDirection);

            throw new NotImplementedException();
        }

        public virtual IEnumerable<IGraphQLValueStatement> Convert<TEntity, TKey>(Expression<Func<TEntity, IEnumerable<TKey>>> keySelectors)
        {
            var expression = keySelectors.Body;

            if (expression is NewArrayExpression newArrayExpression)
                return newArrayExpression.Expressions.Select(x => EvaluateExpression(x)).ToArray();

            if (expression is ListInitExpression listInitExpression)
                return listInitExpression.Initializers.Select(x => EvaluateExpression(x.Arguments.First())).ToArray();

            throw new NotImplementedException();
        }

        public IGraphQLValueStatement Convert<TEntity, TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return EvaluateExpression(keySelector.Body);
        }

        public IGraphQLValueStatement ConvertSelectExpression<TEntity, TKey>(Expression<Func<TEntity, TKey>> selector)
        {
            var expression = selector.Body;

            if (expression is NewExpression newExpression)
                return EvaluateSelectNewExpression(newExpression);

            if (expression is MemberExpression memberExpression)
                return EvaluateMemberExpression(memberExpression, null);

            if (expression is MethodCallExpression methodCallExpression)
                return EvaluateSelectMethodCallExpression(methodCallExpression);

            if (expression is InvocationExpression invocationExpression)
                return EvaluateSelectInvocationExpression(invocationExpression);

            throw new NotImplementedException();
        }

        public IGraphQLValueStatement EvaluateExpression(Expression expression)
        {
            if (expression is UnaryExpression unaryExpression)
                return EvaluateUnaryExpression(unaryExpression);

            if (expression is BinaryExpression binaryExpression)            
                return EvaluateBinaryExpression(binaryExpression);

            if (expression is MethodCallExpression methodCallExpression)
               return EvaluateMethodCallExpression(methodCallExpression);

            if (expression is LambdaExpression lambdaExpression)
                return EvaluateExpression(lambdaExpression.Body);

            if (expression is MemberExpression memberExpression)
                return EvaluateMemberExpression(memberExpression, null);         

            if (expression is ConstantExpression constantExpression)
                return new GraphQLValueStatement(null, _graphQLValueFactory.Construct(constantExpression.Value));

            throw new NotImplementedException();
        }

        private IGraphQLValueStatement EvaluateUnaryExpression(UnaryExpression unaryExpression)
        {
            switch (unaryExpression.NodeType)
            {
                case ExpressionType.Not:
                    return EvaluateNotExpressionType(unaryExpression.Operand);
                case ExpressionType.Quote:
                    return EvaluateExpression(unaryExpression.Operand);
                case ExpressionType.Convert:
                    return EvaluateExpression(unaryExpression.Operand);
                default:
                    throw new NotImplementedException();
            };
        }

        private IGraphQLValueStatement EvaluateBinaryExpression(BinaryExpression binaryExpression)
        {
            switch (binaryExpression.NodeType)
            {
                case ExpressionType.AndAlso:
                    return EvaluateLogicalExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.AndAlso);
                case ExpressionType.OrElse:
                    return EvaluateLogicalExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.OrElse);
                case ExpressionType.Equal:
                    return EvaluateComparisonExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.Equal);
                case ExpressionType.NotEqual:
                    return EvaluateComparisonExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.NotEqual);
                case ExpressionType.GreaterThan:
                    return EvaluateComparisonExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.GreaterThan);
                case ExpressionType.LessThan:
                    return EvaluateComparisonExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.LessThan);
                case ExpressionType.GreaterThanOrEqual:
                    return EvaluateComparisonExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.GreaterThanOrEqual);
                case ExpressionType.LessThanOrEqual:
                    return EvaluateComparisonExpressionType(binaryExpression.Left, binaryExpression.Right, ExpressionType.LessThanOrEqual);
                default:
                    throw new NotImplementedException();
            };
        }

        private IGraphQLValueStatement EvaluateNotExpressionType(Expression expression)
        {
            var valueStatement = EvaluateExpression(expression);
            var logicalOperator = _graphQLStringFactory.Construct(LogicalOperator.Not);

            return new GraphQLValueStatement(logicalOperator, new GraphQLObjectValue(valueStatement));
        }

        private IGraphQLValueStatement EvaluateComparisonExpressionType(Expression left, Expression right, ExpressionType expressionType)
        {
            if (left is UnaryExpression unaryExpression)
                return EvaluateComparisonExpressionType(unaryExpression.Operand, right, expressionType);

            if (!(left is MemberExpression memberExpression))
                throw new InvalidOperationException();

            object operatorValue;
            var value = Expression.Lambda(right).Compile().DynamicInvoke();

            if (value is null)
            {
                switch (expressionType)
                {
                    case ExpressionType.Equal:
                        operatorValue = new { _is_null = true };
                        break;
                    case ExpressionType.NotEqual:
                        operatorValue = new { _is_null = false };
                        break;
                    default:
                        throw new InvalidOperationException();
                };
            }
            else
            {
                switch (expressionType)
                {
                    case ExpressionType.Equal:
                        operatorValue = new { _eq = value };
                        break;
                    case ExpressionType.NotEqual:
                        operatorValue = new { _neq = value }; 
                        break;
                    case ExpressionType.GreaterThan:
                        operatorValue = new { _gt = value };
                        break;
                    case ExpressionType.LessThan:
                        operatorValue = new { _lt = value };
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        operatorValue = new { _gte = value };
                        break;
                    case ExpressionType.LessThanOrEqual:
                        operatorValue = new { _lte = value };
                        break;
                    default:
                        throw new InvalidOperationException();
                };
            }

            var operatorGraphQLValue = _graphQLValueFactory.Construct(operatorValue);
            return EvaluateMemberExpression(memberExpression, operatorGraphQLValue);
        }       

        private IGraphQLValueStatement EvaluateLogicalExpressionType(Expression left, Expression right, ExpressionType expressionType)
        {
            string logicalOperator;
            switch (expressionType)
            {
                case ExpressionType.AndAlso:
                    logicalOperator = _graphQLStringFactory.Construct(LogicalOperator.And);
                    break;
                case ExpressionType.OrElse:
                    logicalOperator = _graphQLStringFactory.Construct(LogicalOperator.Or);
                    break;
                default:
                    throw new InvalidOperationException();
            };

            var leftEvaluated = new GraphQLObjectValue(EvaluateExpression(left));
            var rightEvaluated = new GraphQLObjectValue(EvaluateExpression(right));

            return new GraphQLValueStatement(logicalOperator, new GraphQLCollectionValue(new[] { leftEvaluated, rightEvaluated }));
        }

        private IGraphQLValueStatement EvaluateMethodCallExpression(MethodCallExpression methodCallExpression)
        {
            var methodName = methodCallExpression.Method.Name;
            var category = methodName.EvaluateMethodCallCategory();

            if (category is null)
                throw new NotImplementedException(methodName);

            if (category.Equals(typeof(Constant.SupportedMethodCalls)))
                return EvaluateSupportedMethodCallExpression(methodName, methodCallExpression);

            if (category.Equals(typeof(Constant.ExtensionMethodCalls)))
                return EvaluateExtensionMethodCallExpression(methodName, methodCallExpression);

            throw new NotImplementedException(methodName);
        } 

        private IGraphQLValueStatement EvaluateSupportedMethodCallExpression(string methodName, MethodCallExpression methodCallExpression)
        {
            if (methodName.Equals(Constant.SupportedMethodCalls.Any))
            {
                var potentialMemberExpression = methodCallExpression.Arguments[0] is UnaryExpression unaryExpression
                    ? unaryExpression.Operand
                    : methodCallExpression.Arguments[0];

                if (!(potentialMemberExpression is MemberExpression memberExpression))
                    throw new NotImplementedException();

                if (methodCallExpression.Arguments.Count == 1)
                    return EvaluateMemberExpression(memberExpression, null);

                var genericExpressionType = methodCallExpression.Arguments[1].GetType().GetGenericTypeDefinition().BaseType;
                if (!genericExpressionType.GetGenericTypeDefinition().Equals(typeof(Expression<>)))
                    throw new NotImplementedException();

                var genericExpressionBody = (Expression) genericExpressionType.GetProperty("Body").GetValue(methodCallExpression.Arguments[1]);
                var expressionValue = new GraphQLObjectValue(EvaluateExpression(genericExpressionBody));

                return EvaluateMemberExpression(memberExpression, expressionValue);
            }

            if (methodName.Equals(Constant.SupportedMethodCalls.Equals))
            {
                if (!(methodCallExpression.Object is MemberExpression memberExpression))
                    throw new InvalidOperationException();

                var expressionValue = Expression.Lambda(methodCallExpression.Arguments[0]).Compile().DynamicInvoke();

                var operatorValue = expressionValue is null
                    ? (object) new { _is_null = true }
                    : new { _eq = expressionValue };

                var operatorGraphQLValue = _graphQLValueFactory.Construct(operatorValue);

                return EvaluateMemberExpression(memberExpression, operatorGraphQLValue);
            }

            if (methodName.Equals(Constant.SupportedMethodCalls.Select))
            {
                if (!(methodCallExpression.Arguments[1] is LambdaExpression lambdaExpression && lambdaExpression.Body is NewExpression newExpression))
                    throw new NotImplementedException();

                var expressionTarget = EvaluateExpression(methodCallExpression.Arguments[0]);
                var selectExpression = EvaluateSelectNewExpression(newExpression);
                selectExpression.PropertyName = expressionTarget.PropertyName;

                return selectExpression;
            }

            throw new NotImplementedException(methodName);
        }

        private IGraphQLValueStatement EvaluateExtensionMethodCallExpression(string methodName, MethodCallExpression methodCallExpression)
        {
            var potentialMemberExpression = methodCallExpression.Arguments[0] is UnaryExpression unaryExpression
                    ? unaryExpression.Operand
                    : methodCallExpression.Arguments[0];

            if (!(potentialMemberExpression is MemberExpression memberExpression))
                throw new NotImplementedException();

            if (methodCallExpression.Arguments.Count == 1)
                return EvaluateMemberExpression(memberExpression, null);

            object invokeValue() 
            {
                var expression = methodCallExpression.Arguments[1];
                return Expression.Lambda(expression).Compile().DynamicInvoke();
            }

            object operatorValue;
            switch (methodName)
            {
                case Constant.ExtensionMethodCalls.In:
                    operatorValue = new { _in = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.NotIn:
                    operatorValue = new { _nin = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.Like:
                    operatorValue = new { _like = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.NotLike:
                    operatorValue = new { _nlike = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.LikeInsensitive:
                    operatorValue = new { _ilike = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.NotLikeInsensitive:
                    operatorValue = new { _nilike = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.Similar:
                    operatorValue = new { _similar = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.NotSimilar:
                    operatorValue = new { _nsimilar = invokeValue() };
                    break;
                case Constant.ExtensionMethodCalls.GreaterThan:
                    operatorValue = EvaluateComparisonExpressionType(memberExpression, methodCallExpression, ExpressionType.GreaterThan);
                    break;
                case Constant.ExtensionMethodCalls.LessThan:
                    operatorValue = EvaluateComparisonExpressionType(memberExpression, methodCallExpression, ExpressionType.LessThan);
                    break;
                default:
                    throw new InvalidOperationException();
            };

            var operatorGraphQLValue = _graphQLValueFactory.Construct(operatorValue);
            return EvaluateMemberExpression(memberExpression, operatorGraphQLValue);
        }

        private IGraphQLValueStatement EvaluateMemberExpression(MemberExpression memberExpression, IGraphQLValue graphQLValue)
        {
            var propertyName = memberExpression.Member.Name;
            var expressionNode = memberExpression.Expression;
            var valueStatement = new GraphQLValueStatement(propertyName, graphQLValue);

            return EvaluateMemberExpression(expressionNode, valueStatement);
        }

        private IGraphQLValueStatement EvaluateOrderByExpression(MemberExpression memberExpression, OrderByDirection orderByDirection)
        {
            var propertyName = memberExpression.Member.Name;            
            var expressionNode = memberExpression.Expression;

            var value = _graphQLValueFactory.Construct(orderByDirection);
            var valueStatement = new GraphQLValueStatement(propertyName, value);

            return EvaluateMemberExpression(expressionNode, valueStatement);
        }

        private IGraphQLValueStatement EvaluateOrderByExpression(MethodCallExpression methodCallExpression, OrderByDirection orderByDirection)
        {
            var memberExpression = methodCallExpression.Object as MemberExpression;
            var member = memberExpression.Member;

            Type type;
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    type = ((PropertyInfo)member).PropertyType;
                    break;
                case MemberTypes.Field:
                    type = ((FieldInfo)member).FieldType;
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be type of PropertyInfo or FieldInfo");
            };

            if (!typeof(IGraphQLAggregateContainerNode).IsAssignableFrom(type))
                throw new InvalidOperationException("Order by clause method call expression is only supported using IAggregateContainer properties.");

            if (methodCallExpression.Method.Name.Equals(Constant.AggregateMethodCalls.Count))
                return EvaluateOrderByAggregateCountMethodCall(member.Name, orderByDirection);
            
            return EvaluateOrderByAggregateMethodCall(member.Name, methodCallExpression.Arguments, methodCallExpression.Method.Name, orderByDirection);
        }

        private IGraphQLValueStatement EvaluateOrderByAggregateCountMethodCall(string aggregateName, OrderByDirection orderByDirection)
        {
            var value = _graphQLValueFactory.Construct(orderByDirection);
            var directionObjectValue = new GraphQLObjectValue(new GraphQLValueStatement(Constant.AggregateMethodCalls.Count, value));

            return new GraphQLValueStatement(aggregateName, directionObjectValue);
        }

        private IGraphQLValueStatement EvaluateOrderByAggregateMethodCall(
            string aggregateName, IEnumerable<Expression> arguments, string methodName, OrderByDirection orderByDirection)
        {
            if (!methodName.EvaluateMethodCallCategory().Equals(typeof(Constant.AggregateMethodCalls)))
                throw new InvalidOperationException();

            if (arguments.Count() != 1)
                throw new InvalidOperationException();

            var direction = _graphQLValueFactory.Construct(orderByDirection);
            var propertyName = EvaluateExpression(arguments.First()).PropertyName.ToString();

            var directionObjectValue = new GraphQLObjectValue(new GraphQLValueStatement(propertyName, direction));
            var methodStatement = new GraphQLValueStatement(methodName, directionObjectValue);           

            return new GraphQLValueStatement(aggregateName, new GraphQLObjectValue(methodStatement));
        }

        private IGraphQLValueStatement EvaluateMemberExpression(Expression expressionNode, IGraphQLValueStatement valueStatement)
        {
            while (expressionNode is MemberExpression parentMemberExpression)
            {
                var parentPropertyName = parentMemberExpression.Member.Name;
                var wrappedStatement = new GraphQLValueStatement(parentPropertyName, new GraphQLObjectValue(valueStatement));

                valueStatement = wrappedStatement;
                expressionNode = parentMemberExpression.Expression;
            }

            return valueStatement;
        }

        private IGraphQLValueStatement EvaluateSelectNewExpression(NewExpression newExpression)
        {
            return EvaluateSelectExpressionArguments(newExpression.Arguments);
        }

        private IGraphQLValueStatement EvaluateSelectInvocationExpression(InvocationExpression invocationExpression)
        {
            return EvaluateSelectExpressionArguments(invocationExpression.Arguments);
        }

        private IGraphQLValueStatement EvaluateSelectMethodCallExpression(MethodCallExpression methodCallExpression)
        {
            var methodName = methodCallExpression.Method.Name;
            var category = methodName.EvaluateMethodCallCategory();

            if (category?.Equals(typeof(Constant.SupportedMethodCalls)) == true)
                return EvaluateSupportedMethodCallExpression(methodName, methodCallExpression);
            
            return EvaluateSelectExpressionArguments(methodCallExpression.Arguments);
        }

        private IGraphQLValueStatement EvaluateSelectExpressionArguments(ReadOnlyCollection<Expression> expressionArguments)
        {
            var arguments = expressionArguments.Select(argument =>
            {
                if (argument is MemberExpression memberExpression)
                    return EvaluateMemberExpression(memberExpression, null);

                if (argument is MethodCallExpression methodCallExpression)
                    return EvaluateSelectMethodCallExpression(methodCallExpression);

                if (argument is BinaryExpression binaryExpression)
                    return EvaluateSelectBinaryExpression(binaryExpression);

                if (argument is NewExpression nestedNewExpression)
                    return EvaluateSelectNewExpression(nestedNewExpression);

                if (argument is ConditionalExpression conditionalExpression)
                    return null;

                throw new NotImplementedException();
            }).ToArray();

            var objectValues = arguments.Where(x => !(x is null)).Select(x => new GraphQLObjectValue(x)).ToArray();
            return new GraphQLValueStatement(null, new GraphQLCollectionValue(objectValues));
        }

        private IGraphQLValueStatement EvaluateSelectBinaryExpression(BinaryExpression binaryExpression)
        {
            if (binaryExpression.Left is MemberExpression le && binaryExpression.Right is MemberExpression re)
            {
                var leftMem = EvaluateMemberExpression(le, null);
                var rightMem = EvaluateMemberExpression(re, null);

                return new GraphQLValueStatement(null, new GraphQLObjectValue(new[] { leftMem, rightMem }));
            }

            IGraphQLValueStatement left = null;
            IGraphQLValueStatement right = null;

            if (binaryExpression.Left is BinaryExpression lBin)
                left = EvaluateSelectBinaryExpression(lBin);
            else if (binaryExpression.Left is MemberExpression leftMemberExpression)
                left = EvaluateMemberExpression(leftMemberExpression, null);

            if (binaryExpression.Right is BinaryExpression rBin)
                right = EvaluateSelectBinaryExpression(rBin);
            else if (binaryExpression.Right is MemberExpression rightMemberExpression)
                right = EvaluateMemberExpression(rightMemberExpression, null);

            return new GraphQLValueStatement(null, new GraphQLObjectValue(new[] { left, right }));
        }
    }
}
