using System;
using System.Linq;
using System.Collections.Generic;

namespace WADV.Core.NETCore.Receiver
{
    /// <summary>
    /// List of a type of receivers
    /// </summary>
    /// <typeparam name="T">Receiver type</typeparam>
    /// <typeparam name="TU">Paramater type</typeparam>
    internal sealed class ReceiverList<T, TU>
    {
        /// <summary>
        /// Procedure of process each node when mappin the tree
        /// </summary>
        /// <param name="list">Tree itself</param>
        /// <param name="receiver">Receiver which is on mapping</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        internal delegate bool Mapper(ReceiverList<T, TU> list, T receiver, TU parameter);

        private readonly ReceiverNode<T> _root = new ReceiverNode<T>();
        private readonly Dictionary<ReceiverNode<T>, ReceiverNode<T>> _needToAdd = new Dictionary<ReceiverNode<T>, ReceiverNode<T>>();
        private readonly List<ReceiverNode<T>> _needToRemove = new List<ReceiverNode<T>>();
        private readonly Dictionary<ReceiverNode<T>, int> _needToChangeSequence = new Dictionary<ReceiverNode<T>, int>();
        private readonly Dictionary<ReceiverNode<T>, ReceiverNode<T>> _needToChangeParent = new Dictionary<ReceiverNode<T>, ReceiverNode<T>>();
        private readonly Dictionary<ReceiverNode<T>, int> _index = new Dictionary<ReceiverNode<T>, int>();
        private readonly Mapper _mapper;

        public ReceiverList(Mapper mapper)
        {
            _index.Add(_root, 1);
            _mapper = mapper;
        }

        /// <summary>
        /// Tree node status
        /// </summary>
        private enum NodeStatus
        {
            /// <summary>
            /// Already in receiver list [000001]
            /// </summary>
            InReceiver = 1,
            /// <summary>
            /// Prepare to add [000010]
            /// </summary>
            InAdd = 2,
            /// <summary>
            /// Prepare to remove [000100]
            /// </summary>
            InRemove = 4,
            /// <summary>
            /// Prepare to change sequence [001000]
            /// </summary>
            InChangeSequence = 8,
            /// <summary>
            /// Prepare to change parent [010000]
            /// </summary>
            InChangeParent = 16

        }

        /// <summary>
        /// Get the count of this list
        /// </summary>
        internal int Count
        {
            get
            {
                lock (_index)
                {
                    return _index.Count;  
                }
            }
        }
        /// <summary>
        /// Indicate that if target receiver is belongs to this list
        /// </summary>
        /// <param name="target">Receiver which want to be indicated</param>
        /// <returns></returns>
        internal bool Contains(T target)
        {
            lock (_index)
            {
                return _index.Any(e => e.Key.Receiver.Equals(target));
            }
        }

        private bool Contains(T target, out KeyValuePair<ReceiverNode<T>, int> key)
        {
            lock (_index)
            {
                key = _index.FirstOrDefault(e => e.Key.Receiver.Equals(target));
                return key.Key != null;
            }
        }
        /// <summary>
        /// Add a new receiver to root
        /// </summary>
        /// <param name="target">Receiver which should be added</param>
        /// <returns></returns>
        internal bool AddToRoot(T target)
        {
            lock (_needToAdd)
            {
                KeyValuePair<ReceiverNode<T>, int> itemNode;
                if (Contains(target, out itemNode)) return false;
                if ((itemNode.Value & (int) NodeStatus.InAdd) != 0)
                {
                    _needToAdd[itemNode.Key] = _root;
                    return true;
                }
                var result = new ReceiverNode<T>(target);
                _needToAdd.Add(result, _root);
                lock (_index) _index.Add(result, 2); //000010
                return true;
            }
        }
        /// <summary>
        /// Add a new receiver as a child of another receiver
        /// </summary>
        /// <param name="target">Receiver which should be added</param>
        /// <param name="parent">Receiver which should be acted as parent</param>
        /// <param name="sequence">Run sequence of the new receiver</param>
        /// <returns></returns>
        internal bool Add(T target, T parent, int sequence = 0)
        {
            lock (_needToAdd)
            {
                KeyValuePair<ReceiverNode<T>, int> itemNode;
                if (Contains(target, out itemNode)) return false;
                KeyValuePair<ReceiverNode<T>, int> parentNode;
                if (!Contains(parent, out parentNode)) return false;
                if ((itemNode.Value & (int)NodeStatus.InAdd) != 0)
                {
                    _needToAdd[itemNode.Key] = parentNode.Key;
                    return true;
                }
                var result = new ReceiverNode<T>(target) { RunSequence = sequence };
                _needToAdd.Add(result, parentNode.Key);
                lock (_index) _index.Add(result, 2); //100010
                return true;
            }
        }
        /// <summary>
        /// Delete a receiver
        /// </summary>
        /// <param name="target">Receiver which should be deleted</param>
        /// <returns></returns>
        internal bool Delete(T target)
        {
            lock (_needToRemove)
            {
                KeyValuePair<ReceiverNode<T>, int> itemNode;
                if (Contains(target, out itemNode)) return false;
                if ((itemNode.Value & (int)NodeStatus.InRemove) != 0) return true;
                _needToRemove.Add(itemNode.Key);
                lock (_index) _index[itemNode.Key] |= 4; //000100
                return true;
            }
        }
        /// <summary>
        /// Change sequence of a receiver
        /// </summary>
        /// <param name="target">Receiver which should be changed</param>
        /// <param name="newSequence">New sequence</param>
        /// <returns></returns>
        internal bool SetSequence(T target, int newSequence)
        {
            lock (_needToChangeSequence)
            {
                KeyValuePair<ReceiverNode<T>, int> itemNode;
                if (Contains(target, out itemNode)) return false;
                if ((itemNode.Value & (int)NodeStatus.InChangeSequence) != 0)
                {
                    _needToChangeSequence[itemNode.Key] = newSequence;
                    return true;
                }
                _needToChangeSequence.Add(itemNode.Key, newSequence);
                lock (_index) _index[itemNode.Key] |= 8; //001000
                return true;
            }
        }

