using FluentGraphQL.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    public class Class1 : IClassFixture<Context>
    {
        [Fact]
        public void Test()
        {
            Assert.True(true);
        }
    }
}
