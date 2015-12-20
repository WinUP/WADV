namespace WADV.WPF.Renderer.Exception
{
    /// <summary>
    /// 表示无法找到可操作窗口的异常
    /// </summary>
    public class NullWindowException : System.Exception
    {
        public NullWindowException()
            : base("找不到可以操作的窗口。插件是否还没有初始化？")
        {

        }
    }
}
