Namespace AppCore.Plugin

    ''' <summary>
    ''' 逻辑循环接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ILoop

        ''' <summary>
        ''' 执行一次循环
        ''' </summary>
        Sub StartLooping()

    End Interface

    ''' <summary>
    ''' 初始化接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IInitialise

        ''' <summary>
        ''' 开始初始化
        ''' </summary>
        Function StartInitialising() As Boolean

    End Interface

    ''' <summary>
    ''' 窗体渲染接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IRender

        ''' <summary>
        ''' 渲染一次窗体
        ''' </summary>
        ''' <param name="renderingWindow">要渲染的窗体</param>
        ''' <param name="dc">绘图上下文</param>
        Sub StartRendering(renderingWindow As Window, dc As DrawingContext)

    End Interface

    ''' <summary>
    ''' 小型逻辑循环接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ICustomizedLoop

        ''' <summary>
        ''' 执行一次循环
        ''' </summary>
        ''' <returns>循环是否没有结束</returns>
        Function StartLooping() As Boolean

    End Interface

    ''' <summary>
    ''' 脚本函数注册接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IScriptFunction

        ''' <summary>
        ''' 注册脚本函数
        ''' </summary>
        ''' <param name="ScriptVM">脚本执行主机</param>
        Sub StartRegisting(ScriptVM As LuaInterface.Lua)

    End Interface

End Namespace
