Imports WADV.Core.Enumeration
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Public Class Initialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            If Not My.Computer.FileSystem.FileExists(Path.Combine(PathType.UserFile, "first_run")) Then Return True
            Properties.Register()
            Extension.Item.Add(New 成就制霸)
            Extension.Item.Add(New 设置狂人)
            Extension.Item.Add(New 我要干什么来着)
            Extension.Item.Add(New 系统已启动)
            Return True
        End Function
    End Class
End Namespace
