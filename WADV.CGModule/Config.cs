using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using WADV.AppCore.API;

namespace WADV.CGModule
{
    class Config
    {
        private static int dpi;

        protected static internal int DPI
        {
            get
            {
                return dpi;
            }
            set
            {
                dpi = value;
                WriteConfig();
            }
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        protected static internal void ReadConfigFile()
        {
            XmlDocument configFile = new XmlDocument();
            configFile.Load(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile, "WADV.CGModule.xml"));
            dpi = Convert.ToInt32(configFile.SelectSingleNode("/config/dpi").InnerXml);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private static void WriteConfig()
        {
            XmlDocument configFile = new XmlDocument();
            configFile.Load(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile, "WADV.CGModule.xml"));
            configFile.SelectSingleNode("/config/dpi").InnerXml = Convert.ToString(DPI);
            configFile.Save(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile, "WADV.CGModule.xml"));
        }

        /// <summary>
        /// 转换BGRA像素数组为图片
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="pixel">像素数组</param>
        /// <returns>转换完成后的图片</returns>
        public static BitmapSource ConvertToImage(int width, int height, byte[] pixel)
        {
            return BitmapSource.Create(width, height, DPI , DPI, PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent, pixel, width * 4);
        }
    }
}
