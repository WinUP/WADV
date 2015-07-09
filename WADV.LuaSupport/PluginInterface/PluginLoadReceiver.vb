Namespace PluginInterface
    Friend NotInheritable Class PluginLoadReceiver : Implements Core.PluginInterface.IPluginLoadReceiver
        Public Sub BeforeLoad(types() As Type) Implements Core.PluginInterface.IPluginLoadReceiver.BeforeLoad
            Dim instance = ScriptCore.GetInstance
            For Each target In types.Where(Function(e) e.GetInterface("WADV.LuaSupport.IScriptInitialise") <> Nothing)
                DirectCast(Activator.CreateInstance(target), IScriptInitialise).Register(instance.Vm, instance.Environment)
            Next
        End Sub
    End Class
End Namespace
