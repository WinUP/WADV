﻿using WADV.Core.RAL;

namespace WADV.Core.Configuration {
    /// <summary>
    /// Variables for game system
    /// </summary>
    internal class Variable {
        /// <summary>
        /// Message's mask for system messages
        /// </summary>
        internal static int SystemMessageMask { get; set; } = 1;

        /// <summary>
        /// Message's mask for windows messages
        /// </summary>
        internal static int WindowMessageMask { get; set; } = 2;

        /// <summary>
        /// Game's main window
        /// </summary>
        internal static Window MainWindow { get; set; }

        /// <summary>
        /// Should throw error when any plugin's initialization is failed
        /// </summary>
        internal static bool ThrowPluginInitializeFailed { get; set; }
    }
}