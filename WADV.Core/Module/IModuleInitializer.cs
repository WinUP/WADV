namespace WADV.Core.NETCore.Module {
    /// <summary>
    /// Module initializer
    /// </summary>
    public interface IModuleInitializer {
        /// <summary>
        /// Initialize this module
        /// </summary>
        /// <returns>Should game system load this module</returns>
        bool Initialize();
    }
}