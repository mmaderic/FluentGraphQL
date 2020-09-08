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

using System.Text.RegularExpressions;

namespace FluentGraphQL.Client.Extensions
{
    internal static class StringExtensions
    {
        public static string ToSnakeCase(this string @string)
        {
            var result = Regex.Replace(@string, "[A-Z]", "_$0").ToLower();
            if (result[0] == '_')
                result = result.Substring(1);

            return result;
        }
    }
}
