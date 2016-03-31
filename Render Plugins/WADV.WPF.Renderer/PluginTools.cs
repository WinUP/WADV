using System;
using System.Windows.Controls;

namespace WADV.WPF.Renderer
{
    internal static class PluginTools
    {
        internal static WindowDelegate Window;

        /// <summary>
        /// 获得一个新的Canvas
        /// </summary>
        /// <returns></returns>
        internal static Canvas NewCanvas()
        {
            return (Canvas) Window?.Run(new Func<Canvas>(() => new Canvas()), true);
        }
    }
}
