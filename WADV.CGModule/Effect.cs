using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WADV.AppCore.API;

namespace WADV.CGModule.Effect
{
    /// <summary>
    /// 包括图像效果需要实现的功能
    /// </summary>
    public interface IEffect
    {
        void GetNextImageState(int frame);
        bool IsFinished();
        int GetWidth();
        int GetHeight();
        byte[] GetPixel();
    }

    /// <summary>
    /// 提供预加载所有图像效果的功能
    /// </summary>
    public class Initialiser
    {
        /// <summary>
        /// 待实例化的图像效果列表
        /// </summary>
        protected internal static Dictionary<string, Type> EffectList;

        /// <summary>
        /// 读取并缓存所有图像效果
        /// </summary>
        protected internal static void LoadEffect()
        {
            EffectList = new Dictionary<string, Type>();
            EffectList.Add("BaseEffect", typeof(BaseBGRA32));
            string basePath = PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, "CGEffect\\");
            foreach(string file in System.IO.Directory.GetFiles(basePath,"*.dll"))
            {
                var assembly = System.Reflection.Assembly.LoadFrom(file).GetTypes();
                foreach(Type type in assembly) if(type.GetInterface("IEffect")!=null) EffectList.Add(type.Name, type);
            }
        }
    }

    /// <summary>
    /// 提供图像效果的基本参数
    /// </summary>
    public abstract class ImageBase
    {
        protected int duration;
        protected int width;
        protected int height;
        protected byte[] pixel;
        protected int length;
        protected bool complete = false;
    }

    /// <summary>
    /// 提供BGRA32图像的基本参数和“立即显示”效果
    /// </summary>
    public class BaseBGRA32 : ImageBase, IEffect
    {
        public virtual void GetNextImageState(int frame) { complete = true; }
        public bool IsFinished() { return complete; }
        public int GetWidth() { return width; }
        public int GetHeight() { return height; }
        public byte[] GetPixel() { return pixel; }

        /// <summary>
        /// 获得一个BGRA32图像的图像效果
        /// </summary>
        /// <param name="filename">图像文件路径(Resource目录下)</param>
        /// <param name="duration">动画时长(单位为帧)</param>
        /// <remarks></remarks>
        public BaseBGRA32(string filename, int duration)
        {
            FormatConvertedBitmap bitmapContent = new FormatConvertedBitmap();
            bitmapContent.BeginInit();
            bitmapContent.DestinationPalette = BitmapPalettes.WebPaletteTransparent;
            bitmapContent.DestinationFormat = PixelFormats.Bgra32;
            bitmapContent.Source = new BitmapImage(new Uri(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, filename)));
            bitmapContent.EndInit();
            width = bitmapContent.PixelWidth;
            height = bitmapContent.PixelHeight;
            pixel = new byte[width * height * 4];
            bitmapContent.CopyPixels(pixel, width * 4, 0);
            length = pixel.Length;
            this.duration = duration;
            MessageAPI.SendSync("CG_BASEEFFECT_DECLARE");
        }
    }
}
