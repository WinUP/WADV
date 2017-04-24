namespace WADV.Core.Receiver {
    /// <summary>
    /// Game initialize receiver
    /// </summary>
    public interface IGameInitializer {
        /// <summary>
        /// Initialize game
        /// </summary>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Initialize(out bool abort);
    }
}