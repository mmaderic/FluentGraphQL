using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Extensions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests
{
    [Collection("Context collection")]
    public class DynamicExpressionTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public DynamicExpressionTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task SimpleOrElseTest()
        {
            Expression? expression = null;
            void BuildExpression<TEntity>(Expression<Func<TEntity, bool>> expressionPredicate)
            {
                if (expression is null)
                    expression = expressionPredicate.Body;
                else
                    expression = Expression.OrElse(expression, expressionPredicate.Body);
            }

            var keywords = "cube focus fuji radon".Split(" ");
            foreach (var keyword in keywords)            
                BuildExpression<Brand>(x => x.Name.LikeInsensitive($"{keyword}"));  

            var query = _graphQLClient.QueryBuilder<Brand>()
                .Where(expression)
                .OrderBy(x => x.Name)
                .Build();

            var result = await _graphQLClient.ExecuteAsync(query);

            Assert.Equal(4, result.Count);
            Assert.Equal("Cube", result[0].Name);
            Assert.Equal("Focus", result[1].Name);
            Assert.Equal("Fuji", result[2].Name);
            Assert.Equal("Radon", result[3].Name);
        }
    }
}
