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

using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Builder.Abstractions;
using FluentGraphQL.Builder.Constants;
using System;
using System.Globalization;

namespace FluentGraphQL.Builder.Converters
{
    public class GraphQLValueConverter : IGraphQLValueConverter
    {
        private readonly IGraphQLStringFactory _graphQLStringFactory;

        public GraphQLValueConverter(IGraphQLStringFactory graphQLStringFactory)
        {
            _graphQLStringFactory = graphQLStringFactory;
        }

        public virtual string Convert(object @object)
        {
            var type = @object.GetType().Name;
            switch (type)
            {
                case nameof(Guid):
                    return ConvertGuid((Guid)@object);
                case nameof(String):
                    return ConvertString((string)@object);
                case nameof(DateTime):
                    return ConvertDateTime((DateTime)@object);
                case nameof(Int32):
                    return ConvertInt32((int)@object);
                case nameof(Decimal):
                    return ConvertDecimal((decimal)@object);
                case nameof(Boolean):
                    return ConvertBoolean((bool)@object);
                case nameof(OrderByDirection):
                    return _graphQLStringFactory.Construct((OrderByDirection)@object);
                default:
                    return default;
            };
        }

        public virtual string ConvertGuid(Guid value)
        {
            if (value.Equals(Guid.Empty))
                return Constant.GraphQLKeyords.Null;

            return $"\"{ value }\"";
        }

        public virtual string ConvertString(string value)
        {
            return $"\"{ value }\"";
        }

        public virtual string ConvertDateTime(DateTime value)
        {
            if (value.Equals(DateTime.MinValue))
                return Constant.GraphQLKeyords.Null;

            return $"\"{ value.ToString("s", CultureInfo.InvariantCulture) }\"";
        }

        public virtual string ConvertInt32(int value)
        {
            return value.ToString();
        }

        public virtual string ConvertDecimal(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual string ConvertBoolean(bool value)
        {
            return value.ToString().ToLower();
        }        
    }
}
