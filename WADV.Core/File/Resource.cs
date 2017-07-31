using System.Collections.Generic;

namespace WADV.Core.File {
    public class Resource<T> : LinkedList<T> {
        public Resource(T initialValue) : base(new[] {initialValue}) {
        }
    }
}