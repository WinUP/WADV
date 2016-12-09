using System;
using System.Linq;
using System.Collections.Generic;

namespace WADV.Core.NETCore.RAL
{
    public abstract class RenderNode<T>
    {
        public delegate void MapChildren(string index, T child);
        protected readonly Dictionary<string, T> ChildrenDictionary = new Dictionary<string, T>();

        protected RenderNode(string name)
        {
            Name = name;
        }
        protected RenderNode()
        {
            Name = Guid.NewGuid().ToString().ToUpper();
        }

        public string Name { get; }
        public T[] Children => ChildrenDictionary.Values.ToArray();
        public T this[string key] => ChildrenDictionary[key];

        public void Map(MapChildren mapper)
        {
            foreach (var e in ChildrenDictionary)
                mapper.Invoke(e.Key, e.Value);
        }
    }
}