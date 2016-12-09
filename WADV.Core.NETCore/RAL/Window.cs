using System;
using System.IO;
using WADV.Core.NETCore.Configuration;
using WADV.Core.NETCore.System;
using WADV.Core.NETCore.Exception;
using WADV.Core.NETCore.Utility;

namespace WADV.Core.NETCore.RAL
{
    public abstract class Window
    {
        public bool CanFullscreen
        {
            get { return _canFullscreen; }
            set
            {
                if (value == _canFullscreen) return;
                _canFullscreen = value;
                CanFullscreenChanged?.Invoke(value);
                var message = value ? "[WINDOWS] ENABLE_FULLSCREEN" : "[WINDOWS] DISABLE_FULLSCREEN";
                MessageService.Instance().Send(new Message
                {
                    Content = message,
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void CanFullscreenChangedHandler(bool value);
        public event CanFullscreenChangedHandler CanFullscreenChanged;
        private bool _canFullscreen;

        public bool FullScreen
        {
            get { return _fullscreen; }
            set
            {
                if (!CanFullscreen) throw new FullscreenUnsupportException();
                if (value == _fullscreen) return;
                _fullscreen = value;
                FullscreenChanged?.Invoke(value);
                var message = value ? "[WINDOWS] FULLSCREEN_ENTER" : "[WINDOWS] FULLSCREEN_EXIT";
                MessageService.Instance().Send(new Message
                {
                    Content = message,
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void FullscreenChangedHandler(bool value);
        public event FullscreenChangedHandler FullscreenChanged;
        private bool _fullscreen;

        public Vector2 Resolution
        {
            get { return _resolution; }
            set
            {
                if (value == _resolution) return;
                _resolution = value;
                ResolutionChanged?.Invoke(value);
                MessageService.Instance().Send(new Message
                {
                    Content = "[WINDOW] RESOLUTION_CHANGE",
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void ResolutionChangedHandler(Vector2 value);
        public event ResolutionChangedHandler ResolutionChanged;
        private Vector2 _resolution;

        public bool CanClose
        {
            get { return _canClose; }
            set
            {
                if (value == _canClose) return;
                _canClose = value;
                CanCloseChanged?.Invoke(value);
                var message = value ? "[WINDOWS] ENABLE_CLOSE" : "[WINDOWS] DISABLE_CLOSE";
                MessageService.Instance().Send(new Message
                {
                    Content = message,
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void CanCloseChangedHandler(bool value);
        public event CanCloseChangedHandler CanCloseChanged;
        private bool _canClose;

        public void Close()
        {
            if (!CanClose) throw new CloseUnsupportException();
            var e = new CancelEventArgs();
            WindowClose?.Invoke(e);
            if (e.Cancel) return;
            MessageService.Instance().Send(new Message
            {
                Content = "[WINDOW] CLOSE",
                Mask = Variable.SystemMessageMask
            });
        }
        public delegate void WindowCloseHandler(CancelEventArgs e);
        public event WindowCloseHandler WindowClose;

        public void Open()
        {
            WindowOpen?.Invoke();
            MessageService.Instance().Send(new Message
            {
                Content = "[WINDOW] OPEN",
                Mask = Variable.SystemMessageMask
            });
        }
        public delegate void WindowOpenHandler();
        public event WindowOpenHandler WindowOpen;

        public string IconPath
        {
            get { return _iconPath; }
            set
            {
                if (value == _iconPath) return;
                _iconPath = Path.Combine(Location.Skin, value);
                IconChanged?.Invoke(_iconPath);
                MessageService.Instance().Send(new Message
                {
                    Content = "[WINDOW] ICON_CHANGE",
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void IconChangedHandler(string value);
        public event IconChangedHandler IconChanged;
        private string _iconPath;

        public string CursorPath
        {
            get { return _cursorPath; }
            set
            {
                if (value == _cursorPath) return;
                _cursorPath = Path.Combine(Location.Skin, value);
                CursorChanged?.Invoke(_cursorPath);
                MessageService.Instance().Send(new Message
                {
                    Content = "[WINDOW] CURSOR_CHANGE",
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void CursorChangedHandler(string value);
        public event CursorChangedHandler CursorChanged;
        private string _cursorPath;
        
        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                TitleChanged?.Invoke(_title);
                MessageService.Instance().Send(new Message
                {
                    Content = "[WINDOW] TITLE_CHANGE",
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void TitleChangedHandler(string value);
        public event TitleChangedHandler TitleChanged;
        private string _title;
        
        public bool CanResize
        {
            get { return _canResize; }
            set
            {
                if (value == _canResize) return;
                _canResize = value;
                CanResizeChanged?.Invoke(value);
                var message = value ? "[WINDOWS] ENABLE_RESIZE" : "[WINDOWS] DISABLE_RESIZE";
                MessageService.Instance().Send(new Message
                {
                    Content = message,
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void CanResizeChangedHandler(bool value);
        public event CanResizeChangedHandler CanResizeChanged;
        private bool _canResize;

        public bool Topmost
        {
            get { return _topmost; }
            set
            {
                if (value == _topmost) return;
                _topmost = value;
                TopmostChanged?.Invoke(value);
                var message = value ? "[WINDOWS] ENABLE_TOPMOST" : "[WINDOWS] DISABLE_TOPMOST";
                MessageService.Instance().Send(new Message
                {
                    Content = message,
                    Mask = Variable.SystemMessageMask
                });
            }
        }
        public delegate void TopmostChangedHandler(bool value);
        public event TopmostChangedHandler TopmostChanged;
        private bool _topmost;
        
        public void Navigate(Scene target, params object[] param)
        {
            var e = new NavigationParameter(SceneNow(), target)
            {
                Canceled = false,
                ExtraData = param
            };
            e = Receivers.NavigateReceivers.Map(e);
            if (e.Canceled) return;
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_NAVIGATION_STANDBY", 1);
            Go_Implement(e);
        }

        /// <summary>
        /// Go的实现，已处理NavigationParameter.Canceled<br></br>
        /// 转场完成后，你必须调用基类(WindowBase)的NavigateFinished函数告知系统转场已完成<br></br>
        /// 换句话说，你不调用这个函数也就意味着游戏系统不会知道转场完成
        /// </summary>
        /// <param name="e">转场参数</param>
        protected abstract void Go_Implement(NavigationParameter e);

        /// <summary>
        /// 获取正在展示的场景对象<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        /// <returns></returns>
        public abstract Scene SceneNow();

        /// <summary>
        /// 获取历史记录中出现过的场景对象<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        /// <param name="name">场景名称</param>
        /// <returns></returns>
        public abstract Scene GetScene(string name);

        /// <summary>
        /// 确定是否允许后退到上一个场景<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        /// <returns></returns>
        public abstract bool CanGoBack();

        /// <summary>
        /// 回到上一个场景<br></br>
        /// 属性：<br></br>
        ///  由渲染插件决定 | UI | WINDOW_GOBACK
        /// </summary>
        public void GoBack()
        {
            if (!CanGoBack())
                throw new Exception.IlleagleGoBackException();
            GoBack_Implement();
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_GOBACK", 1);
        }

        /// <summary>
        /// GoBack的实现<br></br>
        /// 已进行CanGoBack检查
        /// </summary>
        protected abstract void GoBack_Implement();

        /// <summary>
        /// 确定是否允许前进到下一个场景<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        /// <returns></returns>
        public abstract bool CanGoForward();

        /// <summary>
        /// 前进到下一个场景<br></br>
        /// 属性：<br></br>
        ///  由渲染插件决定 | UI | WINDOW_GOFORWARD
        /// </summary>
        public void GoForward()
        {
            if (!CanGoForward())
                throw new Exception.IlleagleGoForwardException();
            GoForward_Implemet();
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_GOFORWARD", 1);
        }

        /// <summary>
        /// GoForward的实现<br></br>
        /// 已进行CanGoForward检查
        /// </summary>
        protected abstract void GoForward_Implemet();

        /// <summary>
        /// 删除最近一个可后退场景的记录<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        public abstract void RemoveOneBackHistory();

        /// <summary>
        /// 清除所有可后退场景的记录<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        public abstract void ClearBackHistory();

        /// <summary>
        /// 从文件载入一个资源<br></br>
        /// 属性：<br></br>
        ///  由渲染插件决定 | UI | WINDOW_LOAD_RESOURCE
        /// </summary>
        /// <param name="name">资源的名称</param>
        /// <param name="path">资源文件路径(Resource目录下)</param>
        public void LoadResource(string name, string path)
        {
            LoadResource_Implement(name, PathFunction.CombineToString(PathType.Resource, path));
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_LOAD_RESOURCE", 1);
        }

        /// <summary>
        /// LoadResource的实现
        /// </summary>
        /// <param name="name">资源的名称</param>
        /// <param name="path">资源文件路径，路径已映射到Resource文件夹</param>
        protected abstract void LoadResource_Implement(string name, string path);

        /// <summary>
        /// 从对象载入一个资源<br></br>
        /// 属性：<br></br>
        ///  由渲染插件决定 | UI | WINDOW_LOAD_RESOURCE
        /// </summary>
        /// <param name="name">资源的名称</param>
        /// <param name="target">资源对象</param>
        public void LoadResource(string name, object target)
        {
            LoadResource_Implement(name, target);
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_LOAD_RESOURCE", 1);
        }

        /// <summary>
        /// LoadResource的实现
        /// </summary>
        /// <param name="name">资源的名称</param>
        /// <param name="target">资源对象</param>
        protected abstract void LoadResource_Implement(string name, object target);

        /// <summary>
        /// 删除一个资源<br></br>
        /// 属性：<br></br>
        ///  由渲染插件决定 | UI | WINDOW_REMOVE_RESOURCE
        /// </summary>
        /// <param name="name">资源的名称</param>
        /// <returns></returns>
        public object RemoveResource(string name)
        {
            dynamic target = RemoveResource_Implement(name);
            if (target != null)
                Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_REMOVE_RESOURCE", 1);
            return target;
        }

        public abstract object RemoveResource_Implement(string name);

        /// <summary>
        /// 获取指定名称的资源对象<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        /// <param name="name">资源的名称</param>
        /// <returns></returns>
        public abstract object GetResource(string name);

        /// <summary>
        /// 在窗口上执行一个委托<br></br>
        /// 属性：<br></br>
        ///  由sync参数决定 | UI
        /// </summary>
        /// <param name="target">要执行的委托</param>
        /// <param name="sync">是否同步执行</param>
        /// <param name="params">委托参数</param>
        /// <returns></returns>
        public abstract object Run(Delegate target, bool sync, params object[] @params);

        /// <summary>
        /// 在窗口上执行游戏循环的渲染委托<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        /// <param name="target">目标委托</param>
        protected internal abstract void RunRenderDelegate(Action target);

        /// <summary>
        /// 在窗口上执行自定义指令<br></br>
        /// 属性：<br></br>
        ///  由渲染插件决定 | UI
        /// </summary>
        /// <param name="order">指令</param>
        /// <param name="param">指令参数</param>
        public abstract void RunCustomizedOrder(string order, object param);

        /// <summary>
        /// 告知游戏系统窗口转场完成<br></br>
        /// 属性：<br></br>
        ///  同步 | UI
        /// </summary>
        protected void NavigateFinished()
        {
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_NAVITATION_FINISH", 1);
        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik
        //Facebook: facebook.com/telerik
        //=======================================================

    }
}
