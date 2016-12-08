using WADV.Core.NETCore.Utility;

namespace WADV.Core.NETCore.Receiver
{
    public interface IGameDestructor
    {
        bool Destruct(CancelEventArgs e);
    }
}
