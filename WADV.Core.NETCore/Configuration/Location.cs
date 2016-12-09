using System.IO;

namespace WADV.Core.NETCore.Configuration
{
    internal class Location
    {
        /// <summary>
        /// Get or set the module directory
        /// </summary>
        internal static string Module { get; set; }
        /// <summary>
        /// Get or set the resource directory
        /// </summary>
        internal static string Resource { get; set; }
        /// <summary>
        /// Get or set the skin directory
        /// </summary>
        internal static string Skin { get; set; }
        /// <summary>
        /// Get or set the script directory
        /// </summary>
        internal static string Script { get; set; }
        /// <summary>
        /// Get or set the player directory (etc. saves, settings)
        /// </summary>
        internal static string Player { get; set; }
        /// <summary>
        /// Get or set the log directory
        /// </summary>

        internal static string Log { get; set; }
        /// <summary>
        /// Get the current directory
        /// </summary>
        internal static string GamePath { get; } = Directory.GetCurrentDirectory();

    }
}
