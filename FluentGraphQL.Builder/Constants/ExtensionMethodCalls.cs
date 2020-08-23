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

namespace FluentGraphQL.Builder.Constants
{
    public static partial class Constant 
    {
        internal static class ExtensionMethodCalls
        {
            public const string GreaterThan = "GreaterThan";
            public const string LessThan = "LessThan";
            public const string In = "In";
            public const string NotIn = "NotIn";
            public const string Like = "Like";
            public const string NotLike = "NotLike";
            public const string LikeInsensitive = "LikeInsensitive";
            public const string NotLikeInsensitive = "NotLikeInsensitive";
            public const string Similar = "Similar";
            public const string NotSimilar = "NotSimilar";
        }
    }
}
