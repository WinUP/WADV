namespace WADV.WPF.Renderer
{
    public static class Extension
    {
        /// <summary>
        /// 准备插件启动数据<br></br>
        /// 必须设置完游戏系统主渲染窗口后才能使用<br></br>
        /// 异常：<br></br>
        /// UnsupportedWindowException
        /// </summary>
        public static void Plug()
        {
            var target = Core.API.Game.Window();
            if (!(target is WindowDelegate)) throw new Exception.UnsupportedWindowException();
            PluginTools.Window = (WindowDelegate)target;
        }

        /// <summary>
        /// 获取WindowDelegate类型的游戏系统主窗口<br></br>
        /// 异常：<br></br>
        /// UnsupportedWindowException
        /// </summary>
        /// <returns></returns>
        public static WindowDelegate WpfGameWindow()
        {
            var target = Core.API.Game.Window();
            if (!(target is WindowDelegate)) throw new Exception.UnsupportedWindowException();
            return (WindowDelegate)target;
        }
    }
}
