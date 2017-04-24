using System;
using System.Linq;
using System.Collections.Generic;

namespace WADV.Core.RAL {
    /// <summary>
    /// Render node is the basic structure in Render Abstract Layer, which acts as renderable atomic unit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RenderNode<T> : IDisposable {
        public delegate void MapChildren(string index, T child);

        /// <summary>
        /// Children of this render node
        /// </summary>
        protected readonly Dictionary<string, T> ChildrenDictionary = new Dictionary<string, T>();

        public RenderNode(string name) {
            Name = name;
        }

        public RenderNode() {
            Name = Guid.NewGuid().ToString().ToUpper();
        }

        /// <summary>
        /// Name of this render node
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Children of this render node
        /// </summary>
        public T[] Children => ChildrenDictionary.Values.ToArray();
        public T this[string key] => ChildrenDictionary[key];

        /// <summary>
        /// Map all children
        /// </summary>
        /// <param name="mapper">Mapper</param>
        public void Map(MapChildren mapper) {
            foreach (var e in ChildrenDictionary)
                mapper.Invoke(e.Key, e.Value);
        }

        /// <summary>
        /// Dispose all resources of this render node
        /// </summary>
        public abstract void Dispose();
    }
}