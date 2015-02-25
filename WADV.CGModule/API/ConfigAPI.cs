using WADV.AppCore.API;

namespace WADV.CGModule.API
{
    /// <summary>
    /// 设置API类
    /// </summary>
    public static class ConfigAPI
    {

        /// <summary>
        /// 初始化CG模块
        /// </summary>
        /// <param name="dpi">图像默认DPI</param>
        /// <returns></returns>
        public static void Init(int dpi)
        {
            Config.DPI = dpi;
            Initialiser.LoadEffect();
            MessageAPI.SendSync("[CG]INIT_FINISH");
        }
    }
}
