Namespace Receiver

    ''' <summary>
    ''' 精灵循环接收器辅助类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class LoopReceiverHelper : Implements ISpriteLoopReceiver
        Private ReadOnly _onLogic As Func(Of Object, Object, Object)
        Private ReadOnly _onRender As Func(Of Object, Object)

        Friend Sub New(onLogic As Func(Of Object, Object, Object), onRener As Func(Of Object, Object))
            _onLogic = onLogic
            _onRender = onRener
        End Sub

        Public Function Logic(target As Windows.Controls.Panel, frame As Integer) As Boolean Implements ISpriteLoopReceiver.Logic
            Return DirectCast(_onLogic.Invoke(target, frame), Neo.IronLua.LuaResult).ToBoolean
        End Function

        Public Function Render(target As Windows.Controls.Panel) As Boolean Implements ISpriteLoopReceiver.Render
            Return DirectCast(_onRender.Invoke(target), Neo.IronLua.LuaResult).ToBoolean
        End Function
    End Class
End Namespace