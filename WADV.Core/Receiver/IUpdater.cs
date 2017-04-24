namespace WADV.Core.Receiver {
    /// <summary>
    /// Game loop receiver
    /// </summary>
    public interface IUpdater {
        /// <summary>
        /// Do function of logic loop
        /// </summary>
        /// <param name="frame">Current frame count since game engine was started</param>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Update(int frame, out bool abort);
    }
}