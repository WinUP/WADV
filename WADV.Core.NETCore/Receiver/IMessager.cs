using WADV.Core.NETCore.System;

namespace WADV.Core.NETCore.Receiver
{
    /// <summary>
    /// Message service receiver
    /// </summary>
    public interface IMessager
    {
        /// <summary>
        /// Process a messae. All changes of the message will be saved
        /// </summary>
        /// <param name="message">message object</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Receive(Message message);
        /// <summary>
        /// Set the mask of which kind of message should be received<br/>
        /// Mask is validate by bit, set the bit to 1 if the receiver wants to receive them
        /// </summary>
        /// <returns></returns>
        int Mask { get; }
    }
}
