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

using FluentGraphQL.Builder.Abstractions;
using FluentGraphQL.Builder.Constants;

namespace FluentGraphQL.Builder.Atoms
{
    internal class GraphQLPropertyValue : IGraphQLPropertyValue
    {
        public string ValueLiteral { get; set; }

        public GraphQLPropertyValue(string valueLiteral)
        {
            ValueLiteral = valueLiteral;
        }

        public virtual string ToString(IGraphQLStringFactory graphQLStringFactory)
        {
            return graphQLStringFactory.Construct(this);
        }

        public override string ToString()
        {
            return ValueLiteral;
        }

        public bool IsNull()
        {
            return ValueLiteral is null || ValueLiteral.Equals(Constant.GraphQLKeyords.Null);
        }
    }
}
