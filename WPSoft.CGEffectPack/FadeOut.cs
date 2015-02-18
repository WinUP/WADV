using System;
using WADV.CGModule.Effect;

namespace WPSoft.CGEffectPack
{
    /// <summary>
    /// BGRA32图像的渐隐效果
    /// </summary>
    public class FadeOut : BaseBgra32
    {
        private int opacityPerFrame;

        public FadeOut(string filename, int duration) : base(filename, duration)
        {
            opacityPerFrame = 255 / duration;
            if (opacityPerFrame < 1) opacityPerFrame = 1;
            unsafe {
                for (int i = 3; i < length; i += 4) pixel[i] = 255;
            }
        }

        public override void GetNextImageState(int frame)
        {
            unsafe {
                for (int i = 3; i < length; i += 4)
                {
                    if (pixel[i] - opacityPerFrame > 0) pixel[i] -= Convert.ToByte(opacityPerFrame);
                    else pixel[i] = 0;
                }
            }
            if (pixel[3] == 0) complete = true;
        }
    }
}