        /// <summary>
        /// Change parent of a receiver
        /// </summary>
        /// <param name="target">Receiver which its parent should be changed</param>
        /// <param name="newParent">New parent</param>
        /// <returns></returns>
        internal bool SetParent(T target, T newParent)
        {
            lock (_needToChangeParent)
            {
                KeyValuePair<ReceiverNode<T>, int> itemNode;
                if (Contains(target, out itemNode)) return false;
                KeyValuePair<ReceiverNode<T>, int> parentNode;
                if (!Contains(newParent, out parentNode)) return false;
                if ((itemNode.Value & (int)NodeStatus.InChangeParent) != 0)
                {
                    _needToChangeParent[itemNode.Key] = parentNode.Key;
                    return true;
                }
                _needToChangeParent.Add(itemNode.Key, parentNode.Key);
                lock (_index) _index[itemNode.Key] |= 16; //010000
                return true;
            }
        }

        /// <summary>
        /// Change parent to root of a receiver
        /// </summary>
        /// <param name="target">Receiver which its parent should be changed</param>
        /// <returns></returns>
        internal bool SetParentToRoot(T target)
        {
            lock (_needToChangeParent)
            {
                KeyValuePair<ReceiverNode<T>, int> itemNode;
                if (Contains(target, out itemNode)) return false;
                if ((itemNode.Value & (int)NodeStatus.InChangeParent) != 0)
                {
                    _needToChangeParent[itemNode.Key] = _root;
                    return true;
                }
                _needToChangeParent.Add(itemNode.Key, _root);
                lock (_index) _index[itemNode.Key] |= 16; //010000
                return true;
            }
        }

        internal void Clear()
        {
            _root.Children.Clear();
            GC.Collect();
        }

        private static void Link(ReceiverNode<T> parent, ReceiverNode<T> child)
        {
            child.Parent.Children.Remove(child);
            var pointer = parent.Children.First;
            while (pointer != null)
            {
                if (pointer.Value.RunSequence > child.RunSequence)
                {
                    parent.Children.AddBefore(pointer, child);
                    break;
                }
                pointer = pointer.Next;
                if (pointer == null) parent.Children.AddLast(child);
            }
            child.Parent = parent;
        }

        /// <summary>
        /// Map the list
        /// </summary>
        internal TU Map(TU parameter)
        {
            lock (_index)
            {
                lock (_root)
                {
                    lock (_needToAdd)
                    {
                        foreach (var e in _needToAdd)
                        {
                            Link(e.Value, e.Key);
                            _index[e.Key] &= 29; //011101
                            _index[e.Key] |= 1; //000001
                        }
                        _needToAdd.Clear();
                        lock (_needToChangeSequence)
                        {
                            foreach (var e in _needToChangeSequence)
                            {
                                var child = e.Key;
                                Link(child.Parent, child);
                                _index[e.Key] &= 23; //010111
                            }
                            _needToChangeSequence.Clear();
                            lock (_needToChangeParent)
                            {
                                foreach (var e in _needToChangeParent)
                                {
                                    Link(e.Value, e.Key);
                                    _index[e.Key] &= 15; //001111
                                }
                                _needToChangeParent.Clear();
                                lock (_needToRemove)
                                {
                                    foreach (var e in _needToRemove)
                                    {
                                        e.Parent.Children.Remove(e);
                                        foreach (var subchild in e.Children)
                                        {
                                            Link(e.Parent, subchild);
                                        }
                                        e.Parent = null;
                                        _index.Remove(e);
                                    }
                                    _needToRemove.Clear();
                                    if (_root.Children.Count == 0) return parameter;
                                    var walkStack = new Stack<LinkedListNode<ReceiverNode<T>>>();
                                    var listPointer = _root.Children.First;
                                    walkStack.Push(new LinkedListNode<ReceiverNode<T>>(_root));
                                    while (walkStack.Count > 0)
                                    {
                                        if (!_mapper(this, listPointer.Value.Receiver, parameter))
                                            _needToRemove.Add(listPointer.Value);
                                        if (listPointer.Value.Children.Count > 0)
                                        {
                                            walkStack.Push(listPointer);
                                            listPointer = listPointer.Value.Children.First;
                                            while (!listPointer.Value.Enabled)
                                            {
                                                while (listPointer.Next == null)
                                                {
                                                    while (walkStack.Count > 0)
                                                    {
                                                        listPointer = walkStack.Pop();
                                                        if (listPointer.Next == null) continue;
                                                        break;
                                                    }
                                                    if (walkStack.Count == 0) return parameter;
                                                }
                                                listPointer = listPointer.Next;
                                            }
                                            continue;
                                        }
                                        if (listPointer.Next != null)
                                        {
                                            listPointer = listPointer.Next;
                                            while (!listPointer.Value.Enabled)
                                            {
                                                if (listPointer.Next == null) break;
                                                listPointer = listPointer.Next;
                                            }
                                            if (listPointer.Value.Enabled) continue;
                                        }
                                        while (walkStack.Count > 0)
                                        {
                                            listPointer = walkStack.Pop();
                                            if (listPointer.Next == null) continue;
                                            while (listPointer.Next != null)
                                            {
                                                listPointer = listPointer.Next;
                                                if (listPointer.Value.Enabled) break;
                                            }
                                            if (listPointer.Value.Enabled) break;
                                        }
                                        if (walkStack.Count == 0) return parameter;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return parameter;
        }
    }
}