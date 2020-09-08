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

using FluentGraphQL.Client.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FluentGraphQL.Client.Responses
{
    public class GraphQLMutationReturningResponse<TReturn> : IGraphQLMutationReturningResponse<TReturn>, IList
    {   
        public int AffectedRows { get; set; }
        public List<TReturn> Returning { get; set; }

        public int Count => Returning.Count;
        public bool IsReadOnly => false;

        bool IList.IsFixedSize => false;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => Returning;

        object IList.this[int index] { 
            get => Returning[index];
            set => Returning[index] = (TReturn)value;
        }

        public TReturn this[int index] { 
            get => Returning[index];
            set => Returning[index] = value;               
        }

        public GraphQLMutationReturningResponse()
        {
            Returning = new List<TReturn>();
        }

        public int IndexOf(TReturn item)
        {
            return Returning.IndexOf(item);
        }

        public void Insert(int index, TReturn item)
        {
            Returning.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Returning.RemoveAt(index);
        }

        public void Add(TReturn item)
        {
            Returning.Add(item);
        }

        public void Clear()
        {
            Returning.Clear();
        }

        public bool Contains(TReturn item)
        {
            return Returning.Contains(item);
        }

        public void CopyTo(TReturn[] array, int arrayIndex)
        {
            Returning.CopyTo(array, arrayIndex);
        }

        public bool Remove(TReturn item)
        {
            return Returning.Remove(item);
        }

        public IEnumerator<TReturn> GetEnumerator()
        {
            return Returning.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Returning.GetEnumerator();
        }

        int IList.Add(object value)
        {
            Returning.Add((TReturn)value);
            return Returning.Count - 1;
        }

        bool IList.Contains(object value)
        {
            return Returning.Contains((TReturn)value);
        }

        int IList.IndexOf(object value)
        {
            return Returning.IndexOf((TReturn)value);
        }

        void IList.Insert(int index, object value)
        {
            Returning.Insert(index, (TReturn)value);
        }

        void IList.Remove(object value)
        {
            Returning.Remove((TReturn)value);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Returning.CopyTo((TReturn[])array, index);
        }
    }
}
