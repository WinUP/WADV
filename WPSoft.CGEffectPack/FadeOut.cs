using System;
using WADV.CGModule;

namespace WPSoft.CGEffectPack
{
    /// <summary>
    /// BGRA32图像的渐隐效果
    /// </summary>
    public class FadeOut : BaseBgra32
    {
        private readonly int _opacityPerFrame;

        public FadeOut(string filename, int duration) : base(filename, duration)
        {
            _opacityPerFrame = 255 / duration;
            if (_opacityPerFrame < 1) _opacityPerFrame = 1;
            unsafe {
                for (var i = 3; i < Length; i += 4) Pixel[i] = 255;
            }
        }

        public override void GetNextImageState(int frame)
        {
            unsafe {
                for (var i = 3; i < Length; i += 4)
                {
                    if (Pixel[i] - _opacityPerFrame > 0) Pixel[i] -= Convert.ToByte(_opacityPerFrame);
                    else Pixel[i] = 0;
                }
            }
            if (Pixel[3] == 0) Complete = true;
        }
    }
}
