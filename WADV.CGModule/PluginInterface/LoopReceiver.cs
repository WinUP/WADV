using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WADV.AppCore.API;
using WADV.AppCore.PluginInterface;

namespace WADV.CGModule.PluginInterface
{
    internal sealed class LoopReceiver : ILoopReceiver
    {
        private readonly IEffect _effect;
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
            WindowAPI.InvokeSync(() =>
            {
                _image = new WriteableBitmap(Config.ConvertToImage(_width, _height, effect.GetPixel()));
                _imageRect = new Int32Rect(0, 0, _width, _height);
                content.Background = new ImageBrush { ImageSource = _image, Stretch = Stretch.Uniform };
            });
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
