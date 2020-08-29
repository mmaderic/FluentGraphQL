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
using System.Linq;
using System.Reflection;

namespace FluentGraphQL.Builder.Extensions
{
    public static class TypeExtensions
    {
        internal static Type Root(this Type type)
        {
            if (type.IsInterface)
                return type;

            var interfaces = type.GetInterfaces().Where(x => x.IsGenericType);
            var fragmentInterface = interfaces.FirstOrDefault(x => x.GetGenericTypeDefinition().Equals(typeof(IGraphQLFragment<>)));
            if (!(fragmentInterface is null))
                return fragmentInterface.GenericTypeArguments.First();

            if (type.BaseType.Equals(typeof(object)))
                return type;            

            type = type.BaseType;
            while (!type.BaseType.Equals(typeof(object)))
                type = type.BaseType;

            return type;
        }

        public static bool IsSimple(this Type type)
        {
            return
                type.GetTypeInfo().IsPrimitive ||
                type.Equals(typeof(string)) ||                
                type.Equals(typeof(decimal)) ||
                type.Equals(typeof(DateTime)) ||
                type.Equals(typeof(Guid));
        }
    }
}
