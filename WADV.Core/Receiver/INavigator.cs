using WADV.Core.RAL;

namespace WADV.Core.Receiver {
    /// <summary>
    /// Navigation receiver
    /// </summary>
    public interface INavigator {
        /// <summary>
        /// Process navigation
        /// </summary>
        /// <param name="e">Parameters for this navigation</param>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Navigate(NavigationParameter2D e, out bool abort);
    }
}
