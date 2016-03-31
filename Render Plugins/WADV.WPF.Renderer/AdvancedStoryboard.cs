using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace WADV.WPF.Renderer
{
    /// <summary>
    /// 高级故事板
    /// </summary>
    public class AdvancedStoryboard : Storyboard
    {
        /// <summary>
        /// 是否循环播放<br></br>
        /// 重置函数(ResetDelegate)或作用对象(Target)只要一个为空该属性便没有效果
        /// </summary>
        public bool Circle { get; set; } = false;

        /// <summary>
        /// 循环播放次数<br></br>
        /// 当这个属性为负值时将无限循环播放，默认值为1
        /// </summary>
        public int CircleCount { get; set; } = 1;

        /// <summary>
        /// 播放完成后是否执行重重函数<br></br>
        /// 重置函数(ResetDelegate)或作用对象(Target)只要一个为空该属性便没有效果
        /// </summary>
        public bool ResetWhenComplete { get; set; } = false;

        /// <summary>
        /// 获取或设置重置函数
        /// </summary>
        public Action<FrameworkElement> ResetDelegate { get; } = null;

        /// <summary>
        /// 获取或设置作用对象
        /// </summary>
        public FrameworkElement Target { get; } = null;

        /// <summary>
        /// 获取一个高级故事板
        /// </summary>
        public AdvancedStoryboard()
        {
            Completed += Storyboard_Complete;
        }

        /// <summary>
        /// 获取一个高级故事板
        /// </summary>
        /// <param name="target">作用对象</param>
        /// <param name="resetDelegate">重置函数</param>
        public AdvancedStoryboard(FrameworkElement target, Action<FrameworkElement> resetDelegate)
        {
            Completed += Storyboard_Complete;
            Target = target;
            ResetDelegate = resetDelegate;
        }

        private void Storyboard_Complete(object sender, EventArgs e)
        {
            if (Target == null || ResetDelegate == null) return;
            if (ResetWhenComplete || Circle) PluginTools.Window.Run(new Action(() => ResetDelegate.Invoke(Target)), true);
            if (!Circle || CircleCount == 0) return;
            PluginTools.Window.Run(new Action(() => Target.BeginStoryboard(this)), false);
            CircleCount--;
        }

        /// <summary>
        /// 从主窗口资源中获取AdvancedStoryboard<br></br>
        /// 这个资源的类型必须是Storyboard或AdvancedStoryboard
        /// </summary>
        /// <param name="key">资源的Key</param>
        /// <returns></returns>
        public static AdvancedStoryboard GetFromResource(string key)
        {
            var target = PluginTools.Window.GetResource(key);
            if (!(target is Storyboard)) return null;
            if (target is AdvancedStoryboard) return (AdvancedStoryboard)target;
            return ParseStoryboard((Storyboard)target);
        }

        /// <summary>
        /// 将一个Storyboard的全部属性值映射到新的AdvancedStoryboard上并返回这个AdvancedStoryboard<br></br>
        /// 这个函数仅映射属性，不映射事件和其他成员对象，也不映射私有成员
        /// </summary>
        /// <param name="target">属性来源Storyboard</param>
        /// <returns></returns>
        public static AdvancedStoryboard ParseStoryboard(Storyboard target)
        {
            var answer = new AdvancedStoryboard();
            var answerType = answer.GetType();
            foreach (var e in target.GetType().GetProperties())
            {
                answerType.GetProperty(e.Name).SetValue(answer, e.GetValue(e));
            }
            return answer;
        }
    }
}
