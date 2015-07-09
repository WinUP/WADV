Imports Neo.IronLua

Namespace API
    Friend NotInheritable Class ApiForScript
        Friend Shared Function Show(choice As LuaTable, showEffectName As String, hideEffectName As String, waitFrame As Integer, Optional waitEffectName As String = "BaseProgress") As String
            Dim choiceList() As String = (From tmpChoice In choice.ArrayList Select CStr(tmpChoice)).ToArray
            Return API.Show(choiceList, showEffectName, hideEffectName, waitFrame, waitEffectName)
        End Function
    End Class
End Namespace
