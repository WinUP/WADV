Namespace PluginInterface
    ''' <summary>
    ''' 游戏解构接收器
    ''' </summary>
    Public Interface IGameDestructingReceiver
        ''' <summary>
        ''' 解构游戏
        ''' <param name="e">可取消事件的数据</param>
        ''' </summary>
        Sub DestructGame(e As ComponentModel.CancelEventArgs)
    End Interface
End Namespace