﻿/*
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
using System.Collections.Generic;
using System.Linq;

namespace FluentGraphQL.Builder.Atoms
{
    internal class GraphQLObjectValue : IGraphQLObjectValue
    {
        public IEnumerable<IGraphQLValueStatement> PropertyValues { get; set; }

        private GraphQLObjectValue(GraphQLObjectValue copy)
        {
            PropertyValues = copy.PropertyValues.Select(x => (IGraphQLValueStatement) x.DeepCopy()).ToArray();
        }

        public GraphQLObjectValue(IGraphQLValueStatement propertyValue)
        {
            PropertyValues = new[] { propertyValue };
        }

        public GraphQLObjectValue(IEnumerable<IGraphQLValueStatement> propertyValues)
        {
            PropertyValues = propertyValues;
        }

        public virtual string ToString(IGraphQLStringFactory graphQLStringFactory)
        {
            return graphQLStringFactory.Construct(this);
        }

        public bool IsNull()
        {
            return PropertyValues is null || !PropertyValues.Any();
        }

        public IGraphQLStatement DeepCopy()
        {
            return new GraphQLObjectValue(this);
        }
    }
}
