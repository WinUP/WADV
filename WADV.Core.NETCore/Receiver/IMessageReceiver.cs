using WADV.Core.NETCore.System;

namespace WADV.Core.NETCore.Receiver
{
    public interface IMessageReceiver
    {
        /// <summary>
        /// 执行一次消息处理
        /// </summary>
        /// <param name="message">消息标识符</param>

        void ReceiveMessage(Message message);
        /// <summary>
        /// 获取要接收的消息的Mask<br></br>
        /// Mask是按位验证的，请将需要接收的类型的二进制位置为1，其余置为0
        /// </summary>
        /// <returns></returns>
        int ReceiverMask { get; }
    }
}
