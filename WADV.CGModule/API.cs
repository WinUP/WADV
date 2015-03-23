using System;
using System.Windows.Controls;
using WADV.AppCore.API;

namespace WADV.CGModule.API
{
    /// <summary>
    /// 图像效果API类
    /// </summary>
    public class ImageAPI
    {
        /// <summary>
        /// 显示一个带效果的图像
        /// </summary>
        /// <param name="fileName">图像路径(Resource目录下)</param>
        /// <param name="effectName">效果名称</param>
        /// <param name="duration">效果持续时间</param>
        /// <param name="contentName">图像显示区域的名称</param>
        /// <returns></returns>
        public static bool Show(string fileName, string effectName, int duration, string contentName)
        {
            if (Config.DPI < 1) return false;
            if (!Effect.Initialiser.EffectList.ContainsKey(effectName)) return false;
            Effect.IEffect effect = (Effect.BaseBGRA32)Activator.CreateInstance(Effect.Initialiser.EffectList[effectName], new object[] { fileName, duration });
            var content = WindowAPI.GetChildByName<Panel>(WindowAPI.GetWindow(), contentName);
            if (content == null) return false;
            PluginInterface.ImageLoop loopContent = new PluginInterface.ImageLoop(effect, content);
            MessageAPI.SendSync("CG_SHOW_BEFORE");
            LoopingAPI.AddLoopSync(loopContent);
            LoopingAPI.WaitLoopSync(loopContent);
            MessageAPI.SendSync("CG_SHOW_AFTER");
            return true;
        }

        /// <summary>
        /// 初始化CG模块
        /// </summary>
        /// <param name="dpi">图像默认DPI</param>
        /// <returns></returns>
        public static void Init(int dpi)
        {
            Config.DPI = dpi;
        }
    }
}
