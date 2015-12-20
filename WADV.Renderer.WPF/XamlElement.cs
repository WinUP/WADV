using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using WADV.Core.Component;
using WADV.Core.Render;

namespace WADV.WPF.Renderer
{
    /// <summary>
    /// WPF渲染组件。<br></br>
    /// 这个组件会给予挂载的精灵一个Canvas，并在精灵层次发生变化或精灵离开场景时自动更新或销毁Canvas。<br></br>
    /// 这个组件仅允许挂载一个精灵。对一个以上的精灵的挂载会被拒绝。
    /// </summary>
    public class XamlElement:Component
    {
        /// <summary>
        /// 获取这个组件对应的FrameworkElement<br></br>
        /// 注意：由于WPF的限制，自行操作这个FrameworkElement时切记在Window线程中操作（即使用WADV.Core.Window().Run方法）。
        /// </summary>
        public FrameworkElement Element { get; private set; }

        /// <summary>
        /// 获得新的XamlElement<br></br>
        /// 相当于new XamlElement("Canvas")
        /// </summary>
        public XamlElement()
        {
            Element = null;
        }

        /// <summary>
        /// 将置顶元素封装为XamlElement
        /// </summary>
        /// <param name="element">要使用的元素</param>
        public XamlElement(FrameworkElement element)
        {
            Element = element;
        }

        /// <summary>
        /// 根据元素名称生成XamlElement
        /// </summary>
        /// <param name="elementTypeName">元素名称，必须在System.Windows.Controls名称空间下</param>
        public XamlElement(string elementTypeName)
        {
            Element = (FrameworkElement)Activator.CreateInstance(Type.GetType("System.Windows.Controls." + elementTypeName, true, false));
        }
        
        protected override bool BeforeBinding(Sprite sourceElement)
        {
            if (PluginTools.Window == null) throw new Exception.NullWindowException();
            if (BindedElements.Count > 0) return false; //当存在已挂载的精灵时拒绝新的挂载
            if(Element == null) //如果XamlElement没有目标元素则生成Canvas
            {
                Element = PluginTools.NewCanvas();
                if (Element == null) return false; //申请Canvas失败则拒绝挂载
                PluginTools.Window.Run(new Action(() => //设定Canvas基础参数
                {
                    Element.Name = sourceElement.Name;
                    Element.Width = 0;
                    Element.Height = 0;
                    Element.Margin = new Thickness(0, 0, 0, 0);
                }), true);
            }
            var parent = GetParentElement(sourceElement);
            if (parent == null) //找不到父元素就将场景根元素作为父元素
                PluginTools.Window.Run(new Action(() => ((Panel) sourceElement.Scene.Content()).Children.Add(Element)),
                    true);
            else //找到的话就挂载到父元素下
            {
                if (!(parent.Element is Panel)) throw new Exception.UnsupportedParentException();
                PluginTools.Window.Run(new Action(() => ((Panel)parent.Element).Children.Add(Element)), true);
            }
            sourceElement.UnbindFromScene += Sprite_UnbindFromScene;
            sourceElement.BeforeChangeParent += Sprite_BeforeChangeParent;
            sourceElement.AfterChangeParent += Sprite_AfterChangeParent;
            return true;
        }

        protected override bool BeforeUnbinding(Sprite sourceElement, bool isFromClear = false)
        {
            PluginTools.Window.Run(new Action(() => ((Panel) Element.Parent)?.Children.Remove(Element)), true);
            var sprite = BindedElements[0];
            sprite.UnbindFromScene -= Sprite_UnbindFromScene;
            sprite.BeforeChangeParent -= Sprite_BeforeChangeParent;
            sprite.AfterChangeParent -= Sprite_AfterChangeParent;
            Element = null;
            return true;
        }

        private void Sprite_UnbindFromScene(Sprite sprite, Scene target)
        {
            PluginTools.Window.Run(new Action(() => ((Panel)target.Content()).Children.Remove(Element)), true);
        }

        private void Sprite_BeforeChangeParent(Sprite sprite, Sprite parentWanted)
        {
            if (parentWanted == null || parentWanted != sprite.Parent)
                PluginTools.Window.Run(new Action(() => ((Panel) Element.Parent).Children.Remove(Element)), true);
        }

        private void Sprite_AfterChangeParent(Sprite sprite, ReadOnlyCollection<Sprite> parentList, ref bool transmiteToChildren)
        {
            if (sprite.Parent != null) return;
            var parent = GetParentElement(parentList.Last());
            PluginTools.Window.Run(new Action(() =>
            {
                if (parent == null) ((Panel) sprite.Scene.Content()).Children.Add(Element);
                else
                {
                    if (!(parent.Element is Panel)) throw new Exception.UnsupportedParentException();
                    ((Panel)parent.Element).Children.Add(Element);
                }
            }), true);
        }

        /// <summary>
        /// 循环查找上级精灵中最近的挂载有XamlElement组件的精灵
        /// </summary>
        /// <param name="root">查找起点</param>
        /// <returns></returns>
        private static XamlElement GetParentElement(Sprite root)
        {
            var parent = root.Parent;
            while (parent != null)
            {
                var answer = parent.Components.Get<XamlElement>();
                if (answer != null) return answer;
                parent = parent.Parent;
            }
            return null;
        }
    }
}
