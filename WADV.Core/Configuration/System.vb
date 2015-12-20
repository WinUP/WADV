Imports WADV.Core.GameSystem

Namespace Configuration
    ''' <summary>
    ''' 游戏系统数据存储模块
    ''' </summary>
    Friend Class System
        ''' <summary>
        ''' 获取或设置游戏主窗口
        ''' </summary>
        Friend Shared BaseWindow As Render.WindowBase

        ''' <summary>
        ''' 渲染插件提供的用于渲染精灵的组件的名称
        ''' </summary>
        Friend Shared RenderComponentNameForSprite As String

        ''' <summary>
        ''' 获取或设置脚本核心实例
        ''' </summary>
        Friend Shared ScriptEngine As Script.IScriptEngine

        ''' <summary>
        ''' 获取或设置消息循环实例
        ''' </summary>
        Friend Shared MessageService As MessageService

        ''' <summary>
        ''' 获取或设置计时器实例
        ''' </summary>
        Friend Shared MainTimer As MainTimer

        ''' <summary>
        ''' 获取或设置游戏循环实例
        ''' </summary>
        Friend Shared MainLoop As MainLoop
    End Class
End Namespace