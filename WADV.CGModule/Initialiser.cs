using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using WADV.AppCore;
using WADV.AppCore.API;

namespace WADV.CGModule
{
    /// <summary>
    /// 提供预加载所有图像效果的功能
    /// </summary>
    internal static class Initialiser
    {
        /// <summary>
        /// 待实例化的图像效果列表
        /// </summary>
        internal static Dictionary<string, Type> EffectList;

        /// <summary>
        /// 读取并缓存所有图像效果
        /// </summary>
        internal static void LoadEffect()
        {
            EffectList = new Dictionary<string, Type> {{"BaseEffect", typeof (BaseBgra32)}};
            var basePath = PathAPI.GetPath(PathType.Resource, "CGEffect\\");
            foreach (var type in 
                     from file in System.IO.Directory.GetFiles(basePath, "*.dll") select Assembly.LoadFrom(file).GetTypes()
                     into assembly from type in assembly where type.GetInterface("IEffect") != null select type)
                EffectList.Add(type.Name, type);
            MessageAPI.SendSync("[CG]INIT_EFFECT_FINISH");
        }
    }
}
