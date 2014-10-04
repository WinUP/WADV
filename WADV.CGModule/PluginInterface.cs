using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WADV.AppCore.API;
using WADV.AppCore.Plugin;
using WADV.CGModule.Effect;

namespace WADV.CGModule.PluginInterface
{
    public class Initialise : IInitialise
    {

        public bool StartInitialising()
        {
            Config.ReadConfigFile();
            Initialiser.LoadEffect();
            return true;
        }

    }

    public class Script : IScript
    {

        public void StartRegisting(LuaInterface.Lua ScriptVM)
        {
            ScriptAPI.RegisterFunction(System.Reflection.Assembly.GetExecutingAssembly().GetTypes(), "WADV.CGModule.API", "IM");
        }
    }

    public class ImageLoop : ILooping
    {
        private IEffect effect;
        private Panel content;
        private ImageBrush brush;
        private WriteableBitmap image;
        private Int32Rect imageRect;
        private int width;
        private int height;

        public ImageLoop(IEffect effect, Panel content)
        {
            this.effect = effect;
            this.content = content;
            this.effect.GetNextImageState(LoopingAPI.CurrentFrame());
            width = effect.GetWidth();
            height = effect.GetHeight();
            WindowAPI.GetDispatcher().Invoke((Action)(() =>
            {
                image = new WriteableBitmap(Config.ConvertToImage(width, height, effect.GetPixel()));
                imageRect = new Int32Rect(0, 0, width, height);
                brush = new ImageBrush();
                brush.ImageSource = image;
                content.Background = brush;
            }));
        }

        public bool StartLooping(int frame)
        {
            effect.GetNextImageState(frame);
            if (effect.IsFinished()) return false;
            return true;
        }

        public void StartRendering(Window window)
        {
            image.WritePixels(imageRect, effect.GetPixel(), width * 4, 0);
        }
    } 
}
