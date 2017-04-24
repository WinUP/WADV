using System;
using WADV.Core.Utility;

namespace WADV.Core.Receiver {
    /// <summary>
    /// Module load receiver
    /// </summary>
    public interface IModuleLoader {
        /// <summary>
        /// Initialize a module before module's initialize function
        /// </summary>
        /// <param name="types">All types in this module</param>
        /// <param name="abort">Should stop current loop after run this receiver</param>
        /// <returns>Should this receiver still avaliable in next loop</returns>
        bool Initialize(ReferenceValue<Type[]> types, out bool abort);
    }
}