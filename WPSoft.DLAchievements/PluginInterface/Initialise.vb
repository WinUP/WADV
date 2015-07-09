Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Public Class Initialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            If Not My.Computer.FileSystem.FileExists(Path.Combine(WADV.Core.PathType.UserFile, "first_run")) Then Return True
            Properties.Register()
            API.Achieve.Add(New 成就制霸)
            API.Achieve.Add(New 设置狂人)
            API.Achieve.Add(New 我要干什么来着)
            API.Achieve.Add(New 系统已启动)
            Return True
        End Function
    End Class
End Namespace
