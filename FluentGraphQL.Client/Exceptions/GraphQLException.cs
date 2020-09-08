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

using FluentGraphQL.Client.Models;
using System;
using System.Linq;

namespace FluentGraphQL.Client.Exceptions
{
    public class GraphQLException : ApplicationException
    {
        public GraphQLError[] Errors { get; set; }
        public string QueryString { get; set; }

        public GraphQLException(GraphQLError[] errors, string queryString = null) : base(string.Join("\n", errors.Select(x => x.Message)))
        {
            Errors = errors;
            QueryString = queryString;
        }
    }
}
