''' <summary>
''' 脚本初始化接口
''' </summary>
''' <remarks></remarks>
Public Interface IScriptInitialise
    ''' <summary>
    ''' 初始化插件的脚本支持
    ''' </summary>
    ''' <param name="vm">脚本虚拟机</param>
    ''' <param name="env">脚本执行环境</param>
    ''' <remarks></remarks>
    Sub Register(vm As Neo.IronLua.Lua, env As Neo.IronLua.LuaGlobal)
End Interface
