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

using FluentGraphQL.Builder.Constants;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FluentGraphQL.Builder.Extensions
{
    internal static class StringExtensions
    {
        private static readonly IEnumerable<string> _supportedMethodCalls;
        private static readonly IEnumerable<string> _extensionMethodCalls;
        private static readonly IEnumerable<string> _aggregateMethodCalls;
        private static readonly ConcurrentDictionary<string, Type> _verifiedMethodCallsCache;

        static StringExtensions()
        {            
            var supportedInfos = typeof(Constant.SupportedMethodCalls).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);            
            var extensionInfos = typeof(Constant.ExtensionMethodCalls).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);              
            var aggregateInfos = typeof(Constant.AggregateMethodCalls).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            _supportedMethodCalls = supportedInfos.Select(x => x.GetRawConstantValue() as string).ToArray();
            _extensionMethodCalls = extensionInfos.Select(x => x.GetRawConstantValue() as string).ToArray();
            _aggregateMethodCalls = aggregateInfos.Select(x => x.GetRawConstantValue() as string).ToArray();
             
            _verifiedMethodCallsCache = new ConcurrentDictionary<string, Type>();
        }

        public static string ToSnakeCaseExtended(this string @string)
        {
            var values = Regex.Matches(@string, @"(?<=<graphql-value>)(.+?)(?=</graphql-value>)");
            var valueIndexes = values.Cast<Match>().SelectMany(x => Enumerable.Range(x.Index, x.Length));

            var constructs = @string.Select((x, i) =>
            {
                if (valueIndexes.Contains(i))
                    return x.ToString();
                
                if (i > 0 && char.IsUpper(x) && !char.IsWhiteSpace(@string[i - 1]) && char.IsLetterOrDigit(@string[i - 1]))
                    return $"_{x}".ToLower();

                return char.ToLower(x).ToString();
            });

            var converted = string.Concat(constructs);
            return converted.Replace("<graphql-value>", "").Replace("</graphql-value>", "");
        }

        public static string SnakeCaseToPascalCase(this string @string)
        {
            var parts = @string.Split('_').Select(x => x.First().ToString().ToUpper() + x.Substring(1));
            return string.Join(string.Empty, parts);
        }

        public static Type EvaluateMethodCallCategory(this string methodName)
        {
            var success = _verifiedMethodCallsCache.TryGetValue(methodName, out Type cachedType);
            if (success)
                return cachedType;

            Type type = null;
            if (_supportedMethodCalls.Any(x => x.Equals(methodName)))            
                type = typeof(Constant.SupportedMethodCalls);            

            else if (_extensionMethodCalls.Any(x => x.Equals(methodName)))            
                type = typeof(Constant.ExtensionMethodCalls);                      

            else if (_aggregateMethodCalls.Any(x => x.Equals(methodName)))            
                type = typeof(Constant.AggregateMethodCalls);

            else if (type is null)
                throw new NotImplementedException(methodName);

            _verifiedMethodCallsCache.TryAdd(methodName, type);
            return type;
        }
    }    
}
