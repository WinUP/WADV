﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WADV.Core.RAL {
    public abstract class Resource<T> : Component, ICollection<T> {

        public IEnumerator<T> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(T item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(T item) {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(T item) {
            throw new NotImplementedException();
        }

        public LinkedListNode<T> First { get; }

        public LinkedListNode<T> Last { get; }

        bool ICollection<T>.IsReadOnly => false;

        public int Count { get; private set; }

        public bool IsReadOnly { get; }
    }
}