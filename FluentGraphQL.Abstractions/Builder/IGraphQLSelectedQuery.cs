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

using System;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLSelectedQuery
    {
        object InvokeSelector(object @object);
    }

    public interface IGraphQLSelectedQuery<TEntity, TResult> : IGraphQLSelectedQuery
    {
        Func<TEntity, TResult> Selector { get; }
    }

    public interface IGraphQLStandardSelectedQuery<TEntity, TResult> : IGraphQLSelectedQuery<TEntity, TResult>, IGraphQLStandardQuery<TResult>
    {
        IGraphQLStandardQuery<TEntity> AsNamed();
    }

    public interface IGraphQLSingleSelectedQuery<TEntity, TResult> : IGraphQLSelectedQuery<TEntity, TResult>, IGraphQLSingleQuery<TResult>
    {
        IGraphQLSingleQuery<TEntity> AsNamed();
    }
}
