using System;

namespace WADV.Core.Attribute {
    /// <summary>
    /// Indicate that this class's instance will be auto created by game system when load the module it belonges to
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class AutoInitializeAttribute : global::System.Attribute {
    }
}