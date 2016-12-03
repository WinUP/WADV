namespace WADV.Core.NETCore.Receiver
{
    public interface ILooper
    {
        /// <summary>
        /// 执行一次循环
        /// </summary>
        bool Logic(int frame);
        /// <summary>
        /// 执行一次渲染
        /// </summary>
        /// <remarks></remarks>
        void Render();

    }
}
