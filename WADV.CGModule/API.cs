using System;
using System.Windows.Controls;
using WADV.AppCore.API;

namespace WADV.CGModule.API
{
    /// <summary>
    /// 图像效果API类
    /// </summary>
    public class ImageAPI {

        /// <summary>
        /// 显示一个带效果的图像
        /// </summary>
        /// <param name="fileName">图像路径(Resource目录下)</param>
        /// <param name="effectName">效果名称</param>
        /// <param name="duration">效果持续时间</param>
        /// <param name="contentName">图像显示区域的名称</param>
        /// <returns></returns>
        public static bool Show(string fileName, string effectName, int duration, string contentName) {
            if (Config.DPI < 1) return false;
            if (!Effect.Initialiser.EffectList.ContainsKey(effectName)) return false;
            Effect.IEffect effect = (Effect.BaseBgra32)Activator.CreateInstance(Effect.Initialiser.EffectList[effectName], new object[] { fileName, duration });
            var content = WindowAPI.GetChildByName<Panel>(WindowAPI.GetWindow(), contentName);
            if (content == null) return false;
            PluginInterface.LoopReceiver loopContent = new PluginInterface.LoopReceiver(effect, content);
            MessageAPI.SendSync("CG_SHOW_BEFORE");
            LoopingAPI.AddLoopSync(loopContent);
            LoopingAPI.WaitLoopSync(loopContent);
            MessageAPI.SendSync("CG_SHOW_AFTER");
            return true;
        }

        /// <summary>
        /// 清理对应区域的CG
        /// </summary>
        /// <param name="contentName">要清理的区域的名称</param>
        /// <returns></returns>
        public static bool Clean(string contentName) {
            var content = WindowAPI.GetChildByName<Panel>(WindowAPI.GetWindow(), contentName);
            if (content == null) return false;
            content.Background = null;
            return true;
        }
    }

    /// <summary>
    /// 设置API类
    /// </summary>
    public class ConfigAPI {

        /// <summary>
        /// 初始化CG模块
        /// </summary>
        /// <param name="dpi">图像默认DPI</param>
        /// <returns></returns>
        public static void Init(int dpi)
        {
            Config.DPI = dpi;
            Effect.Initialiser.LoadEffect();
            MessageAPI.SendSync("CG_INIT_ALLFINISH");
        }
    }
}
