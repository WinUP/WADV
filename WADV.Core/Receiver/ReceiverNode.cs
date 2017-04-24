using System.Collections.Generic;

namespace WADV.Core.Receiver {
    /// <summary>
    /// Tree node for receiver list
    /// </summary>
    /// <typeparam name="T">Receiver type</typeparam>
    public sealed class ReceiverNode<T> {
        public ReceiverNode() {
        }

        public ReceiverNode(T receiver) {
            Receiver = receiver;
        }

        /// <summary>
        /// Sequence of this node when map the tree
        /// </summary>
        public int RunSequence { get; internal set; } = 0;

        /// <summary>
        /// Get or set the avaliability of this node 
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Indicate the parent of this node
        /// </summary>
        internal ReceiverNode<T> Parent = null;

        /// <summary>
        /// Indicate a list of children of this node
        /// </summary>
        internal readonly LinkedList<ReceiverNode<T>> Children = new LinkedList<ReceiverNode<T>>();

        /// <summary>
        /// The receiver of this node
        /// </summary>
        public T Receiver;
    }
}