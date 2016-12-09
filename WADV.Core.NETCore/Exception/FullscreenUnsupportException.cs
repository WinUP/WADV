namespace WADV.Core.NETCore.Exception
{
    public class FullscreenUnsupportException:GameException
    {
        public FullscreenUnsupportException() : base("The windows cannot be in fullscreen", "FullscreenUnsupport")
        {
        }
    }
}
