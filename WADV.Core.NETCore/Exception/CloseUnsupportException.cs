namespace WADV.Core.NETCore.Exception
{
    public class CloseUnsupportException:GameException
    {
        public CloseUnsupportException() : base("Winows cannot be closed", "CloseUnsupported")
        {
        }
    }
}
