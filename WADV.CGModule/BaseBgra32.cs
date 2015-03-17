using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WADV.Core;
using WADV.Core.API;

namespace WADV.CGModule
{
    /// <summary>
    /// 提供图像效果的基本参数
    /// </summary>
    public abstract class ImageBase
    {
        protected int Duration;
        protected int Width;
        protected int Height;
        protected byte[] Pixel;
        protected int Length;
        protected bool Complete;
    }

    /// <summary>
    /// 提供BGRA32图像的基本参数和“立即显示”效果
    /// </summary>
    public class BaseBgra32 : ImageBase, IEffect
    {
        public virtual void GetNextImageState(int frame) { Complete = true; }
        public bool IsFinished() { return Complete; }
        public int GetWidth() { return Width; }
        public int GetHeight() { return Height; }
        public byte[] GetPixel() { return Pixel; }

        /// <summary>
        /// 获得一个BGRA32图像的图像效果
        /// </summary>
        /// <param name="filename">图像文件路径(Resource目录下)</param>
        /// <param name="duration">动画时长(单位为帧)</param>
        /// <remarks></remarks>
        public BaseBgra32(string filename, int duration)
        {
            var bitmapContent = new FormatConvertedBitmap();
            bitmapContent.BeginInit();
            bitmapContent.DestinationPalette = BitmapPalettes.WebPaletteTransparent;
            bitmapContent.DestinationFormat = PixelFormats.Bgra32;
            bitmapContent.Source = new BitmapImage(new Uri(PathAPI.GetPath(PathType.Resource, filename)));
            bitmapContent.EndInit();
            Width = bitmapContent.PixelWidth;
            Height = bitmapContent.PixelHeight;
            Pixel = new byte[Width * Height * 4];
            bitmapContent.CopyPixels(Pixel, Width * 4, 0);
            Length = Pixel.Length;
            this.Duration = duration;
        }
    }
}
