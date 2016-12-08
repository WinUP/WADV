namespace WADV.Core.NETCore.Receiver
{
    public interface IRenderer
    {
        /// <summary>
        /// Do function of render loop
        /// </summary>
        /// <param name="frame">Current frame count since game engine was started</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Render(int frame);
    }
}
