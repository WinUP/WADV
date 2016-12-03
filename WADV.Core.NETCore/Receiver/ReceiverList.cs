using System.Linq;
using System.Collections.Generic;

namespace WADV.Core.NETCore.Receiver
{
    internal sealed class ReceiverList<T> where T : ILooper, IMessageReceiver
    {
        internal delegate bool Mapper(ReceiverNode<T> root, T receiver);

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

        internal bool Add(T target, T parent)
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
                var result = new ReceiverNode<T>(target);
                _needToAdd.Add(result, parentNode.Key);
                lock (_index) _index.Add(result, 2); //100010
                return true;
            }
        }

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

        internal void Map()
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
                                    if (_root.Children.Count == 0) return;
                                    var walkStack = new Stack<LinkedListNode<ReceiverNode<T>>>();
                                    var listPointer = _root.Children.First;
                                    walkStack.Push(new LinkedListNode<ReceiverNode<T>>(_root));
                                    while (walkStack.Count > 0)
                                    {
                                        _mapper(_root, listPointer.Value.Receiver);
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
                                                    if (walkStack.Count == 0) return;
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
                                        if (walkStack.Count == 0) return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
