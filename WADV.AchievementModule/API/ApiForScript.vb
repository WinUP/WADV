Imports Neo.IronLua

Namespace API
    Friend NotInheritable Class ApiForScript
        Friend Shared Function newAchievement(name As String, substance As String, onCheck As Func(Of LuaResult), onRegister As Func(Of LuaResult)) As Achievement
            Return New AchievementForScript(name, substance, onCheck, onRegister)
        End Function
    End Class

    ''' <summary>
    ''' 提供从脚本创建成就的服务
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class AchievementForScript : Inherits Achievement
        Private ReadOnly _onCheck As Func(Of LuaResult)
        Private ReadOnly _onRegister As Func(Of LuaResult)

        Friend Sub New(name As String, substance As String, onCheck As Func(Of LuaResult), onRegister As Func(Of LuaResult))
            MyBase.New(name, substance)
            _onCheck = onCheck
            _onRegister = onRegister
        End Sub
        Public Overrides Sub Check()
            _onCheck.Invoke()
        End Sub

        Protected Friend Overrides Sub Register()
            _onRegister.Invoke()
        End Sub
    End Class
End Namespace
