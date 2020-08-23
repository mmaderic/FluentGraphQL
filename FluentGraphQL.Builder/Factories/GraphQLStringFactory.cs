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
using FluentGraphQL.Builder.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGraphQL.Builder.Factories
{
    public class GraphQLStringFactory : IGraphQLStringFactory
    {
        public IGraphQLStringFactoryOptions Options { get; set; }

        public GraphQLStringFactory() { }
        public GraphQLStringFactory(IGraphQLStringFactoryOptions graphQLStringFactoryOptions)
        {
            Options = graphQLStringFactoryOptions;
        }

        public virtual string Construct(IGraphQLQuery graphQLQuery)
        {
            var builder = new StringBuilder();
            var selectNodeString = graphQLQuery.SelectNode.ToString(this);

            builder
                .AppendLine("query {")
                .Append(selectNodeString)
                .Append("}");

            var result = builder.ToString();
            var namingStrategy = GetNamingStrategyFunction();

            return graphQLQuery.QueryString = namingStrategy.Invoke(result);
        }

        public string Construct(IGraphQLMultipleQuery graphQLMultipleQuery)
        {
            var builder = new StringBuilder();

            builder.AppendLine("query {");
            foreach(var query in graphQLMultipleQuery)
            {
                var queryString = query.SelectNode.ToString(this);
                builder.Append(queryString);
            }
            builder.Append("}");

            var result = builder.ToString();
            var namingStrategy = GetNamingStrategyFunction();

            Parallel.ForEach(graphQLMultipleQuery, (query) =>
            {
                var queryString = query.SelectNode.ToString(this);
                query.QueryString = namingStrategy.Invoke(queryString);
            });

            return graphQLMultipleQuery.QueryString = namingStrategy.Invoke(result);
        }

        public virtual string Construct(IGraphQLMutation graphQLMutation)
        {
            var builder = new StringBuilder();
            var selectNodeString = graphQLMutation.SelectNode.ToString(this);

            builder
                .AppendLine("mutation {")
                .Append(selectNodeString)
                .Append("}");

            var result = builder.ToString();
            var namingStrategy = GetNamingStrategyFunction();

            return graphQLMutation.QueryString = namingStrategy.Invoke(result);
        }

        public virtual string Construct(IGraphQLHeaderNode graphQLHeaderNode)
        {
            var builder = new StringBuilder();
            builder.Append(graphQLHeaderNode.Prefix);
            builder.Append(graphQLHeaderNode.Title);
            builder.Append(graphQLHeaderNode.Suffix);

            if (graphQLHeaderNode.Statements.Count > 0)
            {
                var statementsString = string.Join(" ", graphQLHeaderNode.Statements.Select(x => x.ToString(this)));
                statementsString = $"({statementsString})";
                builder.Append(statementsString);
            }

            return builder.ToString();
        }

        public virtual string Construct(IGraphQLSelectNode graphQLSelectNode)
        {
            var builder = new StringBuilder();
            var headerNode = graphQLSelectNode.HeaderNode;

            var headerIndentation = headerNode is null 
                ? null
                : string.Concat(Enumerable.Range(0, headerNode.HierarchyLevel).Select(x => " "));

            var selectNodeIndentation = headerIndentation is null ? " " : headerIndentation + " ";

            var headerNodeString = graphQLSelectNode.HeaderNode?.ToString(this);
            var propertyStatements = graphQLSelectNode.PropertyStatements.Where(x => x.IsActive).Select(x => x.ToString(this)).Prepend($"{selectNodeIndentation}");
            var selectNodePropertiesString = string.Join($"\n{selectNodeIndentation}", propertyStatements);

            var activeChildNodes = graphQLSelectNode.ChildSelectNodes.Concat(graphQLSelectNode.AggregateContainerNodes).Where(x => x.IsActive);
            var selectNodeChildNodesString = string.Concat(activeChildNodes.Select(x => x.ToString(this)));

            builder
                .Append(headerIndentation)
                .Append(headerNodeString)
                .Append("{")
                .AppendLine(selectNodePropertiesString)
                .Append(selectNodeChildNodesString)
                .Append(headerIndentation)
                .AppendLine("}");

            return builder.ToString();
        }

        public virtual string Construct(IGraphQLCollectionValue graphQLCollectionValue)
        {
            var items = string.Join(", ", graphQLCollectionValue.CollectionItems.Select(x => x.ToString(this)));
            return $"[{items}]";
        }

        public virtual string Construct(IGraphQLObjectValue graphQLObjectValue)
        {
            var properties = string.Join(", ", graphQLObjectValue.PropertyValues.Select(x => x.ToString(this)));
            return $"{{{properties}}}";
        }

        public virtual string Construct(IGraphQLPropertyStatement graphQLPropertyStatement)
        {
            return graphQLPropertyStatement.PropertyName;
        }

        public virtual string Construct(IGraphQLValueStatement graphQLValueStatement)
        {
            var valueString = !(graphQLValueStatement.Value is null)
                ? graphQLValueStatement.Value.ToString(this)
                : "{}";

            return $"{graphQLValueStatement.PropertyName}: {valueString}";
        }

        public virtual string Construct(IGraphQLPropertyValue graphQLPropertyValue)
        {
            return $"<graphql-value>{graphQLPropertyValue.ValueLiteral}</graphql-value>";
        }

        public virtual string Construct(LogicalOperator logicalOperator)
        {
            switch (logicalOperator)
            {
                case LogicalOperator.And:
                    return "_and";
                case LogicalOperator.Or:
                    return "_or";
                case LogicalOperator.Not:
                    return "_not";
                default:
                    throw new InvalidOperationException();
            };        
        }

        public virtual string Construct(OrderByDirection orderByDirection)
        {
            switch (orderByDirection)
            {
                case OrderByDirection.Asc:
                    return "asc";
                case OrderByDirection.Desc:
                    return "desc";
                case OrderByDirection.AscNullsFirst:
                    return "asc_nulls_first";
                case OrderByDirection.DescNullsLast:
                    return "desc_nulls_last";
                default:
                    throw new InvalidOperationException();
            };
        }

        public string Construct(string @string)
        {
            var namingStrategy = GetNamingStrategyFunction();
            return namingStrategy.Invoke(@string);
        }

        public string Desconstruct(string @string)
        {
            var namingStrategy = GetReverseNamingStrategyFunction();
            return namingStrategy.Invoke(@string);
        }

        private Func<string, string> GetNamingStrategyFunction()
        {
            if (Options is null)
                return (string input) => input;

            switch (Options.NamingStrategy)
            {
                case NamingStrategy.SnakeCase:
                    return StringExtensions.ToSnakeCaseExtended;
                default:
                    throw new NotImplementedException();
            };
        }

        private Func<string, string> GetReverseNamingStrategyFunction()
        {
            if (Options is null)
                return (string input) => input;

            switch (Options.NamingStrategy)
            {
                case NamingStrategy.SnakeCase:
                    return StringExtensions.SnakeCaseToPascalCase;
                default:
                    throw new NotImplementedException();
            };
        }
    }
}
