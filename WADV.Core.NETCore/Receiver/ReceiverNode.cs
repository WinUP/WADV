using System.Collections.Generic;

namespace WADV.Core.NETCore.Receiver
{
    public sealed class ReceiverNode<T> where T: ILooper, IMessageReceiver
    {
        public ReceiverNode()
        {
            
        }

        public ReceiverNode(T receiver)
        {
            Receiver = receiver;
        }

        public int RunSequence { get; internal set; } = 0;

        public bool Enabled { get; set; } = true;

        internal ReceiverNode<T> Parent = null;

        internal readonly LinkedList<ReceiverNode<T>> Children = new LinkedList<ReceiverNode<T>>();

        public T Receiver;
    }
}
