using System;
using WADV.CGModule;

namespace WPSoft.CGEffectPack
{
    /// <summary>
    /// BGRA32图像的渐显效果
    /// </summary>
    public class FadeIn : BaseBgra32
    {
        private readonly int _opacityPerFrame;

        public FadeIn(string filename, int duration) : base(filename, duration)
        {
            _opacityPerFrame = 255 / duration;
            if (_opacityPerFrame < 1) _opacityPerFrame = 1;
            unsafe {
                for (var i = 3; i < Length; i += 4) Pixel[i] = 0;
            }
        }

        public override void GetNextImageState(int frame)
        {
            unsafe {
                for (var i = 3; i < Length; i += 4)
                {
                    if (Pixel[i] + _opacityPerFrame < 256) Pixel[i] += Convert.ToByte(_opacityPerFrame);
                    else Pixel[i] = 255;
                }
            }
            if (Pixel[3] == 255) Complete = true;
        }
    }
}
