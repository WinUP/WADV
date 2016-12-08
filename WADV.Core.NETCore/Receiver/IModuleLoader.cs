using System;

namespace WADV.Core.NETCore.Receiver
{
    public interface IModuleLoader
    {
        bool Initialize(Type[] types);
    }
}
