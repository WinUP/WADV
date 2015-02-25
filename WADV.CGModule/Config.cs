using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WADV.CGModule
{
    internal static class Config
    {

        internal static int DPI { get; set; }

        /// <summary>
        /// 转换BGRA像素数组为图片
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="pixel">像素数组</param>
        /// <returns>转换完成后的图片</returns>
        internal static BitmapSource ConvertToImage(int width, int height, byte[] pixel)
        {
            return BitmapSource.Create(width, height, DPI , DPI, PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent, pixel, width * 4);
        }
    }
}
