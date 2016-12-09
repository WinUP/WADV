namespace WADV.Core.NETCore.Exception
{
    public class MessageDequeueFailedException : GameException
    {
        public MessageDequeueFailedException() 
            : base("Deque message failed from message service", "MessageDequeueFailed")
        {
        }
    }
}
