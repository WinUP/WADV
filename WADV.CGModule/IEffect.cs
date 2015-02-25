namespace WADV.CGModule
{
    /// <summary>
    /// 包括图像效果需要实现的功能
    /// </summary>
    public interface IEffect
    {
        void GetNextImageState(int frame);
        bool IsFinished();
        int GetWidth();
        int GetHeight();
        byte[] GetPixel();
    }
}
