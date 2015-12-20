using System.Windows.Controls;
using System.Windows.Markup;

namespace WADV.WPF.Renderer
{
    /// <summary>
    /// WPF场景<br></br>
    /// 专用于向NavigationWindow提供场景
    /// </summary>
    public class XamlScene : WADV.Core.Render.Scene
    {
        private readonly Page _page;

        /// <summary>
        /// 获得一个WPF场景<br></br>
        /// 包含场景的XAML文件必须是一个Page
        /// </summary>
        /// <param name="name">场景名称</param>
        /// <param name="path">包含场景的XAML文件的路径(Resource目录下)</param>
        public XamlScene(string name, string path) : base(name)
        {
            _page = (Page) XamlReader.Parse(Core.API.Path.Combine(Core.PathType.Resource, path));
        }

        /// <summary>
        /// 获得一个WPF场景
        /// </summary>
        /// <param name="name">场景名称</param>
        /// <param name="page">场景对象</param>
        public XamlScene(string name, Page page) : base(name)
        {
            _page = page;
        }

        /// <summary>
        /// 获取场景对应的Page<br></br>
        /// 注意：由于WPF的限制，自行操作这个Page时切记在Window线程中操作（即使用WADV.Core.API.Window.Run/RunAsync方法）。
        /// </summary>
        /// <returns></returns>
        public override object Content()
        {
            return _page;
        }
    }
}
