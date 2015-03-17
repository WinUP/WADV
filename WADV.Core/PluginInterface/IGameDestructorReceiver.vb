Namespace PluginInterface

    ''' <summary>
    ''' 游戏解构接口
    ''' </summary>
    Public Interface IGameDestructorReceiver

        ''' <summary>
        ''' 开始解构游戏
        ''' <param name="e">可取消事件的数据</param>
        ''' </summary>
        Sub DestructuringGame(e As ComponentModel.CancelEventArgs)

    End Interface

End Namespace