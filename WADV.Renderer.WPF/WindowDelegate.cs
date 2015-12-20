using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using WADV.Core;
using WADV.Core.Render;

namespace WADV.WPF.Renderer
{
    /// <summary>
    /// WPF窗口托管类<br></br>
    /// 这个类可替代NavigationWindow作为WindowBase的实现供游戏系统调用
    /// </summary>
    public class WindowDelegate : WindowBase
    {
        private readonly NavigationWindow _window;
        private readonly Dispatcher _dispatcher;
        private readonly List<Scene> _scenes;
        private readonly Dictionary<string, ResourceDictionary> _resources;
        private int _sceneIndex;
        private bool _windowTopmost;
        private WindowState _windowState;
        private WindowStyle _windowStyle;
        private ResizeMode _windowResizemode;
        private Rect _windowRect;

        public WindowDelegate(NavigationWindow window)
        {
            _window = window;
            _dispatcher = window.Dispatcher;
            _scenes = new List<Scene>();
            _resources = new Dictionary<string, ResourceDictionary>();
            _sceneIndex = -1;
            _windowRect = new Rect();
            _dispatcher.Invoke(() =>
            {
                _windowTopmost = window.Topmost;
                _windowState = window.WindowState;
                _windowStyle = window.WindowStyle;
                _windowResizemode = window.ResizeMode;
                _windowRect.X = window.Left;
                _windowRect.Y = window.Top;
                _windowRect.Width = window.Width;
                _windowRect.Height = window.Height;
                window.LoadCompleted += (sender, e) => NavigateFinished();
            });
        }

        /// <summary>
        /// 获取这个对象托管的NavigationWindow
        /// </summary>
        /// <returns></returns>
        public NavigationWindow Window()
        {
            return _window;
        }

        private void SetFullScreen()
        {
            _windowState = _window.WindowState;
            _windowStyle = _window.WindowStyle;
            _windowResizemode = _window.ResizeMode;
            _windowRect.X = _window.Left;
            _windowRect.Y = _window.Top;
            _windowRect.Width = _window.Width;
            _windowRect.Height = _window.Height;
            _window.WindowState = WindowState.Normal;
            _window.WindowStyle = WindowStyle.None;
            _window.ResizeMode = ResizeMode.NoResize;
            _window.Topmost = true;
            _window.Left = 0;
            _window.Top = 0;
            _window.Width = SystemParameters.PrimaryScreenWidth;
            _window.Height = SystemParameters.PrimaryScreenHeight;
            _window.Activated += Window_Active;
            _window.Deactivated += Window_Deactive;
        }

        private void CancelFullScreen()
        {
            _window.Activated -= Window_Active;
            _window.Deactivated -= Window_Deactive;
            _window.Left = _windowRect.X;
            _window.Top = _windowRect.Y;
            _window.Width = _windowRect.Width;
            _window.Height = _windowRect.Height;
            _window.WindowState = _windowState;
            _window.WindowStyle = _windowStyle;
            _window.ResizeMode = _windowResizemode;
            _window.Topmost = _windowTopmost;
        }

        private static void Window_Active(object sender, EventArgs e)
        {
            ((NavigationWindow)sender).Topmost = true;
        }

        private static void Window_Deactive(object sender, EventArgs e)
        {
            ((NavigationWindow)sender).Topmost = false;
        }

