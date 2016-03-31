namespace WADV.WPF.Renderer.Exception
{
    /// <summary>
    /// 表示尝试加载不受支持的资源词典格式的错误
    /// </summary>
    public class UnsupportDictionaryFormatException : System.Exception
    {
        public UnsupportDictionaryFormatException()
            : base("你正在尝试加载不受支持的资源词典格式")
        {

        }
    }
}