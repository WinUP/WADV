namespace WADV.WPF.Renderer.Exception
{
    /// <summary>
    /// 表示尝试使用不受支持的窗体的异常
    /// </summary>
    public class UnsupportedWindowException : System.Exception
    {
        public UnsupportedWindowException()
            : base("你正在尝试使用不受支持的窗体。游戏窗口必须由本插件提供的WindowDelegate托管。")
        {
            
        }
    }
}
