using WADV.Core.System;

namespace WADV.Core.Receiver {
    /// <summary>
    /// Message service receiver
    /// </summary>
    public interface IMessager {
        /// <summary>
        /// Process a message. All changes of the message will be saved
        /// </summary>
        /// <param name="message">Message object</param>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Receive(Message message, out bool abort);

        /// <summary>
        /// Set the mask of which kind of message should be received<br/>
        /// Mask is validate by bit, set the bit to 1 if the receiver wants to receive them
        /// </summary>
        /// <returns></returns>
        int Mask { get; }
    }
}