        private static void Window_Close(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 获取主窗口的截图
        /// </summary>
        /// <param name="quality">图像质量[0-100]</param>
        /// <returns>截图的编码器</returns>
        public JpegBitmapEncoder Screenshot(int quality)
        {
            var panel = (Panel)SceneNow().Content();
            var targetImage = new RenderTargetBitmap((int) panel.ActualWidth, (int) panel.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            targetImage.Render(_window);
            var encoder = new JpegBitmapEncoder {QualityLevel = quality};
            encoder.Frames.Add(BitmapFrame.Create(targetImage));
            return encoder;
        }

        /// <summary>
        /// 将主窗口的截图保存到文件中<br></br>
        /// 完成后会发送消息 [RENDER]WINDOW_IMAGE_SAVE
        /// </summary>
        /// <param name="path">要保存的路径(UserFile目录下)</param>
        public void SaveScreenshot(string path)
        {
            var image = Screenshot(80);
            var stream = new FileStream(Core.API.Path.Combine(PathType.UserFile, path), FileMode.Create);
            image.Save(stream);
            stream.Close();
            Core.API.Message.Send("[RENDER]WINDOW_IMAGE_SAVE");
        }

        protected override void SetCanFullscreen_Implement(bool value)
        {
            //似乎什么也不用做
        }

        protected override void SetFullscreen_Implement(bool value)
        {
            if (value)
                _dispatcher.Invoke(SetFullScreen);
            else
                _dispatcher.Invoke(CancelFullScreen);
        }

        protected override void SetResolution_Implement(Vector2 value)
        {
            _dispatcher.Invoke(() =>
            {
                _window.Width = value.X;
                _window.Height = value.Y;
            });
        }

        protected override void SetCanClose_Implement(bool value)
        {
            if (value)
                _dispatcher.Invoke(() => _window.Closing -= Window_Close);
            else
                _dispatcher.Invoke(() => _window.Closing += Window_Close);
        }

        protected override void Close_Implement()
        {
            _dispatcher.Invoke(() => _window.Close());
        }

        public override void Show_Implement()
        {
            _dispatcher.Invoke(() => _window.Show());
        }

        protected override void SetIconFromFile_Implement(string value)
        {
            _dispatcher.Invoke(() => _window.Icon = BitmapFrame.Create(new Uri(value, UriKind.Absolute)));
        }

        protected override void SetCursorFromFile_Implement(string value)
        {
            _dispatcher.Invoke(() => _window.Cursor  = new Cursor(value));
        }

        protected override void SetTitle_Implement(string value)
        {
            _dispatcher.Invoke(() => _window.Title = value);
        }

        protected override void SetCanResize_Implement(bool value)
        {
            var mode = value ? ResizeMode.CanResize : ResizeMode.NoResize;
            if (_windowResizemode == mode) return;
            _dispatcher.Invoke(() => _window.ResizeMode = mode);
        }

        protected override void SetTopmost_Implement(bool value)
        {
            _dispatcher.Invoke(() => _window.Topmost = value);
        }

        //转场检测的执行顺序为：前一个场景的退出函数->后一个场景的进入函数->转场广播
        //转场指示发出后将等待[SYSTEM]WINDOW_PAGE_CHANGE，最后触发新场景的进入完成函数
        protected override void Go_Implement(NavigationParameter e)
        {
            if (!e.PreviousScene.Scene_Exit(e.NextScene , e.ExtraData)) return;
            if (!e.NextScene.Scene_Enter(e.PreviousScene, e.ExtraData)) return;
            if (_sceneIndex != _scenes.Count - 1)
                _scenes.RemoveRange(_sceneIndex + 1, _scenes.Count - _sceneIndex + 1);
            _scenes.Add(e.NextScene);
            _sceneIndex = _scenes.Count - 1;
            _dispatcher.Invoke(() => _window.Navigate(e.NextScene.Content(), e.ExtraData));
            Core.API.Message.Wait("[SYSTEM]WINDOW_PAGE_CHANGE");
            e.NextScene.Scene_Entered(e.ExtraData);
        }

        public override Scene SceneNow()
        {
            return _scenes[_sceneIndex];
        }

        public override Scene GetScene(string name)
        {
            return _scenes.FirstOrDefault(e => e.Name == name);
        }

        public override bool CanGoBack()
        {
            return _dispatcher.Invoke(() => _window.NavigationService.CanGoBack);
        }

        protected override void GoBack_Implement()
        {
            _dispatcher.Invoke(() =>
            {
                if (!_window.NavigationService.CanGoBack) return;
                _window.NavigationService.GoBack();
                _sceneIndex -= 1;
            });
        }

        public override bool CanGoForward()
        {
            return _dispatcher.Invoke(() => _window.NavigationService.CanGoForward);
        }

        protected override void GoForward_Implemet()
        {
            _dispatcher.Invoke(() =>
            {
                if (!_window.NavigationService.CanGoForward) return;
                _window.NavigationService.GoForward();
                _sceneIndex += 1;
            });
        }

        public override void RemoveOneBackHistory()
        {
            _dispatcher.Invoke(() =>
            {
                if (_window.NavigationService.CanGoBack)
                    _window.NavigationService.RemoveBackEntry();
            });
        }

        public override void ClearBackHistory()
        {
            _dispatcher.Invoke(() =>
            {
                while (_window.NavigationService.CanGoBack)
                    _window.NavigationService.RemoveBackEntry();
            });
            if (_scenes.Count < 2) return;
            var scene = _scenes.Last();
            _scenes.Clear();
            _scenes.Add(scene);
            _sceneIndex = 0;
        }

        //异常：UnsupportDictionaryFormatException
        protected override void LoadResource_Implement(string name, string path)
        {
            if (Path.GetExtension(path) != "xaml") throw new Exception.UnsupportDictionaryFormatException();
            var resource = new ResourceDictionary { Source = new Uri(path, UriKind.Absolute) };
            _dispatcher.Invoke(() => _window.Resources.MergedDictionaries.Add(resource));
            _resources.Add(name, resource);
        }

        //异常：UnsupportDictionaryFormatException
        protected override void LoadResource_Implement(string name, object target)
        {
            if (!(target is ResourceDictionary)) throw new Exception.UnsupportDictionaryFormatException();
            _dispatcher.Invoke(() => _window.Resources.MergedDictionaries.Add((ResourceDictionary)target));
            _resources.Add(name, (ResourceDictionary)target);
        }

        public override object RemoveResource_Implement(string name)
        {
            if (!_resources.ContainsKey(name)) return null;
            var target = _resources[name];
            _resources.Remove(name);
            _dispatcher.Invoke(() => _window.Resources.MergedDictionaries.Remove(target));
            return target;
        }

        public override object GetResource(string name)
        {
            return _resources.ContainsKey(name) ? _resources[name] : null;
        }

        public override object Run(Delegate target, bool sync, params object[] @params)
        {
            return sync ? _dispatcher.Invoke(target, @params) : _dispatcher.BeginInvoke(target, @params);
        }

        public override void RunCustomizedOrder(string order, object param)
        {
            //没什么可做的
        }

        protected override void RunRenderDelegate(Action target)
        {
            _dispatcher.Invoke(target);
        }
    }
}