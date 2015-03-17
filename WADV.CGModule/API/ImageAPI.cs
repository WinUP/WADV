using System;
using System.Windows.Controls;
using WADV.Core.API;

namespace WADV.CGModule.API
{
    /// <summary>
    /// 图像效果API类
    /// </summary>
    public static class ImageAPI
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
            if (!Initialiser.EffectList.ContainsKey(effectName)) return false;
            IEffect effect = (BaseBgra32)Activator.CreateInstance(Initialiser.EffectList[effectName], new object[] { fileName, duration });
            var content = WindowAPI.GetChildByName<Panel>(WindowAPI.GetWindow(), contentName);
            if (content == null) return false;
            PluginInterface.LoopReceiver loopContent = new PluginInterface.LoopReceiver(effect, content);
            MessageAPI.SendSync("[CG]SHOW_BEFORE");
            LoopAPI.AddLoopSync(loopContent);
            LoopAPI.WaitLoopSync(loopContent);
            MessageAPI.SendSync("[CG]SHOW_AFTER");
            return true;
        }

        /// <summary>
        /// 清理对应区域的CG
        /// </summary>
        /// <param name="contentName">要清理的区域的名称</param>
        /// <returns></returns>
        public static bool Clean(string contentName)
        {
            var content = WindowAPI.GetChildByName<Panel>(WindowAPI.GetWindow(), contentName);
            if (content == null) return false;
            content.Background = null;
            return true;
        }
    }
}
