using System;
using WADV.CGModule.Effect;

namespace WPSoft.CGEffectPack
{
    /// <summary>
    /// BGRA32图像的渐显效果
    /// </summary>
    public class FadeIn : BaseBGRA32
    {
        private int opacityPerFrame;

        public FadeIn(string filename, int duration) : base(filename, duration)
        {
            opacityPerFrame = 255 / duration;
            if (opacityPerFrame < 1) opacityPerFrame = 1;
            unsafe
            {
                for (int i = 3; i < length; i += 4) pixel[i] = 0;
            }
        }

        public override void GetNextImageState(int frame)
        {
            unsafe
            {
                for (int i = 3; i < length; i += 4)
                {
                    if (pixel[i] + opacityPerFrame < 256) pixel[i] += Convert.ToByte(opacityPerFrame);
                    else pixel[i] = 255;
                }
            }
            if (pixel[3] == 255) complete = true;
        }
    }
}
