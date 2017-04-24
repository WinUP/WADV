namespace WADV.Core.Receiver {
    public interface IRenderer {
        /// <summary>
        /// Do function of render loop
        /// </summary>
        /// <param name="frame">Current frame count since game engine was started</param>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Render(int frame, out bool abort);
    }
}