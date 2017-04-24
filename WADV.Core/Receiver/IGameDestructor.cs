using WADV.Core.Utility;

namespace WADV.Core.Receiver {
    /// <summary>
    /// Game destruct receiver
    /// </summary>
    public interface IGameDestructor {
        /// <summary>
        /// Destruct game
        /// </summary>
        /// <param name="e">Destruct parameters</param>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Destruct(CancelEventArgs e, out bool abort);
    }
}