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

using System.Text.Json;

namespace FluentGraphQL.Client.Extensions
{
    internal static class JsonNamingPolicyExtensions
    {
        private static readonly SnakeCasePolicy _snakeCasePolicy;

        static JsonNamingPolicyExtensions()
        {
            _snakeCasePolicy = new SnakeCasePolicy();
        }

        public static SnakeCasePolicy SnakeCase()
        {
            return _snakeCasePolicy;
        }

        public class SnakeCasePolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name)
            {
                return name.ToSnakeCase();
            }
        }
    }
}
