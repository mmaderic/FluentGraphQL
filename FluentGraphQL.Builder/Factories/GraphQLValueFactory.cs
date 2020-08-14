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
using FluentGraphQL.Builder.Atoms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentGraphQL.Builder.Factories
{
    public class GraphQLValueFactory : IGraphQLValueFactory
    {
        private readonly IGraphQLValueConverter _graphQLValueConverter;  
        
        public GraphQLValueFactory(IGraphQLValueConverter graphQLValueConverter)
        {
            _graphQLValueConverter = graphQLValueConverter;
        }

        public virtual IGraphQLValue Construct(object @object)
        {
            if (@object is null)
                return new GraphQLPropertyValue("null");

            var valueLiteral = _graphQLValueConverter.Convert(@object);
            if (!(valueLiteral is null))
                return new GraphQLPropertyValue(valueLiteral);

            if (@object is IEnumerable enumerable)
            {
                var collection = ConstructCollection(enumerable);
                return new GraphQLCollectionValue(collection);
            }            

            return new GraphQLObjectValue(ConstructObject(@object)); 
        }  
        
        public virtual IEnumerable<IGraphQLValue> ConstructCollection(IEnumerable enumerable)
        {
            var collection = enumerable.Cast<object>();
            return collection.Select(x => Construct(x)).ToArray();
        }

        public virtual IEnumerable<IGraphQLValueStatement> ConstructObject(object @object)
        {
            var properties = @object.GetType().GetProperties();
            return properties.Select(x => new GraphQLValueStatement(x.Name, Construct(x.GetValue(@object))));
        }
    }
}
