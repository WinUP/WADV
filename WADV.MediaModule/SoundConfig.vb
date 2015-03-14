''' <summary>
''' 声音设置类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class SoundConfig

    Friend Shared LastReadingId As Integer = -1
    Friend Shared LastBgmId As Integer = -1

    ''' <summary>
    ''' 获取或设置背景音量
    ''' </summary>
    ''' <value>音量</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Property Background As Integer

    ''' <summary>
    ''' 获取或设置对话音量
    ''' </summary>
    ''' <value>音量</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Property Reading As Integer

    ''' <summary>
    ''' 获取或设置效果音量
    ''' </summary>
    ''' <value>音量</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Property Effect As Integer

End Class