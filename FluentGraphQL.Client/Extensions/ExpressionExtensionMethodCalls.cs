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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentGraphQL.Client.Extensions
{
    public static class ExpressionExtensionMethodCalls
    {
        public static bool GreaterThan(this string a, string b)
        {
            return default;
        }

        public static bool LessThan(this string a, string b)
        {
            return default;
        }  

        public static bool In(this object a, IEnumerable b)
        {
            return default;
        }

        public static bool NotIn(this object a, IEnumerable b)
        {
            return default;
        }

        public static bool Like(this string a, string b)
        {
            return default;
        }

        public static bool NotLike(this string a, string b)
        {
            return default;
        }

        public static bool LikeInsensitive(this string a, string b)
        {
            return default;
        }

        public static bool NotLikeInsensitive(this string a, string b)
        {
            return default;
        }

        public static bool Similar(this string a, string b)
        {
            return default;
        }

        public static bool NotSimilar(this string a, string b)
        {
            return default;
        }

        public static IEnumerable<TEntity> Include<TEntity, TNode>(this IEnumerable<TEntity> collection, Expression<Func<TEntity, TNode>> nodeSelector)
            where TEntity : IGraphQLEntity
        {
            return collection;
        }

        public static TEntity Include<TEntity, TNode>(this TEntity entity, Expression<Func<TEntity, TNode>> nodeSelector)
            where TEntity : IGraphQLEntity
        {
            return entity;
        }
    }
}
