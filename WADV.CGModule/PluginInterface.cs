using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WADV.AppCore.API;
using WADV.AppCore.PluginInterface;
using WADV.CGModule.API;
using WADV.CGModule.Effect;

namespace WADV.CGModule.PluginInterface
{
    public sealed class Initialiser : IInitialise
    {

        public bool Initialising() {
            ScriptAPI.RegisterInTableSync("api_cg", "init", new Action<int>(ConfigAPI.Init), true);
            ScriptAPI.RegisterInTableSync("api_cg", "show", new Func<string, string, int, string, bool>(ImageAPI.Show));
            ScriptAPI.RegisterInTableSync("api_cg", "clean", new Func<string, bool>(ImageAPI.Clean));
            MessageAPI.SendSync("[CG]INIT_LOAD_FINISH");
            return true;
        }

    }

    public sealed class LoopReceiver : ILoopReceiver
    {
        private readonly IEffect _effect;
        private ImageBrush _brush;
        private WriteableBitmap _image;
        private Int32Rect _imageRect;
        private readonly int _width;
        private readonly int _height;

        public LoopReceiver(IEffect effect, Panel content)
        {
            _effect = effect;
            _effect.GetNextImageState(LoopAPI.CurrentFrame());
            _width = effect.GetWidth();
            _height = effect.GetHeight();
            WindowAPI.GetDispatcher().Invoke((Action)(() =>
            {
                _image = new WriteableBitmap(Config.ConvertToImage(_width, _height, effect.GetPixel()));
                _imageRect = new Int32Rect(0, 0, _width, _height);
                _brush = new ImageBrush {ImageSource = _image, Stretch = Stretch.Uniform};
                content.Background = _brush;
            }));
        }

        public bool Logic(int frame)
        {
            _effect.GetNextImageState(frame);
            return !_effect.IsFinished();
        }

        public void Render()
        {
            _image.WritePixels(_imageRect, _effect.GetPixel(), _width * 4, 0);
        }
    } 
}
