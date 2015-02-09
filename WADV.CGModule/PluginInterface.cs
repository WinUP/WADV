using System;
using System.Linq;
using System.Reflection;
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

        public bool Initialising() {
            Initialiser.LoadEffect();
            ScriptAPI.RunStringSync("api_cg={}");
            foreach (var tmpApiClass in (from tmpClass in Assembly.GetExecutingAssembly().GetTypes() where tmpClass.Namespace == "WADV.CGModule.API" && tmpClass.IsClass && tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) == tmpClass.Name.Length - 3 select tmpClass))
            {
                var registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower();
                ScriptAPI.RunStringSync("api_cg." + registerName + "={}");
                ScriptAPI.RegisterSync(tmpApiClass, "api_cg." + registerName);
            }
            MessageAPI.SendSync("CG_INIT_FINISH");
            return true;
        }

    }

    public class ImageLoop : ILoopReceiver
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
                brush.Stretch = Stretch.Uniform;
                content.Background = brush;
            }));
        }

        public bool Logic(int frame)
        {
            effect.GetNextImageState(frame);
            if (effect.IsFinished()) return false;
            return true;
        }

        public void Render(Window window)
        {
            image.WritePixels(imageRect, effect.GetPixel(), width * 4, 0);
        }
    } 
}